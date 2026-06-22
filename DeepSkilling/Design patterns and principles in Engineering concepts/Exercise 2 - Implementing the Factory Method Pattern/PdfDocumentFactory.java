public class PdfDocumentFactory extends DocumentFactory {
    @Override
    public Document createDocument(String name) {
        return new PdfDocument(name);
    }
}
