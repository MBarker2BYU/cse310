# Overview

As a software engineer focused on building practical, data-driven tools, I developed AmmoTracker to deepen my expertise in relational database integration, parameterized querying, and desktop application development. This project allowed me to practice designing efficient data models, implementing secure CRUD operations, and creating responsive user interfaces that interact meaningfully with stored data.

AmmoTracker is a Windows desktop application for tracking ammunition inventory. It uses a local SQLite relational database to store ammunition types (caliber, grain, manufacturer) and individual lots (purchase date, rounds quantity, cost). Users interact via a simple WinForms interface: select an ammo type from a dropdown, enter lot details, add/update/delete entries with buttons, view a joined list in a DataGridView (showing combined type and lot information), filter results dynamically by searching caliber/manufacturer, and see real-time summaries including total rounds and low-stock alerts. All database operations use parameterized SQL commands for safety and performance.

The purpose of writing this software was to master building dynamic SQL queries in C#, handling relational data with joins and aggregates, and delivering a functional inventory tool that provides immediate value for managing stock levels and preventing shortages—skills directly applicable to real-world data-centric applications.

[Software Demo Video](http://youtube.link.goes.here)

# Relational Database

I am using **SQLite**, a lightweight, serverless, file-based relational database engine that requires no installation or configuration and is ideal for desktop applications.

The database (AmmoTracker.db) contains two related tables:

- **AmmoTypes**  
  - TypeID INTEGER PRIMARY KEY AUTOINCREMENT  
  - Caliber TEXT NOT NULL  
  - Grain REAL  
  - Manufacturer TEXT  

- **Lots**  
  - LotID INTEGER PRIMARY KEY AUTOINCREMENT  
  - TypeID INTEGER NOT NULL (FOREIGN KEY REFERENCES AmmoTypes(TypeID))  
  - PurchaseDate TEXT  
  - Rounds INTEGER NOT NULL  
  - CostPerRound REAL  

The tables are linked via the TypeID foreign key, allowing JOIN queries to display combined details (e.g., caliber and manufacturer alongside lot-specific data). On first run, the application automatically creates the tables and seeds a few example ammo types if the database is empty.

# Development Environment

- Tools: Visual Studio 2022 (for WinForms designer, debugging, and NuGet package management), Git for version control, YouTube Studio for recording and hosting the demo video  
- Programming language: C# (.NET 8 or .NET 6 Windows Forms App), with the Microsoft.Data.Sqlite NuGet package for all database interactions

# Useful Websites

- [Microsoft.Data.Sqlite Overview and Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite)
- [Parameters in Microsoft.Data.Sqlite (best practices for safe queries)](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/parameters)
- [DataGridView Control in Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/datagridview-control-windows-forms)
- [SQLite Official Documentation](https://www.sqlite.org/docs.html)

# Future Work

- Add full update functionality for existing lots (currently delete is implemented; update needs form fields pre-filled from selected row)
- Implement date-based filtering (e.g., show lots purchased in last 30 days) using date range controls and BETWEEN queries
- Add export/import features (CSV backup/restore of inventory data)
- Improve UI polish: add tooltips, better validation messages, and color-coding for low-stock items in the grid
- Include user authentication or multiple user profiles for shared use scenarios