package executionmatrix.junit5.extension.internal.models;

import java.util.List;

public class PostTestClassDTO {

    private String versionName;
    private String packageName;
    private String className;
    private List<String> mainFeatures;
    private String displayName;

    public PostTestClassDTO() {

    }

    public PostTestClassDTO(String packageName,
                            String className,
                            String displayName,
                            List<String> mainFeatures,
                            String versionName) {
        this.packageName = packageName;
        this.className = className;
        this.displayName = displayName;
        this.mainFeatures = mainFeatures;
        this.versionName = versionName;
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

    public List<String> getMainFeatures() {
        return mainFeatures;
    }

    public String getVersionName() {
        return versionName;
    }
}
