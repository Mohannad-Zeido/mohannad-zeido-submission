# Investor Commitments CSV Ingestion Script

This Python script ingests investor commitment data from a CSV file into a SQLite database used by the Investor Commitments Backend.

❗️**IMPORTANT:** The database already exists and this script does not need to be run.

## 📄 Script: `ingest_commitment_csv_file.py`

The script reads a `data.csv` file and:

1. Creates two tables if they don't exist:

- `investors`
- `commitments`

2. Inserts unique investors into the `investors` table.
3. Adds associated commitments to the `commitments` table.
4. Performs a data integrity check to ensure all rows from the CSV have been ingested.

## 📦 Requirements

Install required packages:

```bash
pip install pandas
```

## 📁 CSV Format

The input file must be named `data.csv` and contain the following columns:

- `Investor Name`
- `Investory Type`
- `Investor Country`
- `Investor Date Added`
- `Investor Last Updated`
- `Commitment Asset Class`
- `Commitment Amount`
- `Commitment Currency`

## 🗃️ Database Schema

```sql
CREATE TABLE investors (
id INTEGER PRIMARY KEY AUTOINCREMENT,
name TEXT,
investoryType TEXT,
country TEXT,
dateAdded TEXT,
lastUpdated TEXT,
UNIQUE(name)
);

CREATE TABLE commitments (
id INTEGER PRIMARY KEY AUTOINCREMENT,
investorId INTEGER,
assetClass TEXT,
amount REAL,
currency TEXT,
FOREIGN KEY (investorId) REFERENCES investors(id)
);
```

## 📍 Database Location

The SQLite database file is created at:

```
../Backend/InvestorCommitments/Database/investor_commitments.db
```

> Make sure the parent directories exist or modify the path in the script accordingly.

## 🚀 Usage

Run the script:

```bash
python ingest_commitment_csv_file.py
```

If all data is ingested successfully, you’ll see:

```
Data ingestion Completed Successfully. Total Commitments: X
```

If there is a mismatch between the number of CSV rows and ingested records:

```
Ingestion error: CSV has X total commitments, Commitments table has Y
Aborting script due to data count mismatch.
```

## ⚠️ Assumptions

- Investor names are unique in the dataset.
- Every row in the CSV represents one commitment.
- The script is meant for local ingestion during development/testing. In production, this functionality should be exposed via an API or replaced by a pipeline to a persistent database like PostgreSQL.
- "Duplicate" commitments rows wil be treated as a new commitment and not considered an artifact of bad data.
- From basic examination of the csv file the data seems cleand and does not contain errors. As a result no error handling or data cleanin has been added.
