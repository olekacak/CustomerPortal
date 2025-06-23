# CustomerPortal

A Blazor Server application built with .NET and Syncfusion UI components, using a MySQL backend for managing customer data.

---

## 📦 Features

- ✅ User registration and login (with soft delete logic)
- ✅ Customer CRUD operations
- ✅ Dashboard with filtering and charting
- ✅ REST API (via ASP.NET Core)
- ✅ Syncfusion UI integration
- ✅ MySQL 8.0 database support

---

## 🛠️ Tech Stack

- **Frontend**: Blazor Server (.NET 7)
- **Backend**: ASP.NET Core Web API
- **Database**: MySQL
- **UI Components**: Syncfusion Blazor
- **Authentication**: Session-based

---

## Getting Started


### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://www.mysql.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with Blazor Server support


### Setup Instructions

1. **Clone or extract the repository**:
    ```bash
    git clone https://github.com/olekacak/CustomerPortal.git
    cd CustomerPortal
    ```

2. **Import the SQL schema**:
    - Use the `customerdb.sql` file located in the root to set up the database schema in MySQL.

3. **Update connection string**:
    - Update your database connection string in `appsettings.json`.

4. **Run the project**:
    ```bash
    dotnet run --project CustomerPortal/CustomerPortal.csproj
    ```

5. **Access the app**:
    - Navigate to `https://localhost:7026` in your browser.


## Credits

- [Syncfusion Blazor](https://www.syncfusion.com/blazor-components)
- [Pomelo Entity Framework Core Provider for MySQL](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
"""

## 🧪 API Endpoints
- POST /api/customer/register — Register or update a customer
- POST /api/customer/login — Login and check active status
- GET /api/customer/getCustomer — List or search customers
- POST /api/customer/deleteAccount — Soft-delete customer
- GET /api/customer/getCustomerData — Dashboard metrics


## License

This project is licensed under the terms of the included `LICENSE` file.
