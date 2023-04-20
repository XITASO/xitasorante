# Naming Challenge

Good names are crucial to create an understandable code base. Furthermore, bad names increase the likelihood of bugs [^1]. However, finding good names can be a hard problem. In this challenge, you will suffer from bad names and learn how to increase readability by only finding better names.

## Properties of Good Names 💡

Good names should [^1][^2][^3]:

- Create an image in the mind of the reader
- Provide abstraction
- not contain any redundant extra words, pre- or suffixes
- be used consistently (one name per concept!)
- reflect names in the business domain
- be short, but contain enough information to be precise

## Examples:

| Bad                                          | Better                             | Notes                                                      |
| -------------------------------------------- |------------------------------------| ---------------------------------------------------------- |
| `int d // elapsed time`                      | `int elapsedTimeInDays`            | Reveals the intention and contains the unit               |
| `fileObject`                                 | `file`                             | The term `Object` does not provide any value               |
| `int int_amount`                             | `int amount`                       | Prefix just adds noise and is not relevant with modern IDEs |
| `string greetMessageString = "Hello World";` | `string greeting = "Hello World";` | string is redundant, message does not add any information  |

## Assignment 🎯

In `Infrastructure/Import/SegmentationService.cs` you will find a class which contains an algorithm to get a stream of a fraction of a CSV file. Unfortunately, the author of this class was not aware how to find good names for variables and class-members.

Use the "Rename" refactoring of your IDE and find better names for local variables, class members and method names. Watch how readability of the implementation grows while you improve the naming.

### Hint 🤓
If you want to step through the code, execute the unit tests in `Test/Infrastructure/Import/SegmentationServiceTests.cs` in Debug mode.

## Additional Information 📗
- [^1]: J. Ousterhoud, "Choosing Names," in _A Philosophy of Software Design_, 2nd e.d, Palo Alto, CA: Yaknyam Press, 2021, pp. 121-129
- [^2]: T. Ottinger, "Meaningful Names", in _Clean Code_, R. Martin, Massachusetts: Pearson, 2009, pp. 48-61
- [^3]: "Ubiquitous Language", in _Domain Driven Design_, E. Evans, Massachusetts: Addison-Weseley, pp. 24-30