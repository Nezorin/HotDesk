# HotDesk
_Desk booking app made with ASP.NET and C#_
## Architecture
The solution consists of three main parts:

* ```DataAccessLayer``` Defines main entities that are used in whole solution:  ```Desk```,  ```Device```,  ```User```, etc. Also implements the repository pattern, all methods were made generic, and async if possible. The repository defines access to EF core class ```DbContext```. The context also seeds some default data to database like default users and devices.


* ```BusinessLogic``` Contains all services that are implement main logic of the application. Most services implement default CRUD operations like create user, delete device, update desk and so on, but also they contain some useful business logic. For example ```BookingService``` sorts available dates for reservation.


* ```HotDesk``` default MVC project, that contains controllers which mostly work with services, ViewModels to transfer data to view and back, and views. 
  * ```AccountController``` Contains authorization and registration logic. Authorization was made without any tokens or ASPIdentity, just cookie.
  * ```DeskController``` Mainly consists of admin panel logic: desk management functions(delete desk, add desk, edit desk). Also shows users the list of available desks
  * ```ReservationController``` Have method to show all reservation that were made, and contains logic to book desk which is closely related to ```BookingService```
  * ```DeviceController``` Created so that the admin can create and delete devices.

View styles(navbar, tables) were made using Bootstrap 4.


Technologies:
 - **C#**
 - **ASP.NET MVC**
 - **Entity Framework Core**
 - **PostgreSQL**
 - **xUnit**
 - **Moq**
 - **MockQueryable**

## User Guide
#### Installation
You can manually download the project as ZIP archive: Code → Download ZIP. Or, if you have git installed, you can use this command:
```git clone https://github.com/Nezorin/HotDesk.git ```

Also, you should set the DB connection string in ```appsetting.json```

To run the project you can use your IDE or dotnet CLI. If you use Visual Studio: open the solution file, build the solution(Ctrl+B) and run the code(Ctrl+F5), preferably use Kestrel run profile. Or open Command Prompt(Console) and use these commands:
```
cd "PATH TO PROJECT FOLDER" 
dotnet build
dotnet run
```
#### Usage
Admin panel GIF demonstration:

![ezgif-2-c2af3118183f](https://user-images.githubusercontent.com/47496652/135452161-49ea737f-6917-4c0f-bb54-ecbf77987501.gif)

Desk reservation GIF demonstration:

![ezgif-2-579f3542c199](https://user-images.githubusercontent.com/47496652/135452688-d45e387a-bbfd-46d5-89f4-58ac15762086.gif)

Reservations Table:

![image](https://user-images.githubusercontent.com/47496652/135453436-7ce06170-e775-4845-943d-ef655806b671.png)
