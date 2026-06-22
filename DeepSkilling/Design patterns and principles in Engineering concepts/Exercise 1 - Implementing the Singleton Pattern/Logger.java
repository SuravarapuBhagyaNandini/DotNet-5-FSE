public class Logger {
    private Logger() { }

    private static class Holder {
        static final Logger INSTANCE = new Logger();
    }

    public static Logger getInstance() {
        return Holder.INSTANCE;
    }

    public void log(String message) {
        System.out.println("[" + java.time.LocalDateTime.now() + "] " + message);
    }
}
