# Overview

As a software engineer, I developed SwarNet (Sea Warfare Network) to master real-world networking concepts, particularly how modern multiplayer applications handle automatic discovery and reliable communication in local network environments.

SwarNet is a turn-based, two-player sea warfare game (inspired by classic Battleship) that enables players on the same local network to automatically discover available games and compete against each other with reliable gameplay.

The project demonstrates:
- Automatic LAN host discovery using UDP broadcast (no manual IP address entry required)
- Reliable, ordered, and guaranteed message delivery using TCP sockets
- Clean client-server architecture with a structured message protocol
- Thread-safe Windows Forms user interface updates
- Robust connection management and error handling

My primary purpose in building this software was to gain deep, practical experience with socket programming in C#, understand the trade-offs between UDP and TCP, and create a complete, professional-looking application that I can proudly include in my software engineering portfolio.

[Software Demo Video](http://youtube.link.goes.here) - Coming Soon

# Development Environment

- **Operating System**: Windows 11  
- **IDE**: Visual Studio 2022 (Community Edition)  
- **Programming Language**: C# (.NET 8.0 – Windows Forms Application)  
- **Networking Libraries**: System.Net.Sockets (TcpListener, TcpClient, UdpClient)  
- **UI Framework**: Windows Forms (WinForms)  
- **Version Control**: Git + GitHub  
- **Testing Environment**: Multiple application instances on the same machine (localhost) and across local network devices  

# Useful Websites

* [Microsoft Docs – TcpListener Class](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener)  
* [Microsoft Docs – UdpClient Class](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient)  
* [Making Thread-Safe Calls to Windows Forms Controls](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-make-thread-safe-calls)  
* [C# Network Programming Best Practices](https://learn.microsoft.com/en-us/dotnet/framework/network-programming/)  
* [Battleship Game Rules & Strategy](https://en.wikipedia.org/wiki/Battleship_(game))  

SwarNet – Sea Warfare Network  
by ShadowWorx Systems  
🚢🌊