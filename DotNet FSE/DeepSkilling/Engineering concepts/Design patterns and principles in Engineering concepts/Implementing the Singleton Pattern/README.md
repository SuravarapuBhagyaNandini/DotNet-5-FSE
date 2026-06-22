# Implementing the Singleton Pattern

Exercise: Implement the Singleton design pattern using the Bill Pugh holder technique.

Files:
- `Logger.java` — thread-safe singleton implementation (Bill Pugh holder).
- `Main.java` — demo runner that obtains the singleton and logs messages.

How to build and run (from this folder):

```powershell
javac Logger.java Main.java
java Main
```

Sample output:

[Logger] Hello from logger1
[Logger] Hello from logger2
Same instance? true

Explanation:
The `Logger` class uses a private static holder class (`Holder`) that
contains the singleton instance. The instance is created when `getInstance()`
is first called, providing lazy initialization with thread-safety
guaranteed by the JVM. Both `logger1` and `logger2` reference the same
object, so `logger1 == logger2` prints `true`.

Notes:
- Run the commands from this directory. Compiled class files are produced
	in the current folder unless you specify `-d`.
- If you want, I can add unit tests or a small build script next.
