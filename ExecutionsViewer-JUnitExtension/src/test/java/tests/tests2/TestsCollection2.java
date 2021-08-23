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
@Tag("MainFeatureB")
public class TestsCollection2 {


    @Test
    @Tag("SubFeatureB1")
    public void test1() {

    }


    @Test
    @Tag("SubFeatureB2")
    public void test2() {

    }

    @Test
    @Tag("SubFeatureB3")
    public void test3_inFeatureBOnly() {

    }

    @Test
    @Tag("SubFeatureB3")
    public void test4_inFeatureBOnly_secondFeatureB3() {

    }

    @Test
    public void test5_inFeatureBOnly() {

    }

}
