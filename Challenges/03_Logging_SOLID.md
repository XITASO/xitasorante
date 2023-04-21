# Logging SOLID

## Background

In software engineering, SOLID is a mnemonic acronym for five design principles
intended to make object-oriented designs more understandable, flexible, and
maintainable. The principles are a subset of many principles promoted by
American software engineer and instructor Robert C. Martin, first
introduced in his 2000 paper Design Principles and Design Patterns discussing
software rot.

The SOLID ideas are:

- The **Single-responsibility** principle: "There should never be more
  than one reason for a class to change." In other words, every class should
  have only one responsibility.
- The **Open–closed** principle: "Software entities ... should be open for
  extension, but closed for modification."
- The **Liskov substitution** principle: "Functions that use pointers or
  references to base classes must be able to use objects of derived classes
  without knowing it." See also design by contract.
- The **Interface segregation** principle: "Clients should not be forced to
  depend upon interfaces that they do not use."
- The **Dependency inversion** principle: "Depend upon abstractions, not
  concretions."

(Source: [Wikipedia](https://en.wikipedia.org/wiki/SOLID))

## Assignment 🎯

Practice the application of the SOLID-Principles by designing and implementing a
little Logging library.

- Think about the different responsibilities, the library has to fulfill and
  implement them in separate classes.
- Decide where the library should be open for extension and where it should be
  closed for modification.
- Define interfaces that can be integrated with ASP.NET's Dependency 
  Injection so that consumers of the library do not depend on specific 
  implementations.
- (optional) Validate the implementation with unit tests.

### Acceptance Criteria

- The logging library should support various log levels, including debug, info,
  warning, and error.
- The logging library should allow for configuration of a minimum log level, and
  only messages above this level should be written to the log.
- Log messages should automatically include a timestamp indicating when the log
  was created.
- The logging library should provide extensibility for output destinations,
  allowing developers to provide implementations that write to different
  desitnations.

##   