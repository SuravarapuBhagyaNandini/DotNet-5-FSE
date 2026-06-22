public class ExcelDocument implements Document {
    private final String content;
    public ExcelDocument() { this.content = "Excel document content"; }
    @Override
    public String getType() { return "Excel"; }
    @Override
    public void open() { System.out.println("Opening Excel document: " + content); }
}
