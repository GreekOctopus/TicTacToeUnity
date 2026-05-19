# Tic Tac Toe Unity 3D

A 3D Tic Tac Toe game built in Unity, powered by a decoupled C# game engine and an unbeatable Minimax AI.

---

## Features

- **Decoupled C# Core Engine**: Uses clean logic scripts ([Board.cs](Assets/Scripts/Board.cs), [Game.cs](Assets/Scripts/Game.cs)) that are independent of Unity's rendering loop, ensuring excellent testability and modularity.
- **Minimax AI**: An unbeatable AI opponent (`Difficulty.Hard`) that calculates all future board configurations to ensure it never loses.
- **3D Gameplay**: An interactive 3D board grid with physical block colliders.
- **Dynamic Input**: Uses Unity's **New Input System** package to handle mouse clicks and cast rays into the 3D scene.
- **Juicy Animations**: Spawned tokens scale up smoothly with a custom bounce effect using a C# Coroutine (requires no third-party assets).
- **Vibrant Materials**: Colored materials (Green for X, Blue for O) over a dark board layout.
- **Optimal Angle**: The camera is positioned at an optimal 60-degree tilt for a premium 3D perspective.

---

## Folder Structure

- **`Assets/Scripts/`**: Contains core C# logic files (`Board.cs`, `Game.cs`, `AiOpponent.cs`, etc.) and the Unity script wrapper (`UnityGameController.cs`, `BoardCell.cs`).
- **`Assets/Prefabs/`**: Standard 3D Prefab templates for the X and O game tokens.
- **`Assets/Materials/`**: Color settings (Board, X, and O colors).
- **`Assets/Scenes/`**: The standard default gameplay scene.

---

## Getting Started

1. **Install Unity**: Make sure you have Unity Hub and Unity Editor (Unity 2022, Unity 6, or newer) installed.
2. **Open Project**: Add the project root folder to Unity Hub and open it.
3. **Run the Game**:
   - Double-click the scene file located at `Assets/Scenes/SampleScene.unity`.
   - Click the **Play** button at the top of the Unity Editor.
   - Click any empty tile to make your move as `X`. The AI will instantly counter-move as `O`.
