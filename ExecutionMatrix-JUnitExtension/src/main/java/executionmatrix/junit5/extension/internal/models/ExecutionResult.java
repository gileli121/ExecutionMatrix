package executionmatrix.junit5.extension.internal.models;

public enum ExecutionResult {
    Passed(0),
    Failed(1),
    Skipped(2),
    UnExecuted(3),
    Fatal(4);

    private int value;

    private ExecutionResult(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }
}
