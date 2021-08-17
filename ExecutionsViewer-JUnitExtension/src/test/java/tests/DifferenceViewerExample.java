package tests;

import executionmatrix.junit5.extension.ExecutionsViewerExtension;
import executionmatrix.junit5.extension.annotations.TestWithVersion;
import org.junit.jupiter.api.Tag;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.opentest4j.AssertionFailedError;
import org.opentest4j.MultipleFailuresError;

import java.util.Arrays;

@TestWithVersion("v2")
@ExtendWith(ExecutionsViewerExtension.class)
@Tag("AssertionsReporting")
public class DifferenceViewerExample {

    @Test
    public void singleAssertionFailedError() {
        throw new AssertionFailedError("A single AssertionFailedError","An expected value","An actual value");
    }

    @Test
    public void multipleAssertionFailedErrors() {
        throw new MultipleFailuresError("Multiple failures test", Arrays.asList(
                new AssertionFailedError("First failure","Expected value","Actual value"),
                new AssertionFailedError("Second failure", "Second expected value", "Second actual value"),
                new AssertionError("Just a simple failure")));
    }

}
