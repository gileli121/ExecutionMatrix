package executionmatrix.junit5.extension.internal.models;

import org.opentest4j.AssertionFailedError;

public class FailureInExecutionDTO {

    private String failureMessage;
    private String expected;
    private String actual;

    public FailureInExecutionDTO() {

    }

    public FailureInExecutionDTO(Throwable exception) {
        this.failureMessage = exception.getMessage();
        if (exception instanceof AssertionFailedError) {
            AssertionFailedError assertionFailedEx = (AssertionFailedError) exception;
            if (assertionFailedEx.isExpectedDefined())
                this.expected = assertionFailedEx.getExpected().getStringRepresentation();
            if (assertionFailedEx.isActualDefined())
                this.actual = assertionFailedEx.getActual().getStringRepresentation();
        }
    }

    public FailureInExecutionDTO(String failureMessage) {
        this.failureMessage = failureMessage;
    }

    public FailureInExecutionDTO(String failureMessage, String expected, String actual) {
        this(failureMessage);
        this.expected = expected;
        this.actual = actual;
    }

    public String getFailureMessage() {
        return failureMessage;
    }

    public void setFailureMessage(String failureMessage) {
        this.failureMessage = failureMessage;
    }

    public String getExpected() {
        return expected;
    }

    public void setExpected(String expected) {
        this.expected = expected;
    }

    public String getActual() {
        return actual;
    }

    public void setActual(String actual) {
        this.actual = actual;
    }
}
