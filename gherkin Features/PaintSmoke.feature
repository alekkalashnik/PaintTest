@smoke
Feature: Paint Smoke Tests
    Quick smoke tests to verify Paint application basics

    Scenario: Quick Paint launch test
        When I launch the Paint application
        Then Paint application should open successfully
        Then The window title should contain "Untitled"
        And The Paint canvas should be visible
@critical
    Scenario: Application can be closed gracefully
        Given I have launched Paint application
        When I close the Paint application
        Then Paint application should be closed successfully