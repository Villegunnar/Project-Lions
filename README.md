# Unit testing of Lion Bank Inc

###### Identifiera 3 delar av koden som du känner är mer affärskritiska (exempelvis överföring av pengar, insättning eller uttag av pengar osv.)
   ###    *  Ta lån 

##### Man kan ta lån med negativt värde, vilket gör att du ger banken pengar istället för att få ett lån. 

 ###  * Ändra bankens rörliga som Admin 

##### Admin kan ändra bankens rörliga ränta till negativa värden. 

 ###  * Föra över pengar till andra användare 

##### Det går att skicka pengar till sig själv vilket är en onödig funktion. 

 
#
#
#





# Lion Bank Inc, kings of the finance jungle 


## Contributors
Erik Risholm: https://github.com/Riceholme 

Viktor Gunnarsson, https://github.com/Villegunnar

Lukas Rose, https://github.com/Luksros

Erik Norell, https://github.com/Erik9507



## NotionBoard
https://www.notion.so/Team-Lion-Bank-Inc-9e63f1a7658646d093e6c2aaa2adf076


## UML-CHART
![Lion Bank Inc  - classes](https://user-images.githubusercontent.com/91311247/146573842-0b9e5a80-72dc-4042-823b-dcadd4036b46.png)






## Description of all the classes

#### 1. __User__: An abstract parent class named "User" made as a blueprint for Customer-class and Admin-class.
#### 2. Admin: A class inhereting User-class with decimal properties and methods
#### 3. UserFactory: The class were made to create a new "Customer" with certain demands for a username and password. This class and its methods can only be accessed from an "Admin".
#### 4. Customer: A class inhereting User-class and contains most of the structure including methods that defines what the customer needs and uses at the bank.
#### 5. Account: A class that contains properties for the list with "Account" in Customer-class. Properties such as name, balance, currency and interest.
#### 6. BankSystem: A class that contains the majority of methods that runs the "system", mainly "menus" and constructor that holds all the users (Customer and Admin). Basically the whole program runs from this class. It contains a list with "Customer" and a list with "Admins".
#### 7. Program: The already existing class with the "Main-method" to initiate and run our system.

