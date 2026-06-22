public class WordDocumentFactory extends DocumentFactory {
    @Override
    public Document createDocument(String name) {
        return new WordDocument(name);
    }
}
