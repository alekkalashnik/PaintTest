@file-operations
Feature: Paint File Operations
    As a user
    I want to save, open, and manage my Paint files
    So that I can work with my drawings across sessions

    Background:
      
        Given I have launched Paint application
       

    @critical @smoke @save
    Scenario: Save a new drawing
        When I launch the Paint application
        Then The window title should contain "Default"
        When I draw a line from 400,400 to 600,600
        And I draw a line from 600,600 to 800,400
        When I save the file as "GIF" with "Test" name
        Then the Save dialog should be displayed

    @critical @save
    Scenario Outline: Save in different formats
        When I draw a line from 400,400 to 600,600
        When I save the file as "<format>" with "<Testname>" name
        When I open the file with name "<Testname>"

        Examples:
        | format | Testname |   
        | PNG    | testPNG  |
        | JPEG   | testJPEG |
        | GIF    | testGIF  |
        | BMP    | testBMP  |
        

    @critical @open
    Scenario: Open an existing image file
        When I open the file with name "Default Image"
        Then The canvas should contain "Default Image" with drawn content

    @critical @new
    Scenario: New canvas without saving changes prompts warning
        When I draw a line from 400,400 to 600,600
        And I click on "File" menu button
        And I click on "New" menu button
        Then a warning dialog should appear asking to save changes

    @file-operations
    Scenario: Recent files list
        When I click on "File" menu button
        And I click on "Recent" menu button
        Then the Recent menu should display recent files section

    @file-operations
    Scenario: Canvas properties and resize
        When I click on "File" menu button
        And I click on "Properties" or "Resize" menu item
        Then the properties dialog should be displayed