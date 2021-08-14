package tests;

import app.MyFakeBlogWebApp;
import executionmatrix.junit5.extension.ExecutionMatrixExtension;
import executionmatrix.junit5.extension.annotations.TestWithVersionEnv;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Tag;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;

import static org.junit.jupiter.api.Assertions.*;

@ExtendWith(ExecutionMatrixExtension.class)
@DisplayName("Basic Tests Example")
@Tag("Account")
@TestWithVersionEnv("TARGET_BUILD_VERSION")
public class BasicTestsExample {

    private MyFakeBlogWebApp app;

    @BeforeEach
    public void beforeEach() {
        app = new MyFakeBlogWebApp();
    }

    @Test
    @DisplayName("Test valid login")
    @Tag("Login")
    public void testValidLogin() {
        System.out.println("Testing a valid login scenario ...");
        app.login("user1","1234");
        assertTrue(app.isLoggedIn(),"Failed to log in");
    }

    @Test
    @DisplayName("Test login with wrong password")
    @Tag("Login")
    public void testLoginWithWrongPassword() {
        System.out.println("Testing login with wrong password scenario ...");
        app.login("user1","wrongPassword");
        assertFalse(app.isLoggedIn(),"Expected the login to fail but it passed instead");
    }

    @Test
    @DisplayName("Test multiple failed login attempts")
    @Tag("Login")
    public void testMultipleFailedLoginAttempts() {
        System.out.println("Testing multiple failed login attempts ...");
        for (int attempt = 1; attempt <= 10; attempt++)
            app.login("invalidUser","invalidPassword");

        assertThrows(RuntimeException.class,() -> app.login("invalidUser",
                "invalidPassword"),"The app did not blocked attempt number 11 to login");
    }

    @Test
    @DisplayName("Test register new user")
    @Tag("Register")
    public void testRegisterNewUser() {
        System.out.println("Testing register new user ...");
        app.register("newUser","1234");
        app.login("newUser","1234");
        assertTrue(app.isLoggedIn(),"Can't validate the register flow. " +
                "Failed to login to the registered account");
    }

    @Test
    @DisplayName("Test register existing user")
    @Tag("Register")
    public void testRegisterExistingUser() {
        System.out.println("Testing register existing user ...");
        app.register("newUser","1234");
        app.login("newUser","1234");
        assertTrue(app.isLoggedIn(),"Can't validate the register flow. " +
                "Failed to login to the registered account");
        app.logOut();
        assertFalse(app.isLoggedIn(),"Failed to log out after first register attempt");

        assertThrows(RuntimeException.class, () -> app.register("newUser","12345"),
                "Expected the register to failed after second attempt to register the same " +
                        "account. But instead the account is registered again");

    }

}
