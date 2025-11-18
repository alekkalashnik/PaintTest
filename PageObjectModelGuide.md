# Page Object Model (POM) Architecture

## 🎯 Design Principles

This project follows the **Page Object Model** pattern to ensure maintainable, readable, and scalable test automation.

## 📐 Architecture Layers

```
???????????????????????????????????????????????
?   Feature Files (.feature)                  ?  ? Gherkin scenarios (business language)
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
?   Step Definitions (*Steps.cs)              ?  ? Test orchestration (WHAT to test)
?   - NO UI locators                          ?
?   - NO FlaUI calls                          ?
?   - Only Page Object method calls           ?
?   - Assertions (NUnit)                      ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
?   Page Objects (Pages/*Page.cs)             ?  ? UI interactions (HOW to test)
?   - ALL UI locators (FindFirstDescendant)   ?
?   - ALL FlaUI interactions                  ?
?   - NO test logic                           ?
?   - NO assertions                           ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
?   Base Page (BasePage.cs)                   ?  ? Common functionality
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
?   Application Manager                       ?  ? App lifecycle management
???????????????????????????????????????????????
```

## ✅ Correct Implementation

### Step Definition (GOOD ✓)
```csharp
[When(@"I click on ""(.*)"" button")]
public void WhenIClickOnButton(string buttonName)
{
    _paintMainPage!.ClickButton(buttonName);  // ? Delegates to Page Object
}

[Then(@"the Paint canvas should be visible")]
public void ThenThePaintCanvasShouldBeVisible()
{
    Assert.That(_paintMainPage!.IsCanvasVisible(), Is.True);  // ? Uses Page Object method
}
```

### Page Object (GOOD ✓)
```csharp
public void ClickButton(string buttonName)
{
    var button = MainWindow.FindFirstDescendant(cf =>   // ? Locator logic HERE
        cf.ByName(buttonName).And(cf.ByControlType(ControlType.Button)));
    
    if (button != null)
    {
        button.Click();  // ? FlaUI interaction HERE
        Thread.Sleep(300);
    }
    else
    {
        throw new ElementNotFoundException($"Button '{buttonName}' not found", buttonName);
    }
}
```

## ❌ Anti-Patterns (AVOID)

### ❌ BAD: UI Locators in Step Definitions
```csharp
[When(@"I click on ""(.*)"" button")]
public void WhenIClickOnButton(string buttonName)
{
    // ? DON'T DO THIS - UI logic in step definition
    var button = _paintMainPage!.MainWindow.FindFirstDescendant(cf => 
        cf.ByName(buttonName).And(cf.ByControlType(ControlType.Button)));
    
    button?.Click();
}
```

### ❌ BAD: Assertions in Page Objects
```csharp
public void VerifyCanvasVisible()
{
    var canvas = GetCanvas();
    // ? DON'T DO THIS - assertions belong in step definitions
    Assert.That(canvas, Is.Not.Null, "Canvas should be visible");
}
```

### ❌ BAD: Test Logic in Page Objects
```csharp
public void LaunchAndMaximize()
{
    // ? DON'T DO THIS - orchestration logic belongs in step definitions
    WaitForPageLoad();
    MaximizeWindow();
    OpenFileMenu();
}
```

## 📊 Responsibility Matrix

| Layer | Responsibilities | Contains | Does NOT Contain |
|-------|-----------------|----------|------------------|
| **Feature Files** | Business scenarios | Gherkin (Given/When/Then) | Code, technical details |
| **Step Definitions** | Test orchestration, assertions | - Page Object method calls<br>- NUnit assertions<br>- Console logging | - UI locators<br>- FlaUI calls<br>- Direct UI interaction |
| **Page Objects** | UI interaction, element location | - FindFirstDescendant<br>- FlaUI interactions<br>- Element getters<br>- Action methods | - Assertions<br>- Test logic<br>- Test orchestration |
| **Base Page** | Common page functionality | - Wait methods<br>- Page load verification | - Specific UI elements |
| **Application Manager** | App lifecycle | - StartApplication<br>- CloseApplication | - Page-specific logic |

## 📁 File Structure

