public class ExcelDocument extends Document {
    public ExcelDocument(String name) {
        super(name);
    }

    @Override
    public void open() {
        System.out.println("Opening Excel document: " + name);
    }
}
