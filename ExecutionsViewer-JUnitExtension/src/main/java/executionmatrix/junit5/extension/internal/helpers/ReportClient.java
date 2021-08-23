package executionmatrix.junit5.extension.internal.helpers;

import com.google.gson.Gson;
import executionmatrix.junit5.extension.ExecutionsViewerExtension;
import executionmatrix.junit5.extension.internal.models.PostExecutionDTO;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClientBuilder;
import org.apache.http.util.EntityUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class ReportClient {

    private static final String EOL = System.lineSeparator();
    private static final Logger log = LogManager.getLogger(ExecutionsViewerExtension.class);
    private static final String REPORTS_SERVER_ADDRESS = System.getenv("REPORTS_SERVER_ADDRESS"); // Example value: http://localhost:49689

    private final Gson GSON = new Gson();


    public boolean reportExecution(PostExecutionDTO executionDTO) {

        if (REPORTS_SERVER_ADDRESS == null) {
            log.error("Skipping reporting to ExecutionMatrix server because the environment variable REPORTS_SERVER_ADDRESS is undefined." + EOL
                    + "Please define this variable first. Example value: http://localhost:49689");
            return false;
        }


        try (CloseableHttpClient httpClient = HttpClientBuilder.create().build()) {
            String executionResultJson = GSON.toJson(executionDTO);

            System.out.println(executionResultJson);

            HttpPost request = new HttpPost(REPORTS_SERVER_ADDRESS + "/api/ReportExtension/SubmitExecution");
            StringEntity params = new StringEntity(executionResultJson);
            request.addHeader("content-type", "application/json");
            request.setEntity(params);

            HttpResponse response = httpClient.execute(request);

            int responseCode = response.getStatusLine().getStatusCode();
            if (responseCode != 200) {
                log.error("Failed to report this execution to ExecutionMatrix server. Reason: The request to submit the " +
                        "execution ended with response code " + responseCode + EOL +
                        "Response body: " + EntityUtils.toString(response.getEntity(), "UTF-8"));
            }

            return true;

        } catch (Exception ex) {
            log.error("Failed to report this execution to ExecutionMatrix server." + EOL +
                    "Reason: An unknown error has occurred, Error message: " + ex.getMessage());
        }

        return false;
    }
}
