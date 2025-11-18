@drawing
Feature: Paint Drawing Operations
    As a user
    I want to draw on the Paint canvas
    So that I can create simple graphics

    Background:
        Given I have launched Paint application

    @critical @smoke @drawing
    Scenario: Draw with brush tool
        When I draw a line from 400,400 to 600,600
        Then The canvas should contain "Brush_Drawing" with drawn content

    @critical @drawing
    Scenario: Use eraser tool
        When I select "Fill" tool
        When I click on the canvas by coordinates 600,600
        Then The canvas should contain "Filled_Drawing" with drawn content
        When I select "Eraser" tool
        And I click on the canvas by coordinates 400,400
        And I click on the canvas by coordinates 600,600
        Then The canvas should contain "Brush_Drawing_With_Erased_Spots" with drawn content

    @critical @drawing
    Scenario Outline: Draw with different tools
        When I select "Pencil" tool
        And I draw a line from 400,400 to 600,600
        And I draw a line from 600,600 to 800,400
        Then The canvas should contain "Test_Drawing" with drawn content

        Examples:
        | tool    |
        | Pencil  |
        | Brush   |
        | Marker  |


    @critical @drawing
    Scenario: Clear canvas using Select All and Delete
        When I draw a line from 400,400 to 600,600
        And I draw a line from 600,600 to 800,400
        Then The canvas should contain "Test_Drawing_Lines" with drawn content
        When I press "CONTROL" + "KEY_A" shortcut
        And I press "Delete" key
        Then The canvas should contain "Empty_Drawing" with drawn content
