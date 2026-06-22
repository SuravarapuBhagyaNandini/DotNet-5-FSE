public abstract class Document {
    protected String name;

    public Document(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public abstract void open();
}
