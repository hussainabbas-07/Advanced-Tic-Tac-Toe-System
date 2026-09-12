# Advanced Tic Tac Toe

A feature-rich desktop Tic Tac Toe application developed in C# and Windows Forms, designed to demonstrate object-oriented programming, game logic, AI decision-making, tournament management, persistent data storage, and desktop UI development.

## Overview

Advanced Tic Tac Toe is a Windows Forms application that extends the traditional Tic Tac Toe game into a complete competitive gaming system.

The application supports player-versus-player matches, player-versus-AI gameplay, multiple AI difficulty levels, tournaments with bracket progression, player statistics, match history, leaderboards, persistent data storage, and a custom tactical user interface.

The project was developed as an Object-Oriented Programming semester project using C# and Windows Forms.

## Features

* Player vs Player gameplay
* Player vs AI gameplay
* Easy, Medium, and Hard AI difficulty levels
* AI move decision logic
* Toss and starting-player selection
* Match scoring and round management
* Tournament mode
* Automatic tournament bracket progression
* Tournament champion tracking
* Player profiles and statistics
* Win, loss, and draw tracking
* Match history
* Tournament history
* Global leaderboard
* Persistent data storage
* File-based save and load functionality
* Input validation and exception handling
* Custom Windows Forms interface
* Tactical gaming-inspired visual design
* Animated UI elements and visual effects
* Dedicated screens for game, tournament, bracket, history, and leaderboard management

## Technology Stack

| Technology                  | Purpose                                     |
| --------------------------- | ------------------------------------------- |
| C#                          | Application development                     |
| .NET                        | Application framework                       |
| Windows Forms               | Desktop graphical user interface            |
| Object-Oriented Programming | Application architecture                    |
| Collections                 | Player, match, tournament, and ranking data |
| File Handling               | Persistent application data                 |
| Git                         | Version control                             |
| GitHub                      | Source code hosting and release management  |
| Visual Studio 2022          | Development environment                     |

## Object-Oriented Programming

The project was designed around core Object-Oriented Programming principles.

### Encapsulation

Data is controlled through classes, properties, and controlled access methods to protect object state and maintain consistent application behavior.

### Inheritance

Specialized player types inherit common functionality from the base `Player` class.

Examples include:

* `HumanPlayer`
* `AIPlayer`

### Polymorphism

Player behavior is implemented through overridden methods, allowing different player types to provide their own implementations of shared functionality.

### Abstraction

Abstract classes are used to define common behavior while allowing derived classes to provide their own implementations.

### Interfaces

Interfaces such as `ISaveable` and `IStatistics` define common contracts for classes that provide persistence and statistical functionality.

### Association

Game-related classes interact with player objects to manage gameplay.

### Aggregation

Tournament-related components work with objects such as player statistics, match history, and leaderboard data.

### Composition

The game manages objects that form essential parts of the gameplay system, such as the game board.

## Application Architecture

The project is organized into separate components according to their responsibilities.

```text
AdvancedTicTacToeWinForms
│
├── Classes
│   ├── AIPlayer.cs
│   ├── Board.cs
│   ├── Game.cs
│   ├── HumanPlayer.cs
│   ├── Leaderboard.cs
│   ├── MatchHistory.cs
│   ├── Player.cs
│   ├── PlayerStatistics.cs
│   ├── Profile.cs
│   └── Tournament.cs
│
├── Data
│   ├── History.txt
│   ├── Leaderboard.txt
│   ├── Player.txt
│   ├── TournamentHistory.txt
│   └── TournamentLeaderboard.txt
│
├── Enums
│   ├── Difficulty.cs
│   └── GameResult.cs
│
├── Forms
│   ├── AISetupForm.cs
│   ├── BracketForm.cs
│   ├── GameForm.cs
│   ├── HistoryForm.cs
│   ├── LeaderForm.cs
│   ├── MainMenuForm.cs
│   ├── PlayerSetupForm.cs
│   └── TournamentForm.cs
│
├── Interfaces
│   ├── ISaveable.cs
│   └── IStatistics.cs
│
├── Utilities
│   └── FileManager.cs
│
├── Program.cs
├── AdvancedTicTacToeWinForms.csproj
└── AdvancedTicTacToeWinForms.sln
```

## User Interface

The application uses a custom tactical interface rather than the default Windows Forms appearance.

### Main Menu

The main menu provides access to the major game modes and system features.

### Player Setup

The player setup screen allows users to configure the participants before starting a match.

### AI Setup

The AI setup screen provides configuration for player-versus-AI gameplay and difficulty selection.

### Game Screen

The game screen provides the primary gameplay experience with player information, board interaction, turn indicators, scoring, timer functionality, and match controls.

### Tournament

The tournament interface allows users to configure participants and start a tournament.

### Tournament Bracket

The bracket interface displays tournament rounds, matchups, winners, and progression toward the championship.

### Match History

The history interface provides access to recorded matches and tournament results.

### Leaderboard

The leaderboard displays player rankings and competitive statistics.

## Game Modes

### Player vs Player

Two human players compete against each other through the graphical game interface.

### Player vs AI

A human player competes against an AI opponent with configurable difficulty.

### Tournament Mode

Tournament mode allows multiple players to compete through a structured bracket system.

Completed matches automatically determine the participants of subsequent rounds until a tournament champion is determined.

## AI System

The application provides multiple AI difficulty levels:

### Easy

The AI selects from available moves using a simple move-selection strategy.

### Medium

The AI prioritizes winning opportunities and attempts to block the opponent before selecting another available move.

### Hard

The Hard difficulty uses a stronger decision-making strategy to select highly competitive moves.

## Data Persistence

The application uses file-based persistence to store important player and match information.

Stored data includes:

* Player information
* Match history
* Tournament history
* Leaderboard information
* Player statistics

The `FileManager` utility handles the required file operations.

## Validation and Error Handling

The application includes input validation and exception handling to improve reliability and prevent invalid user input from causing unexpected application failures.

## Development Environment

The project was developed using:

* Visual Studio 2022
* C#
* .NET
* Windows Forms

## Getting Started

### Prerequisites

Before running the project, install:

* Visual Studio 2022
* .NET support for Windows Forms
* A Windows environment compatible with the project's target framework

### Running the Project

1. Clone the repository.

```bash
git clone https://github.com/hussainabbas-07/Advanced-Tic-Tac-Toe-System.git
```

2. Open the solution file:

```text
AdvancedTicTacToeWinForms.sln
```

3. Open the solution in Visual Studio 2022.

4. Build the solution.

5. Run the application.

## Project Status

The project is currently implemented and functional.

The current version includes the core gameplay system, AI gameplay, tournament management, bracket progression, player statistics, match history, leaderboard functionality, persistent data storage, and the custom Windows Forms interface.

## Future Improvements

Potential future improvements include:

* Online multiplayer
* Network-based gameplay
* Database-backed persistence
* User authentication
* Cloud synchronization
* Additional game modes
* Advanced AI algorithms
* Expanded tournament formats
* Cross-platform support

## Developer & Academic Context

This project was independently designed and developed by Syed Hussain Abbas as an Object-Oriented Programming semester project at DHA Suffa University.

## License

This project currently does not include an open-source license.
