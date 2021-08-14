// Thanks to https://stackoverflow.com/a/30665299/1687536

package executionmatrix.junit5.extension.internal.helpers;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.io.PrintStream;
import java.util.Arrays;
import java.util.List;

public class ConsoleOutputCapturer {
    private ByteArrayOutputStream baos;
    private PrintStream previous;
    private boolean capturing;

    public void start() {
        if (capturing) {
            return;
        }

        capturing = true;
        previous = System.out;      
        baos = new ByteArrayOutputStream();

        OutputStream outputStreamCombiner =
                new OutputStreamCombiner(Arrays.asList(previous, baos));
        PrintStream custom = new PrintStream(outputStreamCombiner);

        System.setOut(custom);
    }

    public String stop() {
        if (!capturing) {
            return null;
        }

        System.setOut(previous);

        String capturedValue = baos.toString();             

        baos = null;
        previous = null;
        capturing = false;

        if (capturedValue.isBlank())
            return null;

        return capturedValue;
    }

    private static class OutputStreamCombiner extends OutputStream {
        private final List<OutputStream> outputStreams;

        public OutputStreamCombiner(List<OutputStream> outputStreams) {
            this.outputStreams = outputStreams;
        }

        public void write(int b) throws IOException {
            for (OutputStream os : outputStreams) {
                os.write(b);
            }
        }

        public void flush() throws IOException {
            for (OutputStream os : outputStreams) {
                os.flush();
            }
        }

        public void close() throws IOException {
            for (OutputStream os : outputStreams) {
                os.close();
            }
        }
    }
}