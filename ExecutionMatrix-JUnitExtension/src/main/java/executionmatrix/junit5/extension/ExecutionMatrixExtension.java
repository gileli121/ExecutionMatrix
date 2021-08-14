package executionmatrix.junit5.extension;

import executionmatrix.junit5.extension.annotations.TestWithVersion;
import executionmatrix.junit5.extension.annotations.TestWithVersionEnv;
import executionmatrix.junit5.extension.internal.ExtensionContextInfo;
import executionmatrix.junit5.extension.internal.helpers.ConsoleOutputCapturer;
import executionmatrix.junit5.extension.internal.helpers.ReportClient;
import executionmatrix.junit5.extension.internal.models.ExecutionResult;
import executionmatrix.junit5.extension.internal.models.PostExecutionDTO;
import executionmatrix.junit5.extension.internal.models.PostTestClassDTO;
import org.apache.commons.lang3.StringUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.extension.*;
import org.junit.jupiter.engine.descriptor.TestMethodTestDescriptor;
import org.junit.platform.engine.TestDescriptor;


import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.util.HashMap;

public class ExecutionMatrixExtension implements InvocationInterceptor, BeforeEachCallback, AfterEachCallback,
        BeforeAllCallback, AfterAllCallback {

    private static final Logger log = LogManager.getLogger(ExecutionMatrixExtension.class);


    private PostTestClassDTO testClassDTO = null;

    private String versionName = "v0.0.0";

    private final HashMap<TestDescriptor, ExtensionContextInfo> contexts = new HashMap<>();

    private ConsoleOutputCapturer outputCapturer;

    @Override
    public void beforeAll(ExtensionContext extensionContext) throws Exception {


        // Get here the information about the test class where all tests executing and store it
        Class<?> testClass = extensionContext.getRequiredTestClass();

        TestWithVersion targetVerAnn = testClass.getAnnotation(TestWithVersion.class);
        if (targetVerAnn != null) {
            this.versionName = targetVerAnn.value();
        } else {
            TestWithVersionEnv targetVerEnvAnn = testClass.getAnnotation(TestWithVersionEnv.class);
            if (targetVerEnvAnn != null) {
                String versionFromEnv = System.getenv(targetVerEnvAnn.value());
                if (versionFromEnv != null)
                    this.versionName = versionFromEnv;
            }
        }

        String packageName = testClass.getPackageName();
        if (packageName.isEmpty())
            packageName = null;

        String className = testClass.getName();
        if (packageName != null)
            className = StringUtils.replaceOnce(className, packageName + ".", "");

        String classDisplayName = extensionContext.getDisplayName();

        testClassDTO = new PostTestClassDTO(packageName, className, classDisplayName);

        // Setup the console capture helper
        outputCapturer = new ConsoleOutputCapturer();

    }

    @Override
    public void beforeEach(ExtensionContext extensionContext) {
        contexts.clear();

        // This call is just to be safe that we cleaned the previous captured console
        outputCapturer.stop();
    }


    @Override
    public void afterEach(ExtensionContext extensionContext) throws Exception {
        // here we will submit the execution result to the server

        TestDescriptor testDescriptor = getTestDescriptor(extensionContext);
        if (testDescriptor == null) {
            log.error("Failed to prepare the execution report. Reason: Failed to get TestDescriptor " +
                    "instance for the current execution");
            return;
        }

        PostExecutionDTO executionDTO = new PostExecutionDTO(contexts, testDescriptor, testClassDTO, versionName);

        ReportClient reportClient = new ReportClient();
        if (!reportClient.reportExecution(executionDTO))
            log.error("Failed to report the execution to the server, See the reason below.");
    }

    @Override
    public void afterAll(ExtensionContext extensionContext) throws Exception {
        // Here we will find any disabled tests that was not executed and report them also
        TestDescriptor rootTestDescriptor = getTestDescriptor(extensionContext);
        if (rootTestDescriptor == null) {
            log.warn("Failed to look for disabled tests. Reason: Can't Get rootTestDescriptor");
            return;
        }

        if (rootTestDescriptor.getChildren() == null) {
            log.warn("Failed to look for disabled tests. Reason: Can't find any tests from rootTestDescriptor");
            return;
        }

        ReportClient reportClient = new ReportClient();

        for (TestDescriptor childTest : rootTestDescriptor.getChildren()) {
            if (contexts.containsKey(childTest)) continue;
            if (!(childTest instanceof TestMethodTestDescriptor)) continue;

            TestMethodTestDescriptor methodTestDescriptor = (TestMethodTestDescriptor) childTest;

            Method testMethod = methodTestDescriptor.getTestMethod();
            if (testMethod.getAnnotation(Disabled.class) == null) continue;

            // We found disabled test here, prepare the report structure
            PostExecutionDTO executionDTO = new PostExecutionDTO(testClassDTO, versionName, childTest, ExecutionResult.Skipped);

            // Submit the report
            if (!reportClient.reportExecution(executionDTO))
                log.error("Failed to report disabled test, See the reason below");

        }
    }

    @Override
    public void interceptTestMethod(Invocation<Void> invocation, ReflectiveInvocationContext<Method> invocationContext, ExtensionContext extensionContext) throws Throwable {
        ExtensionContextInfo contextInfo = listExtensionContext(extensionContext);
        outputCapturer.start();
        try {
            InvocationInterceptor.super.interceptTestMethod(invocation, invocationContext, extensionContext);
        } catch (Throwable e) {
            if (contextInfo != null)
                contextInfo.setException(e);
            throw e;
        } finally {
            String consoleOutput = outputCapturer.stop();
            if (contextInfo != null)
                contextInfo.setConsoleOutput(consoleOutput);
        }
    }

    @Override
    public <T> T interceptTestFactoryMethod(Invocation<T> invocation, ReflectiveInvocationContext<Method> invocationContext, ExtensionContext extensionContext) throws Throwable {
        ExtensionContextInfo contextInfo = listExtensionContext(extensionContext);
        outputCapturer.start();
        try {
            return InvocationInterceptor.super.interceptTestFactoryMethod(invocation, invocationContext, extensionContext);
        } catch (Throwable e) {
            if (contextInfo != null)
                contextInfo.setException(e);
            throw e;
        } finally {
            String consoleOutput = outputCapturer.stop();
            if (contextInfo != null)
                contextInfo.setConsoleOutput(consoleOutput);
        }
    }

    @Override
    public void interceptDynamicTest(Invocation<Void> invocation, ExtensionContext extensionContext) throws Throwable {
        ExtensionContextInfo contextInfo = listExtensionContext(extensionContext);
        outputCapturer.start();
        try {
            InvocationInterceptor.super.interceptDynamicTest(invocation, extensionContext);
        } catch (Throwable e) {
            if (contextInfo != null)
                contextInfo.setException(e);
            throw e;
        } finally {
            String consoleOutput = outputCapturer.stop();
            if (contextInfo != null)
                contextInfo.setConsoleOutput(consoleOutput);
        }
    }

    private TestDescriptor getTestDescriptor(ExtensionContext context) {
        try {
            Class<?> clazz = Class.forName("org.junit.jupiter.engine.descriptor.AbstractExtensionContext");
            Field privateStringField = clazz.getDeclaredField("testDescriptor");
            privateStringField.setAccessible(true);
            return (TestDescriptor) privateStringField.get(context);
        } catch (Throwable e) {
            log.error("Failed to get TestDescriptor instance");
            return null;
        }
    }

    private ExtensionContextInfo listExtensionContext(ExtensionContext context) {
        TestDescriptor testDescriptor = getTestDescriptor(context);
        if (testDescriptor == null)
            return null;

        ExtensionContextInfo extensionContextInfo = new ExtensionContextInfo(context);
        contexts.put(testDescriptor, extensionContextInfo);
        return extensionContextInfo;
    }


}
