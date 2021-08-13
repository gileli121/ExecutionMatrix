package executionmatrix.junit5.extension;

import com.google.gson.Gson;
import executionmatrix.junit5.extension.annotations.TestWithVersion;
import executionmatrix.junit5.extension.annotations.TestWithVersionEnv;
import executionmatrix.junit5.extension.internal.ExtensionContextInfo;
import executionmatrix.junit5.extension.internal.models.PostExecutionDTO;
import executionmatrix.junit5.extension.internal.models.PostTestClassDTO;
import org.apache.commons.lang3.StringUtils;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClientBuilder;
import org.junit.jupiter.api.extension.*;
import org.junit.platform.engine.TestDescriptor;


import java.lang.annotation.Annotation;
import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.util.HashMap;

public class TestMatrixExtension implements InvocationInterceptor, BeforeEachCallback, AfterEachCallback,
        BeforeAllCallback {

    private PostTestClassDTO testClassDTO = null;

    private String versionName = "v0.0.0";

    private final HashMap<TestDescriptor, ExtensionContextInfo> contexts = new HashMap<>();

    private final Gson GSON = new Gson();


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

    }

    @Override
    public void beforeEach(ExtensionContext extensionContext) throws Exception {
        contexts.clear();
    }


    @Override
    public void afterEach(ExtensionContext extensionContext) throws Exception {
        // here we will submit the execution result to the server

        TestDescriptor testDescriptor = getTestDescriptor(extensionContext);
        if (testDescriptor == null) {
            // TODO: Log here error that we failed to reed the test information
            return;
        }

        PostExecutionDTO executionResult = new PostExecutionDTO(contexts, testDescriptor, testClassDTO, versionName);
        String executionResultJson = GSON.toJson(executionResult);


        try (CloseableHttpClient httpClient = HttpClientBuilder.create().build()) {
            HttpPost request = new HttpPost("http://localhost:49689/api/ReportExtension/SubmitExecution");
            StringEntity params = new StringEntity(executionResultJson);
            request.addHeader("content-type", "application/json");
            request.setEntity(params);

            // TODO: Add logic here to handle the response and warn the user in case of error
            HttpResponse res = httpClient.execute(request);
        } catch (Exception ex) {
            // handle exception here
        }
    }

    @Override
    public void interceptTestMethod(Invocation<Void> invocation, ReflectiveInvocationContext<Method> invocationContext, ExtensionContext extensionContext) throws Throwable {
        ExtensionContextInfo contextInfo = listExtensionContext(extensionContext);
        try {
            InvocationInterceptor.super.interceptTestMethod(invocation, invocationContext, extensionContext);
        } catch (Throwable e) {
            if (contextInfo != null)
                contextInfo.setException(e);
            throw e;
        }
    }

    @Override
    public <T> T interceptTestFactoryMethod(Invocation<T> invocation, ReflectiveInvocationContext<Method> invocationContext, ExtensionContext extensionContext) throws Throwable {
        ExtensionContextInfo contextInfo = listExtensionContext(extensionContext);
        try {
            return InvocationInterceptor.super.interceptTestFactoryMethod(invocation, invocationContext, extensionContext);
        } catch (Throwable e) {
            if (contextInfo != null)
                contextInfo.setException(e);
            throw e;
        }
    }

    @Override
    public void interceptDynamicTest(Invocation<Void> invocation, ExtensionContext extensionContext) throws Throwable {
        ExtensionContextInfo contextInfo = listExtensionContext(extensionContext);
        try {
            InvocationInterceptor.super.interceptDynamicTest(invocation, extensionContext);
        } catch (Throwable e) {
            if (contextInfo != null)
                contextInfo.setException(e);
            throw e;
        }
    }

    private TestDescriptor getTestDescriptor(ExtensionContext context) {
        try {
            Class<?> clazz = Class.forName("org.junit.jupiter.engine.descriptor.AbstractExtensionContext");
            Field privateStringField = clazz.getDeclaredField("testDescriptor");
            privateStringField.setAccessible(true);
            return (TestDescriptor) privateStringField.get(context);
        } catch (Throwable e) {
            // TODO: log here error
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
