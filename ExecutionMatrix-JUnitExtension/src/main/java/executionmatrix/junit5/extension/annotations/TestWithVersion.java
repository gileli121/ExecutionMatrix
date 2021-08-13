package executionmatrix.junit5.extension.annotations;


import java.lang.annotation.*;

@Target({ElementType.TYPE})
@Retention(RetentionPolicy.RUNTIME)
@Documented
@Inherited
public @interface TestWithVersion {
    String value();
}
