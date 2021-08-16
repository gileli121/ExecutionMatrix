package executionmatrix.junit5.extension.internal;

import org.junit.jupiter.api.extension.ExtensionContext;

public class ExtensionContextInfo {
    private final ExtensionContext context;
    private Throwable exception;
    private String consoleOutput = null;

    public ExtensionContextInfo(ExtensionContext context) {
        this.context = context;
    }

    public ExtensionContextInfo(ExtensionContext context, Throwable exception) {
        this(context);
        this.exception = exception;
    }

    public ExtensionContext getContext() {
        return context;
    }

    public Throwable getException() {
        return exception;
    }

    public void setException(Throwable exception) {
        this.exception = exception;
    }

    public void setConsoleOutput(String consoleOutput) {
        this.consoleOutput = consoleOutput;
    }

    public String getConsoleOutput() {
        return consoleOutput;
    }
}
