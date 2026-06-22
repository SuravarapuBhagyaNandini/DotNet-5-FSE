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
java TestFactory
```

Sample test output (from running `TestFactory`):

Created: Word
Created: PDF
Created: Excel
TEST PASSED

Explanation:
The test harness (`TestFactory`) uses each concrete `DocumentFactory` to
create a `Document` and verifies the returned type matches expectations.
If all created types match, the harness prints `TEST PASSED`.

Notes:
- The `Main` class demonstrates the Factory Method pattern by creating and
	opening each document type.
- If you want I can also add `build.bat` to automate compilation and tests.
