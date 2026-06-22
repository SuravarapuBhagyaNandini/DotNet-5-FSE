public abstract class DocumentFactory {
    public abstract Document createDocument(String name);

    // helper method demonstrating usage
    public Document newDocument(String name) {
        Document d = createDocument(name);
        System.out.println("Created document: " + d.getClass().getSimpleName());
        return d;
    }
}
