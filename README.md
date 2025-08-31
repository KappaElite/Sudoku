# Sudoku Game (C# / Avalonia)

## Project Description
This project is a **Sudoku game** implemented in **C#** using the **Avalonia UI framework**.  
It features:
- Sudoku board generation,  
- Sudoku solving,  
- user interaction through a graphical interface.  

The code applies several **design patterns** to organize logic and improve maintainability.

---

## Design Patterns Used

### Memento Pattern
Used in `Memento`, `Caretaker`, and `SudokuSolver` to **save and restore** the state of the Sudoku board during puzzle generation.  

### Strategy Pattern
Implemented in `IOperationStrategy`, `EasyModeStrategy`, and `HardModeStrategy` to allow different **hint management strategies** depending on the selected difficulty.  

### State Pattern
Used in `IState`, `ValidInputState`, and `InvalidInputState` to manage the **state of individual Sudoku fields** (valid/invalid input).  

### Observer Pattern
Defined by `IObserver`, `ISubject`, and used in `SingleSudokuField` to **update UI elements** in response to changes.  

---

## Principles
The project demonstrates the use of **object-oriented principles** and **design patterns** to create a **modular** and **extensible** Sudoku application.
