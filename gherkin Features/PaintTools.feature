@tools
Feature: Paint Tools and Toolbar
    As a user
    I want to access and use different Paint tools
    So that I can create various types of drawings

    Background:
        Given I have launched Paint application


    @tools
    Scenario: Switch between tools
        When I select "Pencil" tool
        Then "Pencil" tool should be active
        When I select "Brush" tool
        Then "Brush" tool should be active
        And "Pencil" tool should not be active

    @tools
    Scenario: Select color from color palette
        When I click on primary color selector
        And I select "Red" color
        Then the primary color should be "Red"

    @tools
    Scenario: Change brush size
        When I select "Brush" tool
        And I change brush size to "Large"
        And I draw a line from 100,100 to 200,100
        Then the line should be thick

    @tools
    Scenario Outline: Select different colors
        When I select "<color>" color
        And I select "Pencil" tool
        And I draw a line from 100,100 to 200,100
        Then the line should be in "<color>" color

        Examples:
        | color  |
        | Red    |
        | Blue   |
        | Green  |
        | Black  |

    @tools
    Scenario: Use shape tools
        When I select "Rectangle" shape tool
        And I draw a shape from 100,100 to 200,200
        Then a rectangle should be drawn on canvas

    @tools
    Scenario: Use text tool
        When I select "Text" tool
        And I click on canvas at 100,100
        And I type "Hello Paint"
        Then the text "Hello Paint" should appear on canvas
