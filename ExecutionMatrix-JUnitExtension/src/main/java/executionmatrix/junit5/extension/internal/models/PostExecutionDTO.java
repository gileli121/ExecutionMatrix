package executionmatrix.junit5.extension.internal.models;

import executionmatrix.junit5.extension.internal.ExtensionContextInfo;
import executionmatrix.junit5.extension.internal.helpers.Utils;
import org.junit.platform.engine.TestDescriptor;
import org.junit.platform.engine.TestTag;

import java.util.*;

public class PostExecutionDTO {

    private String testMethodName;
    private String testDisplayName;
    private PostTestClassDTO testClass;
    private List<String> featureNames;
    private String versionName;
    private ExecutionResult result;
    private String output;
    private List<PostExecutionDTO> childExecutions;

    private transient PostExecutionDTO parent;

    public PostExecutionDTO() {
    }

    private PostExecutionDTO(HashMap<TestDescriptor, ExtensionContextInfo> contextHashMap, TestDescriptor testDescriptor, PostExecutionDTO parent) {
        this.parent = parent;
        ExtensionContextInfo contextInfo = contextHashMap.get(testDescriptor);
        testMethodName = testDescriptor.getLegacyReportingName();
        testDisplayName = testDescriptor.getDisplayName();

        if (contextInfo != null && contextInfo.getException() != null) {
            Throwable exception = contextInfo.getException();
            if (exception instanceof AssertionError)
                result = ExecutionResult.Failed;
            else
                result = ExecutionResult.Fatal;

            if (parent != null) {
                PostExecutionDTO current = parent;
                while (current != null) {

                    if (current.result != ExecutionResult.Failed && current.result != ExecutionResult.Fatal) {
                        current.result = result;
                    } else if (current.result == ExecutionResult.Fatal)
                        result = current.result;

                    current = current.parent;
                }
            }

            output = exception.getMessage();
        } else {
            result = ExecutionResult.Passed;
        }

        if (testDescriptor.getChildren() != null && testDescriptor.getChildren().size() > 0) {
            childExecutions = new ArrayList<>();
            for (TestDescriptor childTestDescriptor : testDescriptor.getChildren())
                childExecutions.add(new PostExecutionDTO(contextHashMap, childTestDescriptor, this));
        }

    }

    // This constructor is used for test that executed
    public PostExecutionDTO(HashMap<TestDescriptor, ExtensionContextInfo> contextHashMap, TestDescriptor testDescriptor, PostTestClassDTO testClass, String versionName) {
        this(contextHashMap, testDescriptor, (PostExecutionDTO) null);
        this.testClass = testClass;

        Set<TestTag> tagsSet = testDescriptor.getTags();
        if (tagsSet != null && tagsSet.size() > 0) {
            featureNames = new ArrayList<>();
            for (TestTag tag : tagsSet)
                featureNames.add(Utils.splitCamelCase(tag.getName()));

        }
        this.versionName = versionName;
    }

    // This constructor is used for unexecuted test like disabled test
    public PostExecutionDTO(PostTestClassDTO testClass, String versionName, TestDescriptor testDescriptor, ExecutionResult executionResult) {
        this.testClass = testClass;
        this.versionName = versionName;
        this.testMethodName = testDescriptor.getLegacyReportingName();
        this.testDisplayName = testDescriptor.getDisplayName();

        Set<TestTag> tagsSet = testDescriptor.getTags();
        if (tagsSet != null && tagsSet.size() > 0) {
            featureNames = new ArrayList<>();
            for (TestTag tag : tagsSet)
                featureNames.add(Utils.splitCamelCase(tag.getName()));
        }

        this.result = executionResult;
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

    public List<String> getFeatureNames() {
        return featureNames;
    }

    public void setFeatureNames(List<String> featureNames) {
        this.featureNames = featureNames;
    }

    public String getVersionName() {
        return versionName;
    }

    public void setVersionName(String versionName) {
        this.versionName = versionName;
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
}
