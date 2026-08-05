# Artisanal Bakery POS

A lightweight web-based Point of Sale system built with ASP.NET Core MVC for managing bakery sales, inventory, and order receipts.

## Overview
This application serves as an in-store POS interface designed to handle quick checkout operations. It runs completely on server-side logic without external database dependencies or client-side JavaScript.

### Core Capabilities
* **Product Catalog:** Browse items, live prices, and current stock levels. Out-of-stock items are automatically disabled.
* **Shopping Cart:** Add items using formatted DTO forms, adjust line quantities, or clear selected items.
* **Checkout Flow:** Capture customer information, validate tendered payment against cart totals, and calculate change due.
* **Inventory Deduction:** Automatically updates available product stock in memory once a payment processes successfully.
* **Sales Records:** Complete transaction log with detailed receipts for past orders.

## Architecture
* **Framework:** ASP.NET Core MVC (.NET 8)
* **Data Flow:** Domain models (`Product`, `CartItem`, `ShoppingCart`, `Transaction`) remain decoupled from input forms (`AddToCartDTO`, `CheckoutFormDTO`).
* **Data Persistence:** In-memory state managed via `StaticDataStore`.
* **Validation:** Server-side evaluation using Data Annotations with explicit model-state feedback.

## Running the Project locally

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or Visual Studio 2022+