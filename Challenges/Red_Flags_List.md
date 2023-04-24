# Red Flags

from J. Ousterhoud, "A Philosophy of Software Design", 2nd e.d, Palo Alto, CA: Yaknyam Press, 2021

## Shallow Module
The interface for a class or method isn't much simpler than its implementation.

<details>
<summary>Example</summary>

```cs
public interface ILinkedList<T> {
    void Add(T item);
    void Remove(T item);
}

public class LinkedList<T> : ILinkedList<T> {
    public void Add(T item) {
        //...
    }
    public void Remove(T item) {
        //...
    }
}
```
</details>

## Information Leakage
A design decision is reflected in multiple modules.

<details>
<summary>Example</summary>

```cs
public class CsvReader {
    public MyDataClass Read() {
        // read column n to member m
    }
}

public class CsvWriter {
    public void Write(MyDataClass content) {
        // write member m to column n
    }
}

public static void Main(string[] args) {
    string content = new CsvReader().Read();
    // ToDo change content
    new CsvWriter().Write(content);
}
```
</details>

## Temporal Decomposition (a form of information leakage)
The code structure is based on the order in which operations are executed, not on information hiding.

> Read HTTP request ➡ Parse HTTP request <br>
> ⚡ To read it correctly you have to, at least partially, parse it.


## Overexposure
An API forces callers to be aware of rarely used features in order to use commonly used features.

<details>
<summary>Example</summary>
In Java, Streams and Readers are un-buffered by default while in 95% of all cases you want to buffer the stream. So you have to keep in mind to wrap int in a buffered reader in order to avoid performance issues:

```java
    public String readFile(File input) throws FileNotFoundException, IOException {
        try (var reader = new FileReader(input)) {
            try (var bufferedReader = new BufferedReader(reader)) {
                StringBuilder content = new StringBuilder();
                String line;

                while ((line = bufferedReader.readLine()) != null) {
                    content.append(line);
                    content.append(System.lineSeparator());
                }

                return content.toString();
            }
        }
    }
```

In .NET, all streams are buffered by default. Also it contains useful helper
methods implementing standard use cases (like `OpenText()` for reading text
files):

```cs
    public string ReadFile(FileInfo input)
    {
        using var reader = input.OpenText();
        return reader.ReadToEnd();
    }
```
</details>

> Implement good defaults for parameters! <br>
> Classes or methods should do "the right thing" by default.

## Pass-Through Method
A pass-through method is one that does nothing except pass its arguments to another method, usually with the same API as the pass-through method. This typically indicates that there is not a clean division of responsibility between the classes.

<details>
<summary>Example</summary>

```cs
public class TranslationService {

    public string GetTranslation (string key) {
        return translator.getTranslation(key);
    }
}
```
</details>

## Repetition
If the same piece of code (or code that is almost the same) appears over and over again, that's a red flag that you haven't found the right abstractions.

> Extract code to methods or modules.

## Special-General Mixture
This red flag occurs when a general-purpose mechanism also contains code specialized for a particular use of that mechanism. This makes the mechanism more complicated and creates information leakage between the mechanism and the particular use case: future modifications to the use case are likely to require changes to the underlying mechanism as well.

<details>
<summary>Example</summary>
Here, a special `NetWorkErrorLogger` has been introduced to log error messages on an RPC-connection. This special logger does not provide any value but complicates logging. It's also a good example for the red flag "Shallow Module".

```cs
public static class NetworkErrorLogger {

    public static void LogRpcOpenError (RpcRequest req, Exception e) {
        logger.log(Level.WARNING, "Error opening a rpc connection. Message: " + e);
    }

    public static void LogRpcCloseError (RpcRequest req, Exception e) {
        logger.log(Level.ERROR, "Error closing a rpc connection. Message: " + e);
    }
    //...
}
```
</details>

> You have to look at the code, to find out what is actually happening and if it's correct. <br>
>


## Conjoined Method
It should be possible to understand each method independently. If you can't understand the implementation of one method without also understanding the implementation of another, that's a red flag. This red flag can occur in other contexts as well: if two pieces of code are physically separated, but each can only be understood by looking at the other, that is a red flag.

<details>
<summary>Example</summary>

```cs
public class CarwashTicketing {

    public Set<string> GetOnetimeCodes (Request req) {
        if (!Validate(req)) {
            throw Error("Not allowed");
        }
        FetchResult result = FetchOnetimeCodes(req.Data);
        if (result.Length() == 0) {
            throw Error("Could not create tickets");
        }
        return result.GetTickets();
    }

    private FetchResult FetchOnetimeCodes(RequestData data) {
        CheckExistingData();
        CreateTickets();
        //...
        return result;
    }
}
```
</details>

## Comment Repeats Code
All of the information in a comment is immediately obvious from the code next to the comment.

<details>
<summary>Example</summary>

```cs
/*
 * Obtain a normalized resource name from REQ.
 */
private static string[] GetNormalizedResourceName(HTTPRequest req) {
    // Return if Request was null
    if (req == null) {
        return
    }
    // ...
}
```
</details>

## Implementation Documentation Contaminates Interface
An interface comment describes implementation details not needed by users of the thing being documented.

<details>
<summary>Example</summary>

```cs
    public interface TaskRepository
    {
        // Does a SELECT * FROM Tasks WHERE ID = {id} query
        Task FindById(Guid id);

        // Deletes a task from the Azure SQL-DB
        void Delete(Task toDelete);
    }
```
</details>

> Do not overload the user of the api with unnecessary information.


## Vague Name
The name of a variable or method is so imprecise that it doesn't convey much useful information and may lead to misuse.

<details>
<summary>Example</summary>

```cs
public class ProjectManager {

    public int GetCount() {
        //...
    }
}

public class BetterProjectManager {

    public int GetNumberOfProjects() {
        //...
    }
}
```
</details>


## Hard to Pick Name
It is difficult to come up with a precise and intuitive name for a variable or method.

> Think about if this method or variable really does the right thing.

## Hard to Describe
The comment that describes a method or variable should be simple and yet complete.

> If you find it difficult to write such a comment, something with your method or variable is wrong.

## Nonobvious Code
The behavior or meaning of a piece of code cannot be understood easily.

<details>
<summary>Example</summary>

```cs
public static void Main(String[] args) {
    //...
    new RaftClient(myAddress, serverAddresses);
}
```

<details>
<summary>Details</summary>

```cs
public class RaftClient {
    public RaftClient(string address, string[] serverAddresses) {
        // to keep the application running
        Thread.createThread();
    }
}
```
</details>

</details>

> Software should be designed for ease of reading, not ease of writing!