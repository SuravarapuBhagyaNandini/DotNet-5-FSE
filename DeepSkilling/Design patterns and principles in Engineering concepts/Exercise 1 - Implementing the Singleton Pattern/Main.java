public class Main {
    public static void main(String[] args) {
        Logger a = Logger.getInstance();
        Logger b = Logger.getInstance();

        a.log("Logger A instance hash: " + System.identityHashCode(a));
        b.log("Logger B instance hash: " + System.identityHashCode(b));

        System.out.println("Same instance? " + (a == b));
    }
}
