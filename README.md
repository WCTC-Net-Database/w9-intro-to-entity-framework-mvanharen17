### Assignment: Entity Framework Core Introduction with Console RPG

**Objective:**  
In this assignment, you will be introduced to Entity Framework (EF) Core and use it to manage your `Room` and `Character` entities within the Console RPG project. You will learn how to set up the Entity Framework context, work with connection strings, generate migrations, and update the database. Additionally, you will be required to implement functionality to add and find characters using the `GameEngine` and `Menu` classes.

---

### **Instructions:**

#### Step 1: Setup Entity Framework Core

Ensure your project references EF Core by installing the necessary packages. Use the following commands in the **Package Manager Console** or the **.NET CLI**:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

#### Step 2: Update `GameContext.cs`

Modify the `GameContext` class to include the correct EF Core connection string and define the `DbSet` properties for your `Room` and `Character` entities.

```csharp
using Microsoft.EntityFrameworkCore;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Character> Characters { get; set; }

    // Override OnConfiguring to set up the connection string for the SQL Server
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Replace with your actual connection string from https://www.connectionstrings.com/sql-server/
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}
```

You can find the correct connection string format on [Connection Strings](https://www.connectionstrings.com/sql-server/). Choose the connection string that corresponds to your SQL Server setup.

#### Step 3: Generate Migrations

**Verify Installation**

To verify that the correct packages have been installed, execute the following command in the terminal:
```bash
dotnet list package
```

This will list all installed packages, including `Microsoft.EntityFrameworkCore.SqlServer` and `Microsoft.EntityFrameworkCore.Tools`.

---

**Verify EF Core CLI Tools**

Ensure you have installed the EF Core tools globally:

```bash
dotnet tool install --global dotnet-ef
```

**Verify the installation by running:**

```bash
dotnet ef
```

If correctly installed, this will show the EF Core tool commands.

---

**Generate Migrations**

Once everything is set up, generate migrations by running:

```bash
dotnet ef migrations add InitialCreate
```

This creates the first migration file in the project and prepares the database schema for use.

#### Step 4: Update the Database

After generating the migration, apply the changes to the database by running:

```bash
dotnet ef database update
```

This will create the `Rooms` and `Characters` tables in your SQL Server database.

#### Step 5: Verify the Template Is Running

Before proceeding to the next steps, verify that your base assignment template is working correctly by running the application. It should not throw any errors, and you should be able to interact with the `GameContext` for simple read/write operations.

---

### **Modification Requirements:**

#### 1. Add a Room

Update your application to support adding a new room to the game. This functionality will reside in the `Menu` and `GameEngine` classes.

In the `Menu` class, add a menu option to **Add a Room**:

```csharp
Console.WriteLine("Add a Room");
```

In the `GameEngine`, create the logic to prompt the user for the room’s details and add it to the context:

```csharp
public void AddRoom()
{
    Console.Write("Enter room name: ");
    var name = Console.ReadLine();

    Console.Write("Enter room description: ");
    var description = Console.ReadLine();

    var room = new Room
    {
        Name = name,
        Description = description
    };

    _context.Rooms.Add(room);
    _context.SaveChanges();

    Console.WriteLine($"Room '{name}' added to the game.");
}
```

This will allow you to add rooms dynamically to the game and store them in the database.

#### 2. Add a Character

Update your application to support adding a new character to a room. This functionality will reside in the `Menu` and `GameEngine` classes.

In the `Menu` class, add a menu option to **Add a Character**:

```csharp
Console.WriteLine("Add a Character");
```

In the `GameEngine`, create the logic to prompt the user for the character’s details and add it to a room:

```csharp
public void AddCharacter()
{
    Console.Write("Enter character name: ");
    var name = Console.ReadLine();

    Console.Write("Enter character level: ");
    var level = int.Parse(Console.ReadLine());

    Console.Write("Enter room ID for the character: ");
    var roomId = int.Parse(Console.ReadLine());

    // TODO Add character to the room
    // Find the room by ID
    // If the room doesn't exist, return
    // Otherwise, create a new character and add it to the room
    // Save the changes to the database
}
```

#### 3. Find a Character

Add another option to **Find a Character** in the `Menu` class:

```csharp
Console.WriteLine("Find a Character");
```

In the `GameEngine`, implement logic to search for a character by name:

```csharp
public void FindCharacter()
{
    Console.Write("Enter character name to search: ");
    var name = Console.ReadLine();

    // TODO Find the character by name
    // Use LINQ to query the database for the character
    // If the character exists, display the character's information
    // Otherwise, display a message indicating the character was not found
}
```

---

### **Grading Rubric (100 points total)**

- **0%** if the program doesn’t compile or run.
- **20%** if the program compiles and runs but lacks significant functionality.
- **40%** if the program compiles, runs, and partially meets the requirements but has missing or incorrect logic.
- **60%** if the program compiles, runs, and meets the base requirements:
  - Menu options are available for adding a room and adding a character.
  - The database has been seeded correctly.
  - Characters and rooms can be added and saved to the database.
- **80%** if the program compiles, runs, and meets all requirements, including:
  - Characters can be correctly found and displayed based on user input.
  - Connection string is properly configured.
  - Migrations have been successfully generated and applied.
  - Base program functionality is tested, and no significant bugs exist.
- **100%** if the program compiles, runs, and meets all requirements, with:
  - Clear, concise code and appropriate use of comments.
  - Well-structured, clean code adhering to best practices, including using Entity Framework Core effectively.

### **Stretch Goal (Optional, +10%):**

Implement functionality to **Update a Character’s Level** by adding another option in the `Menu`. This should allow the user to increase or decrease the level of a specific character by searching for them in the database and then updating the corresponding value.

---

This assignment will help you build foundational skills with Entity Framework Core and ensure that you understand basic operations like adding, finding, and updating records in a database. Good luck!