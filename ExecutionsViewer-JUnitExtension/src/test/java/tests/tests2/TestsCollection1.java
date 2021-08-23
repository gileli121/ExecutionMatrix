package tests.tests2;

import executionmatrix.junit5.extension.ExecutionsViewerExtension;
import executionmatrix.junit5.extension.annotations.TestWithVersion;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Tag;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;

@ExtendWith(ExecutionsViewerExtension.class)
@TestWithVersion("v1")
@DisplayName("Sanity Tests")
@Tag("MainFeatureA")
public class TestsCollection1 {


    @Test
    @Tag("SubFeatureA1")
    public void test1() {

    }


    @Test
    @Tag("SubFeatureA2")
    public void test2() {

    }

}
