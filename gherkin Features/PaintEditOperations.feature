@edit-operations
Feature: Paint Edit Operations
    As a user
    I want to perform edit operations on my drawings
    So that I can modify and refine my work

    Background:
        Given I have launched Paint application

    @critical @edit @undo-redo
    Scenario: Undo drawing action
        When I select "Fill" tool
        When I click on the canvas by coordinates 400,400
        Then The canvas should contain "Filled" with drawn content
        When I press "CONTROL" + "KEY_Z" shortcut
        Then The canvas should contain "Empty_After_Undo" with drawn content

    @critical @edit @undo-redo
    Scenario: Redo drawing action
        When I select "Pencil" tool
        When I click on the canvas by coordinates 400,400
        And I draw a line from 400,400 to 600,600
        Then The canvas should contain "Line" with drawn content
        When I press "CONTROL" + "KEY_Z" shortcut
        Then The canvas should contain "Empty_After_Undo" with drawn content
        When I press "CONTROL" + "KEY_Y" shortcut
        Then The canvas should contain "Line" with drawn content

    @edit @undo-redo
    Scenario: Multiple undo operations
        When I select "Pencil" tool
        When I click on the canvas by coordinates 400,400
        And I draw a line from 400,400 to 600,600
        And I draw a line from 600,600 to 800,600
        Then The canvas should contain "2Lines" with drawn content
        When I press "CONTROL" + "KEY_Z" shortcut
        When I press "CONTROL" + "KEY_Z" shortcut
        Then The canvas should contain "Empty_After_2Undo" with drawn content
        When I press "CONTROL" + "KEY_Y" shortcut
        Then The canvas should contain "Only_Line" with drawn content

    @critical @edit @select
    Scenario: Select then copy and paste content
        When I select "Fill" tool
        When I click on the canvas by coordinates 400,400
        Then The canvas should contain "Filled2" with drawn content
	    When I select "SelectRadioButton" RadioButton
        When I click on the canvas by coordinates 400,400
        And I draw a line from 400,400 to 500,500
        And I press "CONTROL" + "KEY_C" shortcut
        And I press "CONTROL" + "KEY_Z" shortcut
        And I press "CONTROL" + "KEY_Z" shortcut
        When I press "CONTROL" + "KEY_V" shortcut
        Then The canvas should contain "PastedFromTheSelection" with drawn content