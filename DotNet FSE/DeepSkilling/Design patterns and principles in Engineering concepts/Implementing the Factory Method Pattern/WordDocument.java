public class WordDocument implements Document {
    private final String content;
    public WordDocument() { this.content = "Word document content"; }
    @Override
    public String getType() { return "Word"; }
    @Override
    public void open() { System.out.println("Opening Word document: " + content); }
}
