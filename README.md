[README.md](https://github.com/user-attachments/files/26120564/README.md)
# 🛒 POS Management System

A **Point of Sale (POS) Management System** built with **C# Windows Forms** and **Cockroachdb** as the backend database. Designed for small to medium businesses with company-based access for Admins and Cashiers.

---

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Roles](#roles)
- [Getting Started](#getting-started)
- [Database Setup](#database-setup)
- [Project Structure](#project-structure)
- [Contributing](#contributing)

---

## ✨ Features

- 🔐 **Secure Login** with role-based access (Admin / Cashier)
- 🛍️ **Cashier Dashboard** — browse products, add to cart, apply discounts, process payments
- 🛠️ **Admin Dashboard** — manage products, users, and view transaction history
- 💰 **Automatic VAT and discount calculation**
- 🔢 **Auto-generated transaction numbers**
- 🚪 **Logout confirmation** to prevent accidental logouts
- ❌ **Exit confirmation** on close
- 🖱️ **Draggable custom title bar** across all forms

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| C# .NET (WinForms) | Frontend / UI |
| Supabase (PostgreSQL) | Backend database |
| Supabase .NET SDK | Database communication |
| Visual Studio Community | IDE |

---

## 👥 Roles

### Admin
- Full access to product and user management
- View sales reports and transaction history
- Manage cashier accounts

### Cashier
- Browse and search product catalog
- Add items to cart
- Apply discounts
- Process payments and generate receipts

---

## 🚀 Getting Started

### Prerequisites

- [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) (2019 or later)
- [.NET Framework](https://dotnet.microsoft.com/en-us/download) 4.7.2 or later
- A [Supabase](https://supabase.com) account and project

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/pos-management-system.git
   cd pos-management-system
   ```

2. **Open in Visual Studio**
   - Open `POS.sln` in Visual Studio Community

3. **Install Supabase NuGet package**
   ```
   Tools → NuGet Package Manager → Manage NuGet Packages
   Search: supabase-csharp
   Install: Supabase
   ```

4. **Configure Supabase credentials**
   - Create a `SupabaseConfig.cs` file or update your existing config:
   ```csharp
   public static class SupabaseConfig
   {
       public const string Url = "https://your-project.supabase.co";
       public const string Key = "your-anon-public-key";
   }
   ```

5. **Build and Run**
   - Press `F5` or click **Start** in Visual Studio

---

## 🗄️ Database Setup

Run the following SQL in your Supabase **SQL Editor**:

### Users Table
```sql
CREATE TABLE users (
    id       SERIAL PRIMARY KEY,
    username VARCHAR NOT NULL UNIQUE,
    password VARCHAR NOT NULL,
    role     TEXT NOT NULL CHECK (role IN ('ADMIN', 'CASHIER'))
);
```

### Products Table
```sql
CREATE TABLE products (
    id          SERIAL PRIMARY KEY,
    code        VARCHAR NOT NULL UNIQUE,
    description VARCHAR NOT NULL,
    price       NUMERIC(10, 2) NOT NULL
);
```

### Transactions Table
```sql
CREATE TABLE transactions (
    id               SERIAL PRIMARY KEY,
    transaction_no   VARCHAR NOT NULL,
    total            NUMERIC(10, 2),
    discount         NUMERIC(10, 2),
    vat              NUMERIC(10, 2),
    vatable          NUMERIC(10, 2),
    created_at       TIMESTAMP DEFAULT NOW()
);
```

### Sample Data
```sql
INSERT INTO users (username, password, role) VALUES
('geo', 'geobading', 'ADMIN'),
('cashier01', 'ambatucash', 'CASHIER');
```

> ⚠️ **Note:** Passwords in this project are currently stored as plain text for development purposes. It is strongly recommended to implement **password hashing** (e.g. BCrypt) before deploying to production.

---

## 📁 Project Structure

```
POS/
├── Forms/
│   ├── LoginForm.cs          # Login screen with role detection
│   ├── AdminDashboard.cs     # Admin panel
│   └── CashierDashboard.cs   # Cashier POS interface
├── Models/
│   └── Users.cs              # Supabase model for users table
├── Base/
│   └── BaseForm.cs           # Shared draggable title bar + close logic
├── Config/
│   └── SupabaseConfig.cs     # Supabase URL and API key
└── POS.sln
```

---

## 🤝 Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

## 📄 License

This project is for educational purposes. Feel free to use and modify it.

---

<p align="center">Made with ❤️ using C# WinForms + Supabase</p>
