package tests.tests3;


import app.MyFakeBlogWebApp;
import executionmatrix.junit5.extension.ExecutionsViewerExtension;
import executionmatrix.junit5.extension.annotations.TestWithVersion;
import org.junit.jupiter.api.*;
import org.junit.jupiter.api.extension.ExtendWith;

import java.util.Random;
import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.DynamicContainer.dynamicContainer;
import static org.junit.jupiter.api.DynamicTest.dynamicTest;

@ExtendWith(ExecutionsViewerExtension.class)
@TestWithVersion("v1.0.10")
@Tag("Module3")
public class OtherTests {

    private static Random rd;
    @BeforeAll
    public static void setup() {
        rd = new Random();
    }

    private void randomFail() {
        if (rd.nextBoolean())
            fail();
    }

    @TestFactory
    @DisplayName("Login Tests")
    @Tag("Feature4")
    Stream<DynamicNode> loginTests() {
        return Stream.of(
                dynamicTest("Login with valid username", () -> {
                    System.out.println("Testing login with valid username ...");
                    MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                    app.login("user1","1234");
                    assertTrue(app.isLoggedIn(),"Failed to log in");
                    fail("Failed to login!");
                    randomFail();
                }),
                dynamicContainer("Failure login tests", Stream.of(
                        dynamicTest("Login with invalid password", () -> {
                            System.out.println("Testing login with invalid password ...");
                            MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                            app.login("invalidUser","invalidPassword");
                            assertFalse(app.isLoggedIn(),"Expected the login to fail but succeed instead");
                            randomFail();
                        }),
                        dynamicTest("Block login attempts after 10 failed logins", () -> {
                            System.out.println("Testing block login attempts after 10 failed logins ...");
                            MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                            for (int attempt = 1; attempt <= 10; attempt++)
                                app.login("invalidUser","invalidPassword");

                            assertThrows(RuntimeException.class,() -> app.login("invalidUser",
                                    "invalidPassword"),"The app did not blocked attempt number 11 to login");
                            randomFail();

                        })
                ))
        );
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
