# Design & Optimization Considerations – Interaction System

This document captures key design questions, trade-offs, and optimization considerations explored during the development of the interaction system.

---

## 1. Interaction Architecture

### ❓ Why use interfaces (`IInteractable`, `IHoldable`) instead of a single base class?
- Allows flexibility for future interaction types
- Prevents deep inheritance chains
- Enables composition over inheritance

---

## 2. Detection Strategy

### ❓ Why use a central `InteractionDetector` instead of logic on each object?
- Reduces per-object Update calls
- Improves performance in scenes with many interactables
- Centralizes focus and input logic

---

## 3. Event-Based Design

### ❓ Why are switches connected to doors via events instead of direct references?
- Prevents tight coupling
- Enables designer-driven workflows via Inspector
- Supports chained interactions without code changes

Example:
- One switch → multiple doors
- Multiple switches → one door

---

## 4. Animator vs Transform-Based Logic

### ❓ Why support doors without Animator?
- Not all props require complex animation
- Pivot-based rotation is cheaper and simpler
- Allows rapid prototyping

Design choice:
- If Animator exists → use Animator
- Else → fallback to transform rotation

---

## 5. Inventory Design

### ❓ Why use ScriptableObjects for items?
- Data-driven approach
- Easy to extend with new item types
- Clean separation between data and logic

Future extension ideas:
- Stackable items
- Item categories
- Save/load support

---

## 6. UI Feedback Decisions

### ❓ Why dynamic prompt text per interactable?
- Improves player clarity
- Avoids generic “Press E” UX
- Allows state-based feedback (Locked / Open / Close)

---

## 7. Hold Interaction Implementation

### ❓ Why not handle hold logic inside the interactable?
- Centralized hold handling avoids duplication
- Ensures consistent behavior across all hold objects
- UI (progress bar) can be reused easily

---

## 8. Performance Considerations

- No Update() calls on interactables
- Physics checks only from player detector
- Coroutine-based animations instead of frame polling
- Minimal allocations during interaction flow

---

## 9. Extensibility Thoughts

Potential future improvements:
- Save/Load interaction states
- Network-safe interaction events
- Interaction priority system
- Context-sensitive prompts (gamepad / keyboard)

---

