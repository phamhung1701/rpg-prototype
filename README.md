# RPG Prototype - 3D Action RPG

A Unity-based 3D Action RPG prototype featuring a modular State Machine architecture for character control, combat system with combo attacks, and enemy targeting mechanics.

## 🎮 Features

### State Machine Architecture
- **Modular Design**: Abstract `StateMachine` and `State` base classes for extensible character behaviors
- **Player States**: FreeLook, Target Lock, Attack, and Idle states with smooth transitions
- **Easy to Extend**: Add new states by inheriting from `PlayerBaseState`

### Combat System
- **Combo Attacks**: Chain attacks with animation-based timing
- **Damage System**: Hitbox detection and damage calculation
- **Force Application**: Physics-based knockback and movement during attacks

### Target Lock System
- **Enemy Targeting**: Lock-on mechanic for focused combat
- **Camera Follow**: Dynamic camera adjustment when targeting

### Input System
- **New Input System**: Using Unity's modern Input System package
- **Keyboard & Gamepad Support**: Flexible input handling with action callbacks

## 🛠️ Tech Stack

| Technology | Usage |
|------------|-------|
| Unity 2021.3+ | Game Engine |
| C# | Programming Language |
| Unity Input System | Input Handling |
| State Machine Pattern | Character Controller Architecture |

## 📂 Project Structure

```
Assets/
├── Scripts/
│   ├── StateMachine/
│   │   ├── StateMachine.cs          # Base state machine
│   │   ├── State.cs                 # Abstract state class
│   │   └── PlayerStateMachine/
│   │       ├── PlayerStateMachine.cs
│   │       ├── PlayerBaseState.cs
│   │       ├── PlayerFreeLookState.cs
│   │       ├── PlayerTargetState.cs
│   │       ├── PlayerAttackState.cs
│   │       └── PlayerIdleState.cs
│   ├── InputReader.cs               # Input handling
│   ├── ForceReceiver.cs             # Physics forces
│   └── Hitbox.cs                    # Combat detection
├── Combat/
│   ├── Attack/                      # Attack definitions
│   └── Target/                      # Targeting system
├── Animations/                      # Character animations
├── Models/                          # 3D models
└── Scenes/                          # Game scenes
```

## 📝 Status

🚧 **Work in Progress** - This is a learning prototype for practicing game development patterns and Unity mechanics.
