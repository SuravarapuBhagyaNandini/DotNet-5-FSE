public class Main {
    public static void main(String[] args) {
        DocumentFactory wordFactory = new WordDocumentFactory();
        DocumentFactory pdfFactory = new PdfDocumentFactory();
        DocumentFactory excelFactory = new ExcelDocumentFactory();

        Document doc1 = wordFactory.newDocument("report.docx");
        doc1.open();

        Document doc2 = pdfFactory.newDocument("paper.pdf");
        doc2.open();

        Document doc3 = excelFactory.newDocument("sheet.xlsx");
        doc3.open();
    }
}
