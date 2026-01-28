# Overview

As a software engineer, I developed SwarNet (Sea Warfare Network) to master real-world networking concepts, particularly how modern multiplayer applications handle automatic discovery and reliable communication in local network environments.

SwarNet is a turn-based, two-player sea warfare game (inspired by classic Battleship) that enables players on the same local network to automatically discover available games and compete against each other with reliable gameplay.

The project demonstrates:

* Automatic LAN host discovery using UDP broadcast (no manual IP address entry required)
* Reliable, ordered, and guaranteed message delivery using TCP sockets
* Clean client-server architecture with a structured message protocol
* Thread-safe Windows Forms user interface updates
* Robust connection management and error handling
* Loading supplemental data from a local file to customize responses sent to clients

To use the software:
- Build and run the application in Visual Studio (or the built executable).
- One player selects **Host Game**: Starts TCP listener for gameplay and UDP broadcaster for discovery.
- Other player selects **Join Game**: Scans for UDP broadcasts, lists available hosts, and connects via TCP.
- Players place ships on their grids, then take alternating turns attacking until one wins.

My primary purpose was to gain deep, practical experience with socket programming in C#, understand UDP vs TCP trade-offs, and build a complete, professional-looking application for my software engineering portfolio.

[Software Demo Video](https://youtube.com/your-actual-video-link-here)  
*(4-5 minute video showing your face, two instances communicating on the network, gameplay demo, and code walkthrough.)*

# Network Communication

SwarNet uses a **client-server architecture** with hybrid discovery:

- **Server** (host): Listens for TCP connections and broadcasts UDP announcements.
- **Clients** (joiners): Listen for UDP broadcasts to discover hosts, then connect via TCP.

Protocols and ports (configurable in code; defaults shown):
- **UDP** for discovery: Broadcast announcements on port 55555 (or similar) so clients find hosts automatically.
- **TCP** for gameplay: Reliable communication on port 12345 (or similar) using TcpListener/TcpClient.

Message format:
- Text-based JSON payloads sent over TCP (UTF-8 encoded strings).
- Prefixed with a 4-byte integer length header to properly frame messages on the stream.
- Each JSON object includes a "type" field to identify the message purpose (e.g., "join", "attack", "attackResult") along with relevant game data (coordinates, results, turn info, etc.).
- This format supports at least three distinct request/response types, is human-readable for easy debugging, and aligns with common application-layer protocols.

Local file integration:
- In response to certain client requests or game events, the host loads supplemental text or configuration data from a local file.
- The file is read on the server side and used to customize the JSON response payload sent back to the client (e.g., including status messages or predefined strings).
- This demonstrates obtaining information from a local file directly in response to network requests, with the loaded data incorporated into what is displayed or processed on the client side.

# Development Environment

* **Operating System**: Windows 11
* **IDE**: Visual Studio 2022 (Community Edition)
* **Programming Language & Framework**: C# with .NET 8.0 (Windows Forms Application)
* **Networking Libraries**: System.Net.Sockets (TcpListener, TcpClient, UdpClient)
* **Serialization**: System.Text.Json (used for loading the local supplemental file and handling message payloads)
* **UI Framework**: Windows Forms (with custom thread-safe helpers in CrossThread folder)
* **Other Tools**: Git for version control, GitHub for public repository
* **Testing Setup**: Multiple instances on same machine (localhost) and across local network devices

# Useful Websites

* [Microsoft Docs – TcpListener Class](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener)
* [Microsoft Docs – UdpClient Class](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient)
* [Making Thread-Safe Calls to Windows Forms Controls](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-make-thread-safe-calls)
* [System.Text.Json Overview](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview)
* [C# Network Programming Best Practices](https://learn.microsoft.com/en-us/dotnet/framework/network-programming/)
* [Battleship Game Rules & Strategy (Wikipedia)](https://en.wikipedia.org/wiki/Battleship_(game))

# Future Work

* Add reconnection handling for dropped TCP connections
* Implement configurable ports and host name via UI/settings
* Enhance UI with animations, sound effects, or hit/miss visuals
* Support more than two players or add spectator mode
* Add basic encryption or message validation for security
* Package as a standalone .exe for easier sharing