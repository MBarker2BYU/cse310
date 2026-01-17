# Overview

As a software engineer, I built CodeTime Tracker to create a powerful, personal tool for accurately tracking time spent on coding projects and tasks. The goal was to develop a full-featured desktop application that helps developers stay organized, visualize productivity patterns, and generate meaningful reports — ultimately improving focus and time management in real-world development work.

CodeTime Tracker is a Windows desktop application (built with WinForms) that allows users to:
- Create and manage coding projects
- Define code objects (classes, forms, controls, etc.) within each project
- Start/pause/stop timers for individual tasks
- Add manual time entries when needed
- Edit, soft-delete, and restore entries
- Generate detailed text reports with project breakdowns, code object totals, and a daily summary
- Export data to CSV for further analysis
- Persist all data in a local JSON file

The application emphasizes clean UI design, reliable timing logic, and data integrity — making it a practical daily tool while demonstrating strong C# fundamentals.

[Software Demo Video](https://youtu.be/yilKuINe-dg)

# Development Environment

- **IDE**: Visual Studio 2022 Community Edition (full-featured for WinForms development, debugging, and UI design)
- **Programming Language**: C# (.NET 8 / Windows Forms)
- **Data Storage**: System.Text.Json for serialization/deserialization to local JSON file
- **Target Platform**: Windows desktop (x64)

Additional packages/libraries used:
- WinForms for the graphical user interface
- Standard .NET libraries (System.IO, System.Collections.Generic, System.Linq)

# Useful Websites

* [Microsoft Learn - Windows Forms Documentation](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/) - Official reference for WinForms controls, events, and design patterns
* [Microsoft Learn - System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview) - Guidance on JSON serialization and deserialization in .NET
* [Stack Overflow - Handling TimeSpan across days in C#](https://stackoverflow.com/questions/related-to-timespan-days-rollover) - Helpful threads on accurate duration calculations
* [YouTube - WinForms Tutorial Series](https://www.youtube.com/results?search_query=winforms+c%23+tutorial) - Various practical tutorials for event wiring and UI best practices