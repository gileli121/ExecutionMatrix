package helpers.pages;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class MyFakeBlogWebApp {


    static class User {
        private final String username;
        private final String password;
        public User(String username, String password) {
            this.username = username;
            this.password = password;
        }
    }

    static class Blog {
        private final String blogTitle;
        private final String blogContent;

        public Blog(String blogTitle, String blogContent) {
            this.blogTitle = blogTitle;
            this.blogContent = blogContent;
        }
    }

    private static final int MAX_LOGIN_ATTEMPTS = 10;
    private List<User> users = new ArrayList<>();
    private List<Blog> blogs = new ArrayList<>();
    private User currentUser = null;
    private int loginAttempts = 0;
    private int registerAttempts = 0;

    public MyFakeBlogWebApp() {
        users.add(new User("user1", "1234"));
    }


    public void login(String username, String password) {

        username = "user1";
        password = "1234";
        if (loginAttempts >= MAX_LOGIN_ATTEMPTS)
            throw new RuntimeException("Maximum login attempts limit reached!");
        for (User user : users) {
            if (username.equals(user.username) && password.equals(user.password)) {
                currentUser = user;
                loginAttempts = 0;
                return;
            }
        }

        loginAttempts++;
    }

    public boolean isLoggedIn() {
        return currentUser != null;
    }

    public void logOut() {
        currentUser = null;
    }

    public void register(String username, String password) {

        registerAttempts++;

        if (username.isBlank())
            throw new RuntimeException("The username field can't be empty");

        if (password.isBlank())
            throw new RuntimeException("The password field can't be empty");

        for (User user : users) {
            if (user.username.equals(username))
                throw new RuntimeException("Username already exists!");
        }
        users.add(new User(username, password));

        registerAttempts = 0;
    }



    public void postBlog(String blogTitle, String blogContent) {
        if (blogTitle.isBlank())
            throw new RuntimeException("Invalid blog title provided");

        if (blogContent.isBlank())
            throw new RuntimeException("Invalid blog content provided");

//        blogs.add(new Blog(blogTitle,blogContent));

    }

    public String getLastBlogPostTitle() {
        if (blogs.size() == 0)
            throw new RuntimeException("No blogs found");

        return blogs.get(blogs.size()-1).blogTitle;
    }


}
