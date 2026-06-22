Exercise 2 - Implementing the Factory Method Pattern

This exercise demonstrates the Factory Method design pattern for creating different document types.

Files:
- `Document.java` - abstract product
- `WordDocument.java`, `PdfDocument.java`, `ExcelDocument.java` - concrete products
- `DocumentFactory.java` - abstract creator
- `WordDocumentFactory.java`, `PdfDocumentFactory.java`, `ExcelDocumentFactory.java` - concrete creators
- `Main.java` - test harness demonstrating usage

Compile and run (from project root):

```bash
javac -d out "Exercise 2 - Implementing the Factory Method Pattern"/*.java
java -cp out Main
```

Expected output:
- Created document: WordDocument
- Opening Word document: report.docx
- Created document: PdfDocument
- Opening PDF document: paper.pdf
- Created document: ExcelDocument
- Opening Excel document: sheet.xlsx

Notes:
- The `DocumentFactory` is the abstract creator that defines the factory method `createDocument(String)` which concrete creators implement.
