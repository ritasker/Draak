# Draak

A text adventure web application built with .NET.

## Overview

Draak is a text-based adventure game that runs in the browser, combining the nostalgia of classic text adventures with modern web technologies.

## Getting Started

### Prerequisites

- .NET SDK

### Building the Project

The project uses the modern .slnx solution file format. To build:

```bash
dotnet build ./Pier8.Draak.slnx
```

## Architecture

The application will follow a modern web architecture:

- **Game Engine**: Core text adventure logic in .NET
- **Minimal API**: Lightweight REST endpoints for game state and actions
- **Blazor Frontend**: Server-side rendering with interactive components
- **AlpineJS**: Client-side interactivity and DOM manipulation

## Technologies Used

- **.NET**: Backend game engine and web framework
- **ASP.NET Blazor**: Web UI framework
- **AlpineJS**: Lightweight JavaScript framework for interactivity
- **Minimal APIs**: Simple, fast HTTP API endpoints

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Author

Richard Tasker (Copyright 2021)