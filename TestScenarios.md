# Paint Application Test Scenarios

This document provides an overview of all BDD test scenarios for Microsoft Paint application testing.

## Feature Files Overview

### 1. **PaintSmoke.feature** - Smoke Tests (@smoke)
Quick critical tests to verify basic application functionality:
- ✅ Quick Paint launch test
- ✅ File menu accessibility
- ✅ Application graceful closure

### 2. **PaintApplication.feature** - Launch and Basic Operations (@smoke, @ui, @critical)
Core application launch and window management:
- ✅ Launch Paint successfully
- ✅ Verify canvas visibility
- ✅ Maximize/minimize/restore window
- ✅ Window state management

### 3. **PaintDrawing.feature** - Drawing Operations (@drawing)
Drawing functionality with various tools:
- ✅ Draw simple lines with pencil
- ✅ Draw with brush tool
- ✅ Use eraser tool
- ✅ Draw with different tools (pencil, brush, marker)
- ⏳ Draw multiple shapes (rectangles, etc.)
- ⏳ Clear canvas operations

### 4. **PaintTools.feature** - Tools and Toolbar (@tools)
Tool selection and configuration:
- ✅ Verify basic drawing tools availability
- ✅ Switch between tools
- ⏳ Select colors from palette
- ⏳ Change brush size
- ⏳ Use shape tools
- ⏳ Text tool operations

### 5. **PaintFileOperations.feature** - File Operations (@file-operations, @save, @open, @new)
File management operations:
- ⏳ Save new drawings
- ⏳ Save with specific filename
- ⏳ Save in different formats (PNG, JPG, BMP)
- ⏳ Open existing image files
- ⏳ Create new blank canvas
- ⏳ Handle unsaved changes warnings
- ⏳ Recent files list
- ⏳ Canvas properties and resize

### 6. **PaintEditOperations.feature** - Edit Operations (@edit, @undo-redo, @select)
Content editing and manipulation:
- ⏳ Undo/redo operations
- ⏳ Multiple undo operations
- ⏳ Select all content
- ⏳ Cut, copy, paste operations
- ⏳ Select region and delete
- ⏳ Rotate canvas content
- ⏳ Flip canvas content
- ⏳ Resize selections
- ⏳ Crop canvas to selection

### 7. **PaintViewZoom.feature** - View and Zoom (@view, @zoom)
View controls and zoom operations:
- ⏳ Zoom in/out operations
- ⏳ Reset zoom to 100%
- ⏳ Set specific zoom levels
- ⏳ Full screen mode toggle
- ⏳ Show/hide rulers
- ⏳ Show/hide gridlines
- ⏳ Toggle status bar
- ⏳ Magnifier tool usage
- ⏳ Thumbnail view

## Test Categories (Tags)

| Tag | Description | Count | Status |
|-----|-------------|-------|--------|
| @smoke | Critical smoke tests | 5 | ✅ Complete |
| @critical | Must-pass critical tests | 1 | ✅ Complete |
| @ui | User interface tests | 4 | ✅ Complete |
| @drawing | Drawing operations | 7 | 🔄 In Progress |
| @tools | Tool selection and usage | 7 | 🔄 In Progress |
| @file-operations | File save/open operations | 8 | ⏳ Pending |
| @save | Save operations | 3 | ⏳ Pending |
| @open | Open operations | 1 | ⏳ Pending |
| @new | New canvas operations | 2 | ⏳ Pending |
| @edit | Edit operations | 11 | ⏳ Pending |
| @undo-redo | Undo/redo functionality | 3 | ⏳ Pending |
| @select | Selection operations | 4 | ⏳ Pending |
| @view | View controls | 6 | ⏳ Pending |
| @zoom | Zoom operations | 5 | ⏳ Pending |

## Test Execution Priority

### Priority 1 - Smoke Tests (Run First) ✅
```bash
dotnet test --filter "Category=smoke"
```

### Priority 2 - Critical Tests
```bash
dotnet test --filter "Category=critical"
```

### Priority 3 - Feature-Specific Tests
```bash
dotnet test --filter "Category=drawing"
dotnet test --filter "Category=tools"
dotnet test --filter "Category=file-operations"
```

### Priority 4 - Advanced Features
```bash
dotnet test --filter "Category=edit"
dotnet test --filter "Category=view"
```

## Total Test Coverage

- **Total Features**: 7
- **Total Scenarios**: ~50+
- **Test Categories**: 14 tags

## Implementation Status

### Implemented Step Definitions
- ? Launch Paint application
- ? Window title verification
- ? Canvas visibility check
- ? Window state management (maximize, minimize, normal)
- ? Tool selection
- ? Drawing lines
- ? Button clicks

### Pending Step Definitions
- ? File menu operations
- ? Save/Open dialog handling
- ? Color selection
- ? Undo/Redo operations
- ? Copy/Paste operations
- ? Zoom controls
- ? View menu operations
- ? Text tool operations
- ? Shape tools
- ? Canvas content verification

## Next Steps

1. Implement missing step definitions in `PaintApplicationSteps.cs` or create new step definition files:
   - `PaintFileOperationsSteps.cs`
   - `PaintDrawingSteps.cs`
   - `PaintToolsSteps.cs`
   - `PaintEditSteps.cs`
   - `PaintViewSteps.cs`

2. Create page objects for different Paint UI sections:
   - `PaintToolbar.cs`
   - `PaintFileMenu.cs`
   - `PaintColorPalette.cs`

3. Add helper methods for common operations:
   - Canvas content verification
   - File system operations
   - Clipboard operations

4. Configure test execution in CI/CD pipeline with different test categories
