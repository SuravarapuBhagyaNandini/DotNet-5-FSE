public class PdfDocument implements Document {
    private final String content;
    public PdfDocument() { this.content = "PDF document content"; }
    @Override
    public String getType() { return "PDF"; }
    @Override
    public void open() { System.out.println("Opening PDF document: " + content); }
}
