public class TestFactory {
    public static void main(String[] args) {
        DocumentFactory[] factories = new DocumentFactory[] {
            new WordDocumentFactory(),
            new PdfDocumentFactory(),
            new ExcelDocumentFactory()
        };
        String[] expected = {"Word", "PDF", "Excel"};
        boolean ok = true;
        for (int i = 0; i < factories.length; i++) {
            Document d = factories[i].createDocument();
            System.out.println("Created: " + d.getType());
            if (!d.getType().equals(expected[i])) {
                System.out.println("FAIL: expected " + expected[i] + " got " + d.getType());
                ok = false;
            }
        }
        System.out.println(ok ? "TEST PASSED" : "TEST FAILED");
        if (!ok) System.exit(1);
    }
}
