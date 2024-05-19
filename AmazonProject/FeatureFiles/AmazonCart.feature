Feature: Amazon Shopping
    As an unregistered user
    I want to search for a specific item
    So that I can add it to my cart and validate the cart contents

Scenario: Add item to cart and validate
    Given I am on the Amazon homepage as an unregistered user
    When I search for "TP-Link N450 WiFi Router - Wireless Internet Router for Home (TL-WR940N)"
    And I add the corresponding item to the cart
    And I navigate to the cart
    Then I validate that the correct item and amount are displayed