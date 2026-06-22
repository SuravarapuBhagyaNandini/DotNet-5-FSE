public class WordDocument extends Document {
    public WordDocument(String name) {
        super(name);
    }

    @Override
    public void open() {
        System.out.println("Opening Word document: " + name);
    }
}
