package my_package;

import executionmatrix.junit5.extension.TestMatrixExtension;
import executionmatrix.junit5.extension.annotations.TestWithVersion;
import org.junit.jupiter.api.*;
import org.junit.jupiter.api.extension.ExtendWith;


import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.fail;
import static org.junit.jupiter.api.DynamicContainer.dynamicContainer;
import static org.junit.jupiter.api.DynamicTest.dynamicTest;

@ExtendWith(TestMatrixExtension.class)
@TestWithVersion("v1.0.0")
@DisplayName("My Display Name for Class")
@Tag("featureAClassLevel")
@Tag("featureBClassLevel")
public class Tests {

    @Test
    @Tag("childFeatureA1")
    @Tag("childFeatureA2")
    @DisplayName("Test 1")
    public void test1() {
        System.out.println(1);
        fail("some error");
    }

    @Test
    @Tag("childFeatureB1")
    @Tag("childFeatureB2")
    @DisplayName("Test 2")
    public void test2() {
        System.out.println(1);
//        fail("some error");
    }

    @Test
    @DisplayName("Test 3")
    public void test3() {
        System.out.println(1);
//        fail("some error");
    }


    @Test
    @DisplayName("Test - new12")
    public void testNew2() {
        System.out.println(1);
//        fail("some error");
    }
    @TestFactory
    @Tag("childFeatureC1")
    @DisplayName("Main (@TestFactory) 2")
    Stream<DynamicNode> dynamicTestsWithCollection() {
        return Stream.of(
                dynamicContainer("Child 1 (dynamicContainer)", Stream.of(
                        dynamicTest("Child 1 (dynamicTest)", () -> {
                            System.out.println(1);
                            fail("some error");
                        }),
                        dynamicTest("Child 2 (dynamicTest)", () -> {
                            System.out.println(1);
//                            fail("some error");
                        }),
                        dynamicContainer("Child 3 (dynamicContainer)", Stream.of(
                                dynamicTest("Child 1 (dynamicTest)", () -> {
                                    System.out.println(1);
                                    fail("some error");
                                }),
                                dynamicTest("Child 2 (dynamicTest)", () -> {
                                    System.out.println(1);
//                                    fail("some error");
                                })
                        ))
                )),
                dynamicTest("Child 2 (dynamicTest)", () -> {
                    System.out.println(1);
                    fail("some error");
                })
        );
    }


}
