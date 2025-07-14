# Investor Commitments Backend

This Application is the backend part of the Technical test. It is a .Net9 Web API.

## 📁 Project Structure

```bash
/InvestorCommitments.API/            # Main Web API project
/InvestorCommitments.Infrastructure/ # Infrastructure layer (DB access, Repositories)
/InvestorCommitments.Test/           # Test project
/Database/                           # SQLite .db file directory
```

## 🚀 Running the Application

Create an `appsettings.development.json` in the API project root (AKA in the InvestorCommitments.API/ Directory). And paste the following:

```json
{
  "DatabaseOptions": {
    "FilePath": "../Database/investor_commitments.db"
  }
}
```

Run the following commands in terminal:

```bash
cd Backend/InvestorCommitments/InvestorCommitments.API
dotnet build
dotnet run
```

The API will be available at: `http://localhost:8080`

## 📬 API Endpoints

| Method | Endpoint             | Description                         |
| ------ | -------------------- | ----------------------------------- |
| GET    | `/api/investors`     | List all investors                  |
| GET    | `/api/investor/{id}` | Get all commitments for an investor |

Swagger UI: `http://localhost:8080/swagger`

## 🧪 Testing

Tests are located in the `InvestorCommitments.Test` project.

Run them with:

```bash
dotnet test
```

## 🗃️ Database Schema

```sql
CREATE TABLE IF NOT EXISTS investors (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT,
    investoryType TEXT,
    country TEXT,
    dateAdded TEXT,
    lastUpdated TEXT,
    UNIQUE(name)
);

CREATE TABLE IF NOT EXISTS commitments (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    investorId INTEGER,
    assetClass TEXT,
    amount REAL,
    currency TEXT,
    FOREIGN KEY (investorId) REFERENCES investors(id)
);
```
