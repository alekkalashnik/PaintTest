Feature: Paint Application Launch and Basic Operations
    As a user
    I want to launch Microsoft Paint application
    So that I can verify it opens correctly and is ready to use

    @smoke @critical
    Scenario: Launch Paint application successfully
        Given I have launched Paint application
        Then Paint application should open successfully
        And The window title should contain "Untitled"
        And The Paint canvas should be visible
  
    @ui @critical
    Scenario Outline: Change window state
        Given I have launched Paint application
        When I change the window state to "Normal"
        Then the Paint window should be in "Normal" state
        When I change the window state to "Maximized"
        Then the Paint window should be in "Maximized" state

        Examples:
        | state     |
        | Maximized |
        | Normal    |

    @ui @critical
    Scenario: Minimize and restore Paint window
        Given I have launched Paint application
        When I change the window state to "Minimized"
        And I change the window state to "Normal"
        Then the Paint window should be in "Normal" state