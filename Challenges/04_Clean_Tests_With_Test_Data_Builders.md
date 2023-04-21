## Assignment 🎯: Improving Test Readability with Test Data Builders

In this task, you will be working on improving the readability of unit tests by
implementing Test Data Builders, a design pattern that helps create test data in
a more concise and readable way. You will start by reviewing the existing test
data builders located under `Test.TestDataBuilders`, and then implement any
missing test data builders. Next, you will assemble more complex objects by
combining the test data builders. Finally, you will refactor the `OrderTests`
and
the `OrderProcessorTests` using the new test data builders.

## Tasks ✅

1. Review the existing test data builders located under `Test.TestDataBuilders`.
   Familiarize yourself with the current implementation and understand how they
   are used to create test data.

2. Implement any missing test data builders that are required for the
   `OrderTests` and the `OrderProcessorTests`. Follow the pattern of the
   existing test data builders and ensure that the builders provide fluent and
   expressive methods for building test objects with meaningful names.

3. Assemble more complex objects by combining the test data builders. For
   example, you can use the test data builders for `Dish` and `OrderPosition` to
   create an `Order` object with multiple dishes and order positions. Aim to
   make the test data creation code more concise and readable by leveraging the
   builders.

4. Refactor the `OrderTests` and the `OrderProcessorTests` using the new test
   data builders. Replace any long and repetitive setup code with calls to the
   test data builders to create the necessary test data. Ensure that the tests
   remain functionally equivalent and maintain their original assertions.

5. Review and validate your changes. Double-check that the tests still pass and
   the test data creation code is now more readable and maintainable with the
   use of Test Data Builders.

6. Document your changes and be prepared to present your improvements to the
   group, explaining how the use of Test Data Builders has improved the
   readability of the tests.

7. Bonus: If time permits, consider implementing Test Data Builders for other
   tests in the project, or explore other ways to further improve the
   readability of the test code using best practices and coding standards.

8. Bonus 2: Get familiar with the
   [Bogus fake data generator](https://github.com/bchavez/Bogus) and integrate
   it into your test data
   builders so that
   they generate their data using the fake data generator.

Note: Feel free to collaborate with your peers and seek assistance from the
facilitator if you encounter any issues or have questions during the task.
Remember to follow coding best practices and maintain code quality throughout
the implementation.

## Hint 🤓
If you don't remember the test data builder pattern, here are two blog 
articles helping you:
- [How to create test data with the Builder pattern by Cesar Aguirre]
  (https://canro91.github.io/2021/04/26/CreateTestValuesWithBuilders/)
- [Test Data Builders in C# by Mark Seemann](https://blog.ploeh.dk/2017/08/15/test-data-builders-in-c/)