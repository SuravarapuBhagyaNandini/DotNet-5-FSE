public class Product implements Comparable<Product> {
    private final int productId;
    private final String productName;
    private final String category;

    public Product(int productId, String productName, String category) {
        this.productId = productId;
        this.productName = productName;
        this.category = category;
    }

    public int getProductId() { return productId; }
    public String getProductName() { return productName; }
    public String getCategory() { return category; }

    @Override
    public String toString() {
        return String.format("Product{id=%d, name='%s', category='%s'}", productId, productName, category);
    }

    @Override
    public int compareTo(Product other) {
        return this.productName.compareToIgnoreCase(other.productName);
    }
}
