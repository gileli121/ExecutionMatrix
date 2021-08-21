package executionmatrix.junit5.extension.internal.models;

import executionmatrix.junit5.extension.internal.ExtensionContextInfo;
import executionmatrix.junit5.extension.internal.helpers.Utils;
import org.junit.platform.engine.TestDescriptor;
import org.junit.platform.engine.TestTag;
import org.opentest4j.AssertionFailedError;
import org.opentest4j.MultipleFailuresError;

import java.util.*;

public class PostExecutionDTO {

    private String testMethodName;
    private String testDisplayName;
    private PostTestClassDTO testClass;
    private List<String> features;
    private ExecutionResult result;
    private String output;
    private List<PostExecutionDTO> childExecutions;
    private List<FailureInExecutionDTO> failures;

    private transient PostExecutionDTO parent;

    private static final String EOL = System.lineSeparator();

    public PostExecutionDTO() {
    }

    private PostExecutionDTO(HashMap<TestDescriptor, ExtensionContextInfo> contextHashMap, TestDescriptor testDescriptor, PostExecutionDTO parent) {
        this.parent = parent;
        ExtensionContextInfo contextInfo = contextHashMap.get(testDescriptor);
        testMethodName = testDescriptor.getLegacyReportingName();
        testDisplayName = testDescriptor.getDisplayName();


        if (contextInfo != null) {

            if (contextInfo.getConsoleOutput() != null)
                output = contextInfo.getConsoleOutput();


            if (contextInfo.getException() != null) {

                Throwable exception = contextInfo.getException();

                if (output == null)
                    output = "";

                output += exception.getClass().getName();
                if (exception.getMessage() != null)
                    output += ":" + EOL + "\t" + exception.getMessage();

                output += EOL;

                if (exception instanceof MultipleFailuresError) {
                    MultipleFailuresError multiException = (MultipleFailuresError)exception;

                    result = ExecutionResult.Failed;

                    if (multiException.hasFailures()) {
                        List<Throwable> failuresExs = multiException.getFailures();
                        this.failures = new ArrayList<>();
                        for (Throwable failureEx : failuresExs) {
                            this.failures.add(new FailureInExecutionDTO(failureEx));
                            if (!(failureEx instanceof AssertionError))
                                this.result = ExecutionResult.Fatal;
                        }
                    }

                } else if (exception instanceof AssertionFailedError) {
                    this.failures = new ArrayList<>();
                    this.failures.add(new FailureInExecutionDTO(exception));
                    result = ExecutionResult.Failed;
                } else if (exception instanceof AssertionError)
                    result = ExecutionResult.Failed;
                else
                    result = ExecutionResult.Fatal;

                if (parent != null) {
                    PostExecutionDTO current = parent;
                    while (current != null) {

                        if (current.result != ExecutionResult.Failed && current.result != ExecutionResult.Fatal)
                            current.result = result;
                        else if (current.result == ExecutionResult.Fatal)
                            result = current.result;
                        else
                            current.result = result;

                        current = current.parent;
                    }
                }



            } else {
                result = ExecutionResult.Passed;
            }


        }


        if (testDescriptor.getChildren() != null && testDescriptor.getChildren().size() > 0) {
            childExecutions = new ArrayList<>();
            for (TestDescriptor childTestDescriptor : testDescriptor.getChildren())
                childExecutions.add(new PostExecutionDTO(contextHashMap, childTestDescriptor, this));
        }

    }

    // This constructor is used for test that executed
    public PostExecutionDTO(HashMap<TestDescriptor, ExtensionContextInfo> contextHashMap, TestDescriptor testDescriptor, PostTestClassDTO testClass) {
        this(contextHashMap, testDescriptor, (PostExecutionDTO) null);
        this.testClass = testClass;

        Set<TestTag> tagsSet = testDescriptor.getTags();
        if (tagsSet != null && tagsSet.size() > 0) {
            List<String> features = new ArrayList<>();
            for (TestTag tag : tagsSet) {
                String feature = Utils.splitCamelCase(tag.getName());
                if (!testClass.getMainFeatures().contains(feature))
                    features.add(Utils.splitCamelCase(tag.getName()));
            }

            if (features.size() > 0)
                this.features = features;
        }
    }

    // This constructor is used for unexecuted test like disabled test
    public PostExecutionDTO(PostTestClassDTO testClass, TestDescriptor testDescriptor, ExecutionResult executionResult) {
        this.testClass = testClass;
        this.testMethodName = testDescriptor.getLegacyReportingName();
        this.testDisplayName = testDescriptor.getDisplayName();

        Set<TestTag> tagsSet = testDescriptor.getTags();
        if (tagsSet != null && tagsSet.size() > 0) {
            features = new ArrayList<>();
            for (TestTag tag : tagsSet)
                features.add(Utils.splitCamelCase(tag.getName()));
        }

        this.result = executionResult;
    }

    private String getOptimizedName() {
        return testDisplayName != null ? testDisplayName + " ("+testMethodName+")" : testMethodName;
    }

    public String getTestMethodName() {
        return testMethodName;
    }

    public void setTestMethodName(String testMethodName) {
        this.testMethodName = testMethodName;
    }

    public String getTestDisplayName() {
        return testDisplayName;
    }

    public void setTestDisplayName(String testDisplayName) {
        this.testDisplayName = testDisplayName;
    }

    public PostTestClassDTO getTestClass() {
        return testClass;
    }

    public void setTestClass(PostTestClassDTO testClass) {
        this.testClass = testClass;
    }

    public List<String> getFeatures() {
        return features;
    }

    public void setFeatures(List<String> features) {
        this.features = features;
    }

    public ExecutionResult getResult() {
        return result;
    }

    public void setResult(ExecutionResult result) {
        this.result = result;
    }

    public String getOutput() {
        return output;
    }

    public void setOutput(String output) {
        this.output = output;
    }

    public List<PostExecutionDTO> getChildExecutions() {
        return childExecutions;
    }

    public void setChildExecutions(List<PostExecutionDTO> childExecutions) {
        this.childExecutions = childExecutions;
    }

    public List<FailureInExecutionDTO> getFailures() {
        return failures;
    }

    public void setFailures(List<FailureInExecutionDTO> failures) {
        this.failures = failures;
    }
}
