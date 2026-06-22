# Implementing the Factory Method Pattern

Exercise: Implement the Factory Method design pattern.

Files:
- `Document.java` — document interface.
- `WordDocument.java`, `PdfDocument.java`, `ExcelDocument.java` — concrete documents.
- `DocumentFactory.java` — abstract factory.
- `WordDocumentFactory.java`, `PdfDocumentFactory.java`, `ExcelDocumentFactory.java` — concrete factories.
- `Main.java` — demo runner.

How to build and run (from this folder):

```powershell
javac *.java
java Main
```

Sample output:

Created document type: Word
Opening Word document: Word document content
Created document type: PDF
Opening PDF document: PDF document content
Created document type: Excel
Opening Excel document: Excel document content

Explanation:
This example uses an abstract `DocumentFactory` with concrete factory
implementations (`WordDocumentFactory`, `PdfDocumentFactory`,
`ExcelDocumentFactory`). Each concrete factory implements `createDocument()`
to return a concrete `Document` (`WordDocument`, `PdfDocument`,
`ExcelDocument`). The client (`Main`) works with the abstract factory to
create documents without depending on concrete classes, following the
Factory Method pattern.

Notes:
- This pattern helps when you need to add new document types with minimal changes to client code.
- I can add unit tests or a simple build script if you want.
