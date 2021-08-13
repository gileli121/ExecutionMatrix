package my_package;

import executionmatrix.junit5.extension.TestMatrixExtension;
import org.junit.jupiter.api.*;
import org.junit.jupiter.api.extension.ExtendWith;

import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.fail;
import static org.junit.jupiter.api.DynamicContainer.dynamicContainer;
import static org.junit.jupiter.api.DynamicTest.dynamicTest;

@ExtendWith(TestMatrixExtension.class)
@DisplayName("My Display Name for Class")
@Tag("AgentTests")
public class Tests2 {


    @Test
    @DisplayName("Test 1 - B")
    public void test1() {
        System.out.println(1);
        fail("some error");
    }

    @Test
    @DisplayName("Test 2 - B")
    public void test2() {
        System.out.println(1);
//        fail("some error");
    }


    @TestFactory
    @DisplayName("Main (@TestFactory) 2 - B")
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
