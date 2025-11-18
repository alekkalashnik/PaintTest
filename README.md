# Paint UI Automation Test Suite

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![FlaUI](https://img.shields.io/badge/FlaUI-5.0.0-blue)](https://github.com/FlaUI/FlaUI)
[![Reqnroll](https://img.shields.io/badge/Reqnroll-3.2.1-green)](https://reqnroll.net/)
[![NUnit](https://img.shields.io/badge/NUnit-4.2.2-brightgreen)](https://nunit.org/)

Automated UI testing suite for Microsoft Paint application using BDD (Behavior-Driven Development) approach with Reqnroll and FlaUI.

##  Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running Tests](#running-tests)
- [Project Structure](#project-structure)
- [Test Scenarios](#test-scenarios)
- [Architecture](#architecture)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)

##  Overview

This project provides comprehensive UI automation testing for Microsoft Paint (mspaint.exe) using:

- **FlaUI**: UI automation library based on Microsoft UI Automation API
- **Reqnroll**: BDD framework (successor to SpecFlow)
- **NUnit**: Testing framework
- **Page Object Model**: Design pattern for maintainable test code

##  Features

- ? **BDD Test Scenarios**: Human-readable test scenarios using Gherkin syntax
- ? **Page Object Model**: Clean separation of concerns and maintainable code
- ? **Robust Error Handling**: Retry logic for timing-sensitive operations
- ? **Multiple Test Categories**: Smoke, UI, Drawing, Tools, File Operations, Edit, View/Zoom
- ? **Comprehensive Coverage**: Tests for all major Paint features
- ? **CI/CD Ready**: Designed for continuous integration pipelines
- ? **HTML reporting**: Available by this path PaintTest\bin\Debug\net8.0-windows\Reports

##  Prerequisites

- **Operating System**: Windows 10/11 (Paint application required)
- **.NET SDK**: 8.0 or higher
- **Visual Studio** or **Visual Studio Code** (optional but recommended)
- **Microsoft Paint** (mspaint.exe) - Included with Windows

##  Installation

### 1. Clone the Repository

```bash
git clone <https://github.com/alekkalashnik/PaintTest>
cd PaintTest
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Build the Project

```bash
dotnet build
```

##  Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Tests by Category

```bash
# Smoke tests only
dotnet test --filter "TestCategory=smoke"

# File operations tests
dotnet test --filter "TestCategory=file-operations"

# Drawing tests
dotnet test --filter "TestCategory=drawing"
```

### Run Specific Test

```bash
dotnet test --filter "FullyQualifiedName~SaveInDifferentFormats"
```

### Run with Verbose Output

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Run in Visual Studio

1. Open `PaintTest.sln` in Visual Studio
2. Build the solution
3. Open Test Explorer (Test ? Test Explorer)
4. Click "Run All" or right-click specific tests

##  Project Structure

```
PaintTest/
  Core/
?    ApplicationManager.cs          # Application lifecycle management
?    PaintContext.cs                # Shared test context
?    Exceptions/
?       ElementNotFoundException.cs # Custom exceptions
?
  Pages/                              # Page Object Model
?     BasePage.cs                    # Base page functionality
?     BaseFileDialogPage.cs         # Base for file dialogs
?     PaintMainPage.cs              # Main Paint window
?     OpenDialogPage.cs             # Open file dialog
?     SaveAsDialogPage.cs           # Save As dialog
?     ImagePropertiesPage.cs        # Image properties dialog
?
  Features/
?     StepDefinitions/              # Gherkin step implementations
?        PaintApplicationSteps.cs
?     Hooks/
?         TestHooks.cs              # Before/After hooks
?
  gherkin Features/                  # BDD feature files
?     PaintApplication.feature
?     PaintDrawing.feature
?     PaintTools.feature
?     PaintFileOperations.feature
?     PaintEditOperations.feature
?     PaintViewZoom.feature
?
  PageObjectModelGuide.md           # POM architecture guide
  TestScenarios.md                  # Test scenarios overview
  README.md                         # This file
```

## Test Scenarios

The project includes comprehensive test coverage across multiple feature areas:

### Smoke Tests (@smoke)
Quick validation of critical functionality
- Application launch
- File menu accessibility
- Graceful closure

### UI Tests (@ui, @critical)
User interface and window management
- Launch and verify canvas
- Window state management (maximize/minimize/restore)

### Drawing Tests (@drawing)
Drawing operations and tools
- Draw lines, shapes
- Use different tools (pencil, brush, eraser)
- Canvas operations

### Tool Tests (@tools)
Tool selection and configuration
- Verify tool availability
- Switch between tools
- Color selection
- Brush size configuration

### File Operations (@file-operations, @save, @open)
File management
- Save in different formats (PNG, JPEG, BMP, GIF)
- Open existing files
- Handle overwrite confirmations
- Recent files list

### Edit Operations (@edit, @undo-redo)
Content manipulation
- Undo/redo
- Cut/copy/paste
- Select operations
- Rotate and flip
- Crop canvas

### View/Zoom (@view, @zoom)
View controls
- Zoom in/out
- Full screen mode
- Rulers and gridlines
- Magnifier tool

For detailed test scenarios, see [TestScenarios.md](TestScenarios.md).

##   Architecture

### Page Object Model

The project follows the Page Object Model design pattern:

```
             
?     Gherkin Feature Files           ?  Business-readable scenarios
             
               ?
             
?     Step Definitions                ?  Test orchestration & assertions
             
               ?
             
?     Page Objects                    ?  UI interaction & locators
             
               ?
             
?     FlaUI (UI Automation)           ?  Low-level UI automation
             
```

**Benefits:**
 **Separation of Concerns**: UI logic separated from test logic
 **Maintainability**: UI changes only affect Page Objects
 **Reusability**: Page Object methods reused across tests
 **Readability**: Tests read like plain English

### Coding Standards

- Follow C# naming conventions
- Use meaningful variable and method names
- Add XML comments for public methods
- Keep methods focused and atomic
- Handle exceptions gracefully

##  Development Roadmap

- [ ] Add screenshot capture on test failure
- [ ] Implement parallel test execution
- [ ] Add HTML test reports
- [ ] Extend coverage to Paint 3D
- [ ] Add performance benchmarking
- [ ] Integrate with CI/CD pipelines

##  License

This project is for educational and testing purposes.
