# Northwind RESTful API (ASP.NET Core + EF Core + Azure SQL)

This repository contains the final tasks of the course project.
The goal was to build a **RESTful Web API** using **ASP.NET Core** and **Entity Framework Core** with an **Azure SQL Database** (Northwind).
The API is designed to be tested with **Swagger** or **Postman**, and later consumed by a **React frontend**.

---

## 📌 Project Goals

* Connect an ASP.NET Core application to an **Azure SQL Server** database.
* Scaffold the **Northwind** database context and entity models with **Entity Framework Core**.
* Implement **CRUD operations** for key tables:

  * `Customers`
  * `Products`
  * `Employees`
* Demonstrate **REST API best practices**:

  * Retrieve by primary key
  * Retrieve all records
  * Filter by non-primary fields
  * Create (POST)
  * Update (PUT)
  * Delete (DELETE)
* Avoid issues with recursive relationships in `Employees` (`ReportsTo`) and large binary data fields (`Photo`, `Kuva`).

---

## ⚙️ Technical Setup

* **Framework:** ASP.NET Core 8
* **ORM:** Entity Framework Core 8
* **Database:** Azure SQL Server (Northwind sample)
* **Tools:** dotnet-ef, Swagger (Swashbuckle), Postman

---

## 🚀 Implemented Features

### CustomersController

* `GET /api/customers` → Get all customers
* `GET /api/customers/{id}` → Get one customer by ID
* `GET /api/customers/company/{search}` → Search by company name
* `POST /api/customers` → Add a new customer
* `PUT /api/customers/{id}` → Update customer
* `DELETE /api/customers/{id}` → Delete customer

### ProductsController

* `GET /api/products` → Get all products
* `GET /api/products/{id}` → Get one product by ID
* `GET /api/products/category/{id}` → Get products by category
* `POST /api/products` → Add a new product
* `PUT /api/products/{id}` → Update product
* `DELETE /api/products/{id}` → Delete product

### EmployeesController

* `GET /api/employees` → Get all employees
* `GET /api/employees/{id}` → Get one employee by ID
* `GET /api/employees/city/{city}` → Get employees by city
* `POST /api/employees` → Add a new employee
* `PUT /api/employees/{id}` → Update employee
* `DELETE /api/employees/{id}` → Delete employee

⚠️ To prevent infinite JSON loops and oversized responses:

* `ReportsTo` values were set to `NULL`
* `Photo` and `Kuva` fields were nulled

---

## 🔍 Testing

* **Swagger** is enabled by default (`/swagger/index.html`)
* API can also be tested via **Postman**

---

## 📚 Future Use

This API will be consumed in the **React course project** to demonstrate frontend–backend communication with real data.

---
