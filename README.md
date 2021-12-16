Link to board: https://www.notion.so/Team-Lion-Bank-Inc-9e63f1a7658646d093e6c2aaa2adf076

link to UML diagram: https://lucid.app/lucidchart/3d043684-a710-4f1c-a8f6-24bd73a9f82f/edit?invitationId=inv_3cc35165-fbb8-42c6-8295-dd8b3913eb91&page=0_0#

Readme File to explain the basic structure of the syntax. And also grov explain the different functions of the program, classes, methods etc.

-Description of all the classes

1. User: An abstract parent class named "User" made as a blueprint for Customer-class and Admin-class.
2. Admin: A class inhereting User-class with decimal properties and methods
3. UserFactory: The class were made to create a new "Customer" with certain demands for a username and password. This class and its methods can only be accessed from an "Admin".
4. Customer: A class inhereting User-class and contains most of the structure including methods that defines what the customer needs and uses at the bank.
5. Account: A class that contains properties for the list with "Account" in Customer-class. Properties such as name, balance, currency and interest.
6. BankSystem: A class that contains the majority of methods that runs the "system", mainly "menus" and constructor that holds all the users (Customer and Admin). Basically the whole program runs from this class. It contains a list with "Customer" and a list with "Admins".
7. Program: The already existing class with the "Main-method" to initiate and run our system.
