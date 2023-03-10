## Clay.BE.Assignment.OfficeAccess
An access management API that grants or denies access to users on one or more doors in an office based on their access level.
API also maintains history of door access attempts by users, whether the access was granted or denied.
Certain users have the authority to view door access history.

## Tools and technologies used
1. .NET 6
2. Entity Framework Core 6.0 (Code first)
3. C#
4. Sql Server 2016
5. xUnit
6. Moq

## Project dependency diagram
- Project structure has been setup keeping layered architecture in mind.
- Ensuring separation of concerns in each layer to ensure maintainability of the project if new features are incorporated.

![Project_Architecture](https://user-images.githubusercontent.com/16301198/209124032-4f76aeb4-eed1-4a07-9477-eb25d1eb20cd.png)

## Database diagram
- Database has been designed keeping scalability in mind.
- Selected design choice allows system to have many offices, each office can have many doors where each door has its own access level.
- Similarly, there can be many users, each user is assigned one or more access levels, therefore, user can access multiple doors.

![Database_Diagram](https://user-images.githubusercontent.com/16301198/209124134-f901a934-60f4-467f-a2d4-1d4c8ef24901.png)

##### Note: The intial db creation migration already has seed data. Please ensure to add connection string to database in user secrets or appsettings. Example connection string: "Server=NB-RAAH\\SQLEXPRESS;Database=OfficeAccessDb;Integrated Security=True;"

![image](https://user-images.githubusercontent.com/16301198/211291562-f305bb71-6d64-4f79-aac8-17baddf12e7e.png)

#### Initial seed data has already been added with initial migration to test out application.
![image](https://user-images.githubusercontent.com/16301198/211483573-072c04d4-498c-406a-b8fa-85b4b3a824b0.png)

