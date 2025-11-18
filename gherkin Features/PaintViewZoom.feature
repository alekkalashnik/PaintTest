@view-zoom
Feature: Paint View and Zoom Operations
    As a user
    I want to control the view and zoom level
    So that I can work on details or see the full picture

    Background:
        Given I have launched Paint application

    @view @zoom
    Scenario: Zoom in on canvas
        When I press "Ctrl++" to zoom in
        Then the zoom level should increase

    @view @zoom
    Scenario: Zoom out on canvas
        When I press "Ctrl+-" to zoom out
        Then the zoom level should decrease

    @view @zoom
    Scenario: Reset zoom to 100%
        When I press "Ctrl++" to zoom in 3 times
        And I press "Ctrl+0" to reset zoom
        Then the zoom level should be 100%

    @view @zoom
    Scenario Outline: Set specific zoom level
        When I set zoom level to "<zoom_level>"
        Then the zoom level should be "<zoom_level>"

        Examples:
        | zoom_level |
        | 50%        |
        | 100%       |
        | 200%       |
        | 400%       |

    @view
    Scenario: Toggle full screen mode
        When I press "F11" to toggle full screen
        Then the application should be in full screen mode

    @view
    Scenario: Show or hide rulers
        When I click on "View" menu
        And I toggle "Rulers" option
        Then rulers should be displayed on canvas edges

    @view
    Scenario: Show or hide gridlines
        When I click on "View" menu
        And I toggle "Gridlines" option
        Then gridlines should be displayed on canvas

    @view
    Scenario: Toggle status bar visibility
        When I click on "View" menu
        And I toggle "Status bar" option
        Then the status bar should be hidden or shown

    @view @zoom
    Scenario: Zoom using magnifier tool
        When I select "Magnifier" tool
        And I click on canvas at 100,100
        Then the view should be zoomed in at that location

    @view
    Scenario: Thumbnail view in corner
        When I enable thumbnail preview
        Then a small thumbnail of the full canvas should be displayed