public class PdfDocument extends Document {
    public PdfDocument(String name) {
        super(name);
    }

    @Override
    public void open() {
        System.out.println("Opening PDF document: " + name);
    }
}
