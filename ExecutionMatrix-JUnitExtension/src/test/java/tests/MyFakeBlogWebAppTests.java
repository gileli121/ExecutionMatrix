package tests;

import executionmatrix.junit5.extension.TestMatrixExtension;
import executionmatrix.junit5.extension.annotations.TestWithVersionEnv;
import helpers.pages.MyFakeBlogWebApp;
import org.junit.jupiter.api.*;
import org.junit.jupiter.api.extension.ExtendWith;


import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;
import static org.junit.jupiter.api.DynamicContainer.dynamicContainer;
import static org.junit.jupiter.api.DynamicTest.dynamicTest;

@ExtendWith(TestMatrixExtension.class)
@TestWithVersionEnv("TARGET_BUILD_VERSION")
@DisplayName("Sanity Tests")
@Tag("BasicUserFunctionality")
public class MyFakeBlogWebAppTests {

    @TestFactory
    @DisplayName("Login Tests")
    @Tag("Account")
    Stream<DynamicNode> loginTests() {
        return Stream.of(
                dynamicTest("Login with valid username", () -> {
                    MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                    app.login("user1","1234");
                    assertTrue(app.isLoggedIn(),"Failed to log in");
                }),
                dynamicContainer("Failure login tests", Stream.of(
                        dynamicTest("Login with invalid password", () -> {
                            MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                            app.login("invalidUser","invalidPassword");
                            assertFalse(app.isLoggedIn(),"Expected the login to fail but succeed instead");
                        }),
                        dynamicTest("Block login attempts after 10 failed logins", () -> {
                            MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                            for (int attempt = 1; attempt <= 10; attempt++)
                                app.login("invalidUser","invalidPassword");

                            assertThrows(RuntimeException.class,() -> app.login("invalidUser",
                                    "invalidPassword"),"The app did not blocked attempt number 11 to login");

                        })
                ))
        );
    }


    @TestFactory
    @DisplayName("Register Tests")
    @Tag("Account")
    Stream<DynamicNode> registerTests() {
        return Stream.of(
                dynamicTest("Register with valid username & password", () -> {
                    MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                    app.register("user2","1234");
                    app.login("user2","1234");
                    assertTrue(app.isLoggedIn(),"Register failed (Failed to log in)");
                }),
                dynamicContainer("Failure register tests", Stream.of(
                        dynamicContainer("Field validation tests", Stream.of(
                                dynamicTest("Register with empty username field", () -> {
                                    MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                                    assertThrows(RuntimeException.class, () -> app.register("","1234"),
                                            "Expected to fail register due to empty username field");
                                }),
                                dynamicTest("Register with empty password field", () -> {
                                    MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                                    assertThrows(RuntimeException.class, () -> app.register("user2",""),
                                            "Expected to fail register due to empty password field");
                                })
                        )),
                        dynamicTest("Block register attempts after 10 failed attempts", () -> {
                            MyFakeBlogWebApp app = new MyFakeBlogWebApp();
                            for (int attempt = 1; attempt <= 10; attempt++) {
                                try {
                                    app.register("","invalidPassword");
                                } catch (RuntimeException ignored) {}
                            }

                            assertThrows(RuntimeException.class,() -> app.register("",
                                    "invalidPassword"),"The app did not blocked attempt number 11 to login");

                        })
                ))
        );
    }

    @TestFactory
    @Tag("BlogsFunctionality")
    @DisplayName("Post Blogs Tests")
    Stream<DynamicNode> testBlogsFunctionality() {
        MyFakeBlogWebApp app = new MyFakeBlogWebApp();
        app.login("user1","1234");
        assertTrue(app.isLoggedIn(),"Precondition Failed: Failed to login");
        return Stream.of(
                dynamicTest("Post a valid blog post", () -> {
                    app.postBlog("Blog Title 1","Blog Content 1");
                    String lastBlogTitle = app.getLastBlogPostTitle();
                    assertEquals("Blog Title 1",lastBlogTitle,"Posting the blog failed because the blog title was wrong");
                }),
                dynamicContainer("Failure blog post tests", Stream.of(
                        dynamicContainer("Field validation tests", Stream.of(
                                dynamicTest("Post blog with empty title field", () -> {
                                    assertThrows(RuntimeException.class, () -> app.postBlog("","blogContent"),
                                            "Expected to fail posting blog due to empty blogTitle field");
                                }),
                                dynamicTest("Post blog with empty content field", () -> {
                                    assertThrows(RuntimeException.class, () -> app.postBlog("blogTitle1",""),
                                            "Expected to fail posting blog due to empty blogContent field");
                                })
                        ))
                ))
        );
    }

}

