package tests.tests3;


import executionmatrix.junit5.extension.ExecutionsViewerExtension;
import executionmatrix.junit5.extension.annotations.TestWithVersion;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Tag;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;

import java.util.Random;

import static org.junit.jupiter.api.Assertions.fail;

@ExtendWith(ExecutionsViewerExtension.class)
@TestWithVersion("v1.0.3")
@Tag("Module2")
public class ChartTests {

    private static Random rd;
    @BeforeAll
    public static void setup() {
        rd = new Random();
    }

    private void randomFail() {
        if (rd.nextBoolean())
            fail();
    }

    @Tag("Feature1")
    @Test
    public void feature1_test1() {
        randomFail();
    }


    @Tag("Feature1")
    @Test
    public void feature1_test2() {
        randomFail();
    }


    @Tag("Feature1")
    @Test
    public void feature1_test3() {
        randomFail();
    }



    @Tag("Feature1")
    @Test
    public void feature1_test4() {
        randomFail();
    }



    @Tag("Feature1")
    @Test
    public void feature1_test5() {
        randomFail();
    }



    @Tag("Feature1")
    @Test
    public void feature1_test6() {
        randomFail();
    }






    @Tag("Feature2")
    @Test
    public void feature2_test1() {
        randomFail();
    }


    @Tag("Feature2")
    @Test
    public void feature2_test2() {
        randomFail();
    }


    @Tag("Feature2")
    @Test
    public void feature2_test3() {
        randomFail();
    }



    @Tag("Feature2")
    @Test
    public void feature2_test4() {
        randomFail();
    }



    @Tag("Feature2")
    @Test
    public void feature2_test5() {
        randomFail();
    }



    @Tag("Feature2")
    @Test
    public void feature2_test6() {
        randomFail();
    }









    @Tag("Feature3")
    @Test
    public void feature3_test1() {
        randomFail();
    }


    @Tag("Feature3")
    @Test
    public void feature3_test2() {
        randomFail();
    }


    @Tag("Feature3")
    @Test
    public void feature3_test3() {
        randomFail();
    }



    @Tag("Feature3")
    @Test
    public void feature3_test4() {
        randomFail();
    }



    @Tag("Feature3")
    @Test
    public void feature3_test5() {
        randomFail();
    }



    @Tag("Feature2")
    @Test
    public void feature3_test6() {
        randomFail();
    }


    @Tag("Feature4")
    @Test
    public void feature4_test6() {
        randomFail();
    }



}
