public class Main {
    public static void main(String[] args) {
        DocumentFactory[] factories = new DocumentFactory[] {
            new WordDocumentFactory(),
            new PdfDocumentFactory(),
            new ExcelDocumentFactory()
        };

        for (DocumentFactory factory : factories) {
            Document doc = factory.createDocument();
            System.out.println("Created document type: " + doc.getType());
            doc.open();
        }
    }
}
