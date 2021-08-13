package executionmatrix.junit5.extension.internal.models;

public class PostTestClassDTO {
    private String packageName;
    private String className;
    private String displayName;
    
    public PostTestClassDTO() {
        
    }
    
    public PostTestClassDTO(String packageName,
                            String className,
                            String displayName) {
        this.packageName = packageName;
        this.className = className;
        this.displayName = displayName;
    }

    public String getClassName() {
        return className;
    }

    public String getDisplayName() {
        return displayName;
    }

    public String getPackageName() {
        return packageName;
    }
}
