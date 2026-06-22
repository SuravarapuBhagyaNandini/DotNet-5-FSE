public class ExcelDocumentFactory extends DocumentFactory {
    @Override
    public Document createDocument(String name) {
        return new ExcelDocument(name);
    }
}
