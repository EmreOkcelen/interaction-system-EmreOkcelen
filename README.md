# Unity Interaction System – Internship Case

This project implements a modular, extensible interaction system in Unity, designed as an internship technical case.  
The goal was to build a clean, scalable interaction architecture supporting multiple interaction types, UI feedback, inventory logic, and event-driven connections between objects.

---

## 🎮 Features Overview

### Interaction Types
The system supports **three core interaction types**, each implemented via base classes and interfaces:

- **Instant Interaction**
  - Single button press
  - Examples: key pickup, button press
- **Hold Interaction**
  - Requires holding the interaction key for a duration
  - Examples: chest opening
- **Toggle Interaction**
  - Binary on/off state
  - Examples: doors, switches, lights

Each interactable implements `IInteractable` and optionally `IHoldable`.

---

## 🧩 Interactable Objects

### 🚪 Door
- Toggle-based interaction
- Locked / unlocked state
- Requires specific key (via inventory)
- Shows feedback when locked
- Opens via:
  - Animator (optional)
  - OR pivot-based rotation (no animator required)

### 🔑 Key Pickup
- Instant interaction
- Adds key to player inventory
- Supports multiple key types (ScriptableObject-based)

### 🎚️ Switch / Lever
- Toggle interaction
- Event-based design
- Can trigger external objects (e.g. doors)
- Does not directly reference the target (decoupled)

### 📦 Chest / Container
- Hold interaction (configurable duration)
- One-time open
- Can spawn or grant items
- Cannot be reopened

---

## 🖥️ UI Feedback

The system provides clear visual feedback to the player:

- Interaction prompt (`Press E`, `Locked - Key Required`, etc.)
- Dynamic prompt text per interactable
- Hold progress bar (fill-based UI)
- Feedback for:
  - Out of range
  - Cannot interact (locked, invalid state)

---

## 🎒 Inventory System

- Simple inventory implementation
- Collects and stores keys
- Door checks inventory before unlocking
- Items defined using `ScriptableObject`
- Inventory UI lists collected items (simple list)

---

## 🧠 Architecture Highlights

- **Event-driven design** using `UnityEvent`
- **Loose coupling** between interactables
- **Inspector-friendly** workflow (designer-friendly)
- No hard dependencies between switches and doors
- Animator is optional; transform-based logic supported

---

## ⭐ Bonus Implementations

- Pivot-based door rotation (no animator required)
- Event-chained interactions (switch → door)
- Extendable interaction base classes
- Optimization-friendly (no Update polling on interactables)

---

## 📂 Project Structure (Simplified)

Assets/
├── Scripts/
│ └── Runtime/
│ ├── Core/
│ ├── Player/
│ ├── Interactables/
│ └── UI/
├── Prefabs/
│ ├── Doors/
│ ├── Switches/
│ ├── Items/
│ └── UI/
└── ScriptableObjects/
└── Items/


---

## 📝 Notes

This system was designed to be readable, extensible, and suitable for scaling into a full gameplay framework.

