# Overview

As a software engineer, I developed AmmoTracker to advance my skills in building robust, data-driven desktop applications with relational databases. The primary goal was to create a complete system that handles secure, parameterized SQL operations, multi-table relationships, dynamic joins, aggregates, and real-time data presentation in a practical UI — all while producing a tool I could actually use for personal inventory management.

AmmoTracker is a Windows Forms desktop application for tracking ammunition stock. It uses a local SQLite database to store ammo types (manufacturer, caliber, grain) and purchase records (date, rounds added, cost per round, optional lot number). The main grid shows aggregated totals per ammo type with low-stock status based on user-defined minimum thresholds. A secondary history grid displays individual purchases for the selected type, with full support to add, edit, or delete entries. Filters allow narrowing the view by manufacturer, caliber, grain, purchase date range, or lot number.

To use the program: launch the app (database auto-creates on first run), select or add ammo types via dropdowns, log purchases with details, view totals and alerts in the main grid, double-click a row to see purchase history, edit thresholds or delete purchases as needed, and apply filters to focus on specific criteria.

The purpose of writing this software was to master relational data modeling and dynamic SQL in C#, while delivering a functional inventory tracker that helps maintain stock levels, track costs, and avoid shortages — skills directly transferable to real-world data applications.

[Software Demo Video](https://youtu.be/YOUR_VIDEO_ID_HERE)

# Relational Database

I am using **SQLite**, a lightweight, serverless, file-based relational database engine that requires no installation or configuration and is ideal for desktop applications.

The database (AmmoTracker.db) contains five related tables:

- **Manufacturers**  
  ManufacturerID INTEGER PRIMARY KEY AUTOINCREMENT  
  ManufacturerName TEXT NOT NULL UNIQUE

- **Calibers**  
  CaliberID INTEGER PRIMARY KEY AUTOINCREMENT  
  CaliberName TEXT NOT NULL UNIQUE

- **Grains**  
  GrainID INTEGER PRIMARY KEY AUTOINCREMENT  
  GrainValue TEXT NOT NULL UNIQUE

- **AmmoTypes** (junction table)  
  TypeID INTEGER PRIMARY KEY AUTOINCREMENT  
  ManufacturerID INTEGER NOT NULL  
  CaliberID INTEGER NOT NULL  
  GrainID INTEGER NOT NULL  
  MinimumThreshold INTEGER DEFAULT 0 NOT NULL  
  FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)  
  FOREIGN KEY (CaliberID) REFERENCES Calibers(CaliberID)  
  FOREIGN KEY (GrainID) REFERENCES Grains(GrainID)  
  UNIQUE(ManufacturerID, CaliberID, GrainID)

- **Purchases**  
  PurchaseID INTEGER PRIMARY KEY AUTOINCREMENT  
  TypeID INTEGER NOT NULL  
  PurchaseDate TEXT NOT NULL  
  RoundsAdded INTEGER NOT NULL CHECK(RoundsAdded > 0)  
  RoundsPerContainer INTEGER NOT NULL CHECK(RoundsPerContainer > 0)  
  Containers INTEGER NOT NULL DEFAULT 1  
  LotNumber TEXT  
  CostPerRound REAL NOT NULL CHECK(CostPerRound >= 0)  
  FOREIGN KEY (TypeID) REFERENCES AmmoTypes(TypeID)

Tables are linked via foreign keys, enabling JOIN queries to display combined details (e.g., full ammo type info alongside purchase records). On first run, the application automatically creates all tables and an index on Purchases.TypeID for faster queries.

# Development Environment

- Visual Studio 2026 Professional Edition  
- C# with Windows Forms (.NET 8)  
- Microsoft.Data.Sqlite NuGet package for all SQLite database interactions

# Useful Websites

- [Microsoft.Data.Sqlite Documentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/)  
- [SQLite Official Documentation](https://www.sqlite.org/docs.html)  
- [Parameterized Queries in SQLite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/parameters)  
- [SQL JOIN Types Explained](https://www.w3schools.com/sql/sql_join.asp)  
- [SQLite Aggregate Functions](https://www.sqlite.org/lang_aggfunc.html)  
- [DataGridView Control in Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/datagridview-control-windows-forms)

# Future Work

- Implement live filtering on combo change (no Apply button needed)  
- Add total value per type in purchase history grid  
- Include CSV export of current inventory and purchase history  
- Add color-coding in grids (e.g., red for low-stock rows)  
- Improve validation messages and tooltips for better usability  