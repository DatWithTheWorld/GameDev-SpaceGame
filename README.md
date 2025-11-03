# SpaceExplorer

A 2D space shooter game built with Unity, where you pilot a spaceship through an asteroid field, collect stars for points, and try to survive as long as possible.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Running the Project](#running-the-project)
- [Project Structure](#project-structure)
- [Game Mechanics](#game-mechanics)
- [Controls](#controls)
- [Scripts Overview](#scripts-overview)
- [Dependencies](#dependencies)
- [Building the Game](#building-the-game)
- [Troubleshooting](#troubleshooting)

## Overview

SpaceExplorer is an arcade-style 2D space shooter game developed in Unity. The player controls a spaceship that must navigate through a field of moving asteroids while collecting stars to earn points. The game ends when the player collides with an asteroid.

## Features

- **Smooth Player Movement**: Arrow keys or WASD control system with boundary constraints
- **Laser Shooting**: Space bar or mouse click to fire lasers at asteroids
- **Dynamic Enemy Spawning**: Asteroids spawn continuously from screen edges with random movement patterns
- **Collectible Stars**: Collect stars that spawn on screen to earn points
- **Score System**: Track your score with real-time updates and penalty system on collision
- **Main Menu**: User-friendly menu with play, instructions, and quit options
- **Game Over Screen**: Shows final score with restart and main menu options
- **Background Effects**: Animated background stars for visual appeal
- **Modular Architecture**: Clean code structure with manager classes and separated concerns

## Requirements

### Software
- **Unity Editor**: Version 6000.2.1f1 or compatible (LTS versions recommended)
- **Platform**: Windows, macOS, or Linux

### System Requirements
- Minimum 4GB RAM (8GB recommended)
- DirectX 11 or OpenGL 3.2 compatible graphics card
- 2GB free disk space

### Unity Packages
The project uses the following Unity packages (automatically managed):
- Universal Render Pipeline (URP) 17.2.0
- Unity Input System 1.14.2
- 2D Animation & Sprite packages
- TextMeshPro
- Unity UI (uGUI)

## Installation

### Step 1: Clone or Download the Repository

```bash
git clone https://github.com/DatWithTheWorld/GameDev-Ex4
cd GameDev-Ex4
```

Or download the ZIP file and extract it to your desired location.

### Step 2: Open in Unity

1. Launch Unity Hub
2. Click **Add** or **Open** project
3. Navigate to the project folder (contains `Assets`, `ProjectSettings`, `Packages` folders)
4. Select the project folder and click **Open**

### Step 3: Wait for Unity to Import

Unity will automatically:
- Import all assets
- Compile scripts
- Set up package dependencies
- Generate necessary meta files (if not present)

**Note**: The first import may take several minutes depending on your system.

### Step 4: Verify Project Settings

1. Open **Edit** > **Project Settings**
2. Ensure **Universal Render Pipeline** is set as the render pipeline
3. Check that **Input System** package is installed (visible in Package Manager)

## Running the Project

### Option 1: Play in Unity Editor

1. Open the **MainMenu** scene:
   - Navigate to `Assets/Scenes/MainMenu.unity`
   - Double-click to open it

2. Press the **Play** button (▶) in the Unity Editor toolbar

3. Use the Main Menu:
   - Click **Play** to start the game
   - Click **Instructions** to view game instructions
   - Click **Quit** to exit (works in builds)

### Option 2: Load Gameplay Scene Directly

1. Open `Assets/Scenes/Gameplay.unity`
2. Press **Play** button

**Note**: Ensure both `MainMenu.unity` and `Gameplay.unity` are added to **File > Build Settings > Scenes in Build**.

## Project Structure

```
SpaceExplorer/
├── Assets/
│   ├── Scripts/              # All C# game scripts
│   │   ├── Collectibles/     # Star collectible script
│   │   ├── Enemy/            # Asteroid controller
│   │   ├── Managers/         # Game management scripts
│   │   ├── Player/           # Player controller and laser
│   │   └── UI/               # UI management scripts
│   ├── Scenes/               # Unity scene files
│   │   ├── MainMenu.unity
│   │   ├── Gameplay.unity
│   │   └── SampleScene.unity
│   ├── Prefabs/              # Reusable game objects
│   ├── Sprites/              # 2D sprite assets
│   ├── Audio/                # Sound effects and music
│   ├── Materials/            # Material assets
│   └── Settings/             # Project settings and configs
├── ProjectSettings/          # Unity project configuration
├── Packages/                 # Package dependencies
├── Library/                  # Generated Unity files (git-ignored)
└── README.md                # This file
```

## Game Mechanics

### Player
- **Movement**: Arrow keys or WASD to move in all directions
- **Boundaries**: Player cannot move outside screen boundaries
- **Shooting**: Space bar or left mouse click to fire lasers
- **Collision**: Hitting an asteroid ends the game

### Asteroids
- **Spawning**: Spawn continuously from screen edges
- **Movement**: Random direction movement with rotation
- **Collision**: Destroy player on contact
- **Laser Interaction**: Can be destroyed by lasers (if implemented)

### Stars
- **Spawning**: Spawn randomly inside the screen area
- **Collection**: Player collects stars by touching them
- **Points**: Each star awards points (default: 10 points)
- **Animation**: Rotating and floating animation

### Scoring
- **Points**: Collected stars add to score
- **Penalty**: Colliding with asteroids may deduct points (configurable)
- **Display**: Score shown in real-time and on game over screen

## Controls

| Action | Input |
|--------|-------|
| Move Up | ↑ (Arrow Up) / W |
| Move Down | ↓ (Arrow Down) / S |
| Move Left | ← (Arrow Left) / A |
| Move Right | → (Arrow Right) / D |
| Shoot Laser | Space / Left Mouse Click |
| Pause (if implemented) | Escape |

## Scripts Overview

### Core Managers

#### `GameManager.cs`
- **Purpose**: Central game state manager using Singleton pattern
- **Responsibilities**:
  - Manages game state (active/paused/game over)
  - Tracks and updates score
  - Handles game over logic
  - Manages scene transitions
  - Provides events for score changes and game over

#### `SpawnManager.cs`
- **Purpose**: Controls spawning of asteroids and stars
- **Responsibilities**:
  - Spawns asteroids at screen edges periodically
  - Spawns stars inside screen area
  - Manages spawn rates and maximum counts
  - Supports multiple prefab variations

#### `SceneTransitionManager.cs`
- **Purpose**: Handles scene loading and transitions
- **Responsibilities**:
  - Load gameplay scene from main menu
  - Quit game functionality
  - Scene loading with proper cleanup

#### `BackgroundStarGenerator.cs`
- **Purpose**: Creates animated background star effects
- **Responsibilities**:
  - Generates decorative background stars
  - Manages star animations

### Player Scripts

#### `PlayerController.cs`
- **Purpose**: Controls player spaceship
- **Responsibilities**:
  - Handles movement input
  - Constrains movement to screen boundaries
  - Manages shooting with fire rate limits
  - Detects collisions with asteroids

#### `Laser.cs`
- **Purpose**: Controls laser projectile behavior
- **Responsibilities**:
  - Moves laser forward
  - Handles collision with asteroids
  - Destroys itself when off-screen or on hit

### Enemy Scripts

#### `AsteroidController.cs`
- **Purpose**: Controls asteroid movement and behavior
- **Responsibilities**:
  - Random movement patterns
  - Rotation animation
  - Collision detection with player
  - Destruction handling

### Collectible Scripts

#### `StarCollectible.cs`
- **Purpose**: Manages collectible star behavior
- **Responsibilities**:
  - Rotation and floating animations
  - Collision detection with player
  - Score awarding on collection

### UI Scripts

#### `MainMenuUI.cs`
- **Purpose**: Manages main menu UI interactions
- **Responsibilities**:
  - Play button functionality
  - Instructions panel toggle
  - Quit button handling

#### `GameplayUI.cs`
- **Purpose**: Manages in-game UI elements
- **Responsibilities**:
  - Score display
  - Health display (if implemented)
  - Game state UI updates

#### `EndGameUI.cs`
- **Purpose**: Manages game over screen
- **Responsibilities**:
  - Display final score
  - Restart button functionality
  - Return to main menu button

#### `FitBackground.cs`
- **Purpose**: Ensures background UI fits screen properly
- **Responsibilities**:
  - Scales background to match screen size
  - Handles different aspect ratios

## Dependencies

### Unity Packages

All packages are defined in `Packages/manifest.json`:

- **com.unity.render-pipelines.universal** (17.2.0) - Universal Render Pipeline
- **com.unity.inputsystem** (1.14.2) - New Input System
- **com.unity.2d.animation** (12.0.2) - 2D Animation tools
- **com.unity.2d.tilemap** (1.0.0) - Tilemap system
- **com.unity.ugui** (2.0.0) - Unity UI system
- **com.unity.collab-proxy** (2.10.1) - Unity Collaborate/Version Control
- And other standard Unity modules

### External Assets

The project includes:
- **2D Space Kit** - Space-themed sprite pack
- **Galaxia Sprite Pack #1** - Enemy, player, and effect sprites
- **Simple FX Kit** - Visual effects
- **2DSimpleUIPack** - UI elements and prefabs
- **TextMeshPro** - Advanced text rendering

## Building the Game

### Step 1: Configure Build Settings

1. Open **File > Build Settings**
2. Ensure scenes are added in correct order:
   - **MainMenu** (index 0)
   - **Gameplay** (index 1)
3. Select target platform (Windows, macOS, Linux, etc.)

### Step 2: Configure Player Settings

1. Open **Edit > Project Settings > Player**
2. Set company name, product name, and icon
3. Configure platform-specific settings

### Step 3: Build

1. Click **Build** in Build Settings window
2. Choose output folder
3. Wait for build to complete

### Step 4: Test Build

1. Navigate to build folder
2. Run the executable
3. Test all game features

## Troubleshooting

### Common Issues

#### Scene Not Found Error
**Problem**: "Scene 'MainMenu' not found in Build Settings"

**Solution**:
1. Open **File > Build Settings**
2. Click **Add Open Scenes** or drag scenes from Project window
3. Ensure MainMenu.unity is at index 0

#### Missing References
**Problem**: Null reference exceptions in scripts

**Solution**:
1. Open the scene with the error
2. Select the GameObject with the script component
3. In Inspector, assign missing references (prefabs, UI elements, etc.)

#### Input System Not Working
**Problem**: Controls not responding

**Solution**:
1. Ensure Input System package is installed
2. Check **Edit > Project Settings > Player > Active Input Handling**
3. Set to "Input System Package (New)" or "Both"

#### Render Pipeline Errors
**Problem**: Shaders or materials appear pink/missing

**Solution**:
1. Verify URP is installed and active
2. Check **Edit > Project Settings > Graphics > Scriptable Render Pipeline Settings**
3. Ensure URP asset is assigned

#### Prefabs Missing
**Problem**: "Prefab not assigned" warnings

**Solution**:
1. Check `Assets/Prefabs/` folder
2. In Inspector, reassign prefab references
3. Verify prefabs exist and are not broken

### Performance Tips

- Limit maximum asteroids and stars in `SpawnManager`
- Use object pooling for better performance (not implemented)
- Reduce particle effects if experiencing lag
- Optimize sprite sizes and formats

## License

This project is provided as-is for educational and demonstration purposes.

## Credits

- Developed using Unity Engine
- Asset packs included from various sources (check asset README files)
- Original codebase structure by project maintainer

## Contributing

When contributing to this project:
1. Follow existing code style and commenting conventions
2. Test changes in Unity Editor before committing
3. Ensure all scenes load correctly
4. Update this README if adding new features

## Contact & Support

For issues, questions, or contributions:
- GitHub Repository: [https://github.com/DatWithTheWorld/GameDev-Ex4](https://github.com/DatWithTheWorld/GameDev-Ex4)
- Open an issue on GitHub for bug reports

---

**Enjoy playing SpaceExplorer!** 🚀⭐

