# Test Specification

## 1. Scope

This submission demonstrates a small C#/.NET test automation framework covering:

- UI automation against `https://www.saucedemo.com/`
- API automation against `https://jsonplaceholder.typicode.com/`
- Postman validation for two JSONPlaceholder endpoints
- One SQL query for the W3Schools Northwind dataset

The goal is not broad coverage. The focus is framework structure, maintainability, separation of concerns, and clear reasoning.

---

## 2. Assumptions

- SauceDemo is available and the standard public test account is usable.
- UI tests run against Chrome by default.
- JSONPlaceholder returns stable public test data.
- API contract validation focuses on required fields and expected data types/values relevant to the selected resource.
- Tests should be independent and should not rely on execution order.
- No production secrets are required for this task.
- SpecFlow is intentionally not included because it is optional and would add unnecessary complexity for a one-hour assignment.

---

## 3. UI Requirements

### UI-REQ-01 — User can log in

Given a valid SauceDemo user  
When the user submits valid credentials  
Then the inventory page should be displayed.

### UI-REQ-02 — User can add an item to the cart

Given an authenticated user on the inventory page  
When the user adds a product to the cart  
And opens the cart  
Then the selected product should be present in the cart.

---

## 4. UI Test Scenarios

### UI-TC-01 — Valid login

**Requirement:** UI-REQ-01

Steps:
1. Open SauceDemo.
2. Enter valid username.
3. Enter valid password.
4. Submit login.

Expected:
- User is redirected to the inventory page.
- Inventory content is visible.

### UI-TC-02 — Add product to cart

**Requirement:** UI-REQ-02

Steps:
1. Log in with a valid user.
2. Add one product to the cart.
3. Open the cart.

Expected:
- Cart contains the selected product.
- Product name matches the product added from inventory.

---

## 5. API Requirements

### API-REQ-01 — Resource request returns success

Given the JSONPlaceholder API  
When a valid resource is requested  
Then the response status should be HTTP 200.

### API-REQ-02 — Resource response matches the expected contract

Given a successful API response  
When the response body is deserialized  
Then required contract fields should be present and contain valid values.

---

## 6. API Test Scenarios

### API-TC-01 — GET user resource

Request:

`GET /users/1`

Expected:
- HTTP status code is 200.
- Response can be deserialized into the expected user model.
- Required properties are populated.
- Returned user ID matches the requested resource.

### API-TC-02 — GET post resource

Request:

`GET /posts/1`

Expected:
- HTTP status code is 200.
- Response can be deserialized into the expected post model.
- Required properties are populated.
- Returned post ID matches the requested resource.

---

## 7. Postman Requirements

The Postman collection must contain:

### PM-REQ-01

`GET {{baseUrl}}/users`

Assertions:
- Status code is 200.
- Response is an array.
- Every returned user has required key properties populated.

### PM-REQ-02

`GET {{baseUrl}}/posts?userId=1`

Assertions:
- Status code is 200.
- Response is an array.
- Every returned post has required key properties populated.

A Postman environment must define:

`baseUrl = https://jsonplaceholder.typicode.com`

---

## 8. SQL Requirement

Return customer names and countries for customers whose orders were shipped using `United Package`.

The query should join the relevant Northwind tables rather than rely on hard-coded IDs where avoidable.

---

## 9. Non-Functional Expectations

- Tests should be readable and maintainable.
- UI selectors should be isolated in Page Objects.
- API HTTP logic should be isolated in API client classes.
- Models should represent API response contracts.
- Test methods should express intent and avoid low-level implementation details.
- No hard-coded sleeps.
- Tests should remain independent.
- Configuration should be centralized rather than scattered across tests.

---

## 10. Out of Scope for This Submission

Due to the one-hour limit, the following are intentionally not implemented unless time remains:

- SpecFlow
- Docker execution
- Selenium Grid
- WireMock
- CI workflow
- advanced reporting
- retry policies
- test data builders/factories
- schema validation library

These would be considered for a long-lived production framework and are described in the README.