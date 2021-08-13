# ExecutionMatrix
ExecutionMatrix - Deploy, Host and view your JUnit report in external reporting tool


# Screenshots
![image](https://user-images.githubusercontent.com/17680514/129413973-ac29371f-f315-46ec-b46a-0333d7920b8a.png)
![image](https://user-images.githubusercontent.com/17680514/129414108-35a8806b-13ba-45ed-9741-f2e125faef36.png)
![image](https://user-images.githubusercontent.com/17680514/129414258-55819f21-2616-48b7-9de8-65ef15287c07.png)

![image](https://user-images.githubusercontent.com/17680514/129414534-03c8e53e-ab50-488f-a7bc-541e0605175c.png)


# Installation
For now there is no installation method & guide.
You will need to build the project on your own

# How to use

## Setup the Report server

> TODO

## Setup the JUnit extension & execution configurations

In order to be able to report from JUnit to the server, you need to use `TestMatrixExtension` JUnit extension class.
Add the following annotation above the test class

```java
@ExtendWith(TestMatrixExtension.class)
...
public class MyFakeBlogWebAppTests {
```

Next, define environment variable that will hold the build version.
This is used in order to separate the execution reports by build versions later.
![image](https://user-images.githubusercontent.com/17680514/129416721-186a76ce-223a-4b7e-a1de-7e2295bc9f46.png)


Add `@TestWithVersionEnv("TARGET_BUILD_VERSION")` annotation above the tests class. `TARGET_BUILD_VERSION` can be any name.
In this case, `TARGET_BUILD_VERSION` will be the name of the environment variable that hold the version under test.
For example the `TARGET_BUILD_VERSION` value can be `v0.0.1`. When you have a new build, you should change this value to reflect the new version
so the new reports will be under the new version.


```java
@ExtendWith(TestMatrixExtension.class)
@TestWithVersionEnv("TARGET_BUILD_VERSION")
...
public class MyFakeBlogWebAppTests {
```

If you use intellij, you can configure the `TARGET_BUILD_VERSION` environment variable here:
![image](https://user-images.githubusercontent.com/17680514/129416298-f9148d8d-5b95-4413-b516-bc91a5b47b99.png)


Next, you must to add `@Tag` to the test class & tests.
The tag is mapped to feature that shown here:
![image](https://user-images.githubusercontent.com/17680514/129415442-2462de5f-5fe5-4157-840f-764261e887c7.png)

You will also be able to select only executions that related to the `@Tag`/Feature
![image](https://user-images.githubusercontent.com/17680514/129415665-9ab0e8e0-8b86-4562-9761-1c45b334802c.png)


To add @Tag, you need to add under each test `@Tag` annotation with feature name inside.
For example:
![image](https://user-images.githubusercontent.com/17680514/129415798-de45a64c-1e3f-48d1-927a-66c7c02ff073.png)


Next, It is highly recommended to add `@DisplayName` annotation to the class and tests.
The value in the `@DisplayName` is simplified name that is easier to read.
The `@DisplayName` is reflected here:
![image](https://user-images.githubusercontent.com/17680514/129417180-eb3b67a5-980d-48d6-83bb-ee08b7fc5fea.png)
![image](https://user-images.githubusercontent.com/17680514/129417324-10f37ca2-6fcf-4e43-8d93-8d16522bf1a2.png)


# Contribution Guidelines

Please ensure your pull request adheres to the following guidelines:

- Alphabetize your entry.
- Search previous suggestions before making a new one, as yours may be a duplicate.
- Suggested READMEs should be beautiful or stand out in some way.
- Make an individual pull request for each suggestion.
- New categories, or improvements to the existing categorization are welcome.
- Keep descriptions short and simple, but descriptive.
- Start the description with a capital and end with a full stop/period.
- Check your spelling and grammar.
- Make sure your text editor is set to remove trailing whitespace.
- Use the `#readme` anchor for GitHub READMEs to link them directly

Thank you for your suggestions!
