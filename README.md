# Coolblue Insurance API

This project is the assignment for Coolblue recruitment process.

## Current API Version
Version 1.0

## Contributors
Name : Venkata Satyanarayana Addanki.

Date : 25-04-2022

## Installation
1. Check out master branch
2. Build the solution

## Usage
Run application.

The page that is opened by default is swagger page. From here, all
endpoints can be tested.

## Framework
All projects are configured to use .NET Core 3.1

## Project Structure
* WebApi: Layer in charge of handling request and responses.
* Infrastructure: Layer in charge of executing business logic.
* Domain: Layer in charge of exposing entities.
* Application: Layer in charge of maintaining abstractions for business logic
* WebApi.Test: Integration tests for API.

# Work proposal
## Task 2 [REFACTORING]:
* Applied Solid principles
* Self documenting code
* Naming conventions
* Remove unused namespace
* Project and File names
* Avoid magic strings

## Task 3 [FEATURE 1]:
Create an endpoint that will accept order that contains list of products
Calculate each product insurance and provide the total insurance of all products in the order

## Task 4 [FEATURE 2]:
Endpoint will add extra 500 euro to the camera insured amount.
we need to make sure only 500 euro need to be add for multiple cameras in an order

## Task 5 [FEATURE 3]:
Create an endpoint that will add surcharge rates to the Product types api
we need to fetch the surcharge rate of the respective product type from product type endpoint and add to the insurance value in insurance endpoint