```
PaintTest/
??? gherkin Features/
?   ??? PaintApplication.feature          ? Gherkin scenarios
?   ??? PaintDrawing.feature
?   ??? PaintTools.feature
?
??? Features/
?   ??? StepDefinitions/
?   ?   ??? PaintApplicationSteps.cs      ? Step definitions (orchestration)
?   ?   ??? PaintDrawingSteps.cs
?   ?   ??? PaintToolsSteps.cs
?   ?
?   ??? Hooks/
?       ??? TestHooks.cs                  ? Before/After scenario hooks
?
??? Pages/
?   ??? BasePage.cs                       ? Base page class
?   ??? PaintMainPage.cs                  ? Page Objects (UI interaction)
?   ??? PaintImagePropertiesPage.cs
?   ??? PaintFileMenuPage.cs
?
??? Core/
    ??? ApplicationManager.cs             ? App lifecycle
    ??? Exceptions/
        ??? ElementNotFoundException.cs   ? Custom exceptions
```

## 🎁 Key Benefits

### 🔧 Maintainability
- UI changes only require updating Page Objects
- Step definitions remain unchanged when UI changes
- Single source of truth for element locators

### 📖 Readability
- Feature files read like plain English
- Step definitions show test flow clearly
- Page Objects hide UI complexity

### ♻️ Reusability
- Page Object methods can be used by multiple step definitions
- Common functionality in BasePage
- Shared across different feature files

### 🧪 Testability
- Easy to mock Page Objects for unit testing step definitions
- Clear separation of concerns
- Independent testing of each layer

## 📝 Coding Guidelines

### Step Definitions Should:
- ✓ Use descriptive method names matching Gherkin steps
- ✓ Call Page Object methods
- ✓ Contain assertions (for Then steps)
- ✓ Log important information to console
- ✓ Handle test orchestration

### Page Objects Should:
- ✓ Encapsulate UI element location logic
- ✓ Provide action methods (Click, Enter, Select)
- ✓ Provide query methods (IsVisible, GetText)
- ✓ Throw meaningful exceptions when elements not found
- ✓ Include Thread.Sleep for UI synchronization
- ✓ Return data (strings, booleans) for verification

### Page Objects Should NOT:
- ❌ Contain assertions (Assert.That, Assert.IsTrue)
- ❌ Contain test logic or orchestration
- ❌ Call other test steps
- ❌ Write to console (except errors/warnings)

## 🚀 Example: Adding a New Feature

### 1. Create Feature File
```gherkin
Feature: Paint Color Selection
    Scenario: Select red color
        Given I have launched Paint application
        When I select "Red" color from palette
        Then the primary color should be "Red"
```

### 2. Create Step Definitions
```csharp
[When(@"I select ""(.*)"" color from palette")]
public void WhenISelectColorFromPalette(string colorName)
{
    _paintMainPage!.SelectColor(colorName);  // ? Delegates to Page Object
}

[Then(@"the primary color should be ""(.*)""")]
public void ThenThePrimaryColorShouldBe(string expectedColor)
{
    var actualColor = _paintMainPage!.GetPrimaryColor();  // ? Gets data from Page Object
    Assert.That(actualColor, Is.EqualTo(expectedColor));   // ? Assertion in step def
}
```

### 3. Implement in Page Object
```csharp
public void SelectColor(string colorName)
{
    var colorElement = MainWindow.FindFirstDescendant(cf =>  // ? UI locator
        cf.ByName(colorName).And(cf.ByControlType(ControlType.Button)));
    
    colorElement?.Click();  // ? UI interaction
    Thread.Sleep(200);
}

public string GetPrimaryColor()
{
    var colorDisplay = MainWindow.FindFirstDescendant(cf =>  // ? UI locator
        cf.ByAutomationId("PrimaryColorDisplay"));
    
    return colorDisplay?.Name ?? "Unknown";  // ? Returns data, no assertion
}
```

## 📌 Summary

**Remember: Step Definitions orchestrate WHAT to test, Page Objects implement HOW to test it.**

This separation ensures:
- Easy maintenance when UI changes
- Clear, readable test scenarios
- Reusable UI interaction code
- Testable components
