import sqlite3
import pandas as pd
import sys

df = pd.read_csv("data.csv")

# I want to store the DB file in the Backend Server so using that path here.
# Realistically that server would either expose endpoints to accept the data and store it in an actual DB like Postgres.
# But for the sake of this assessment I created a simple ingestion script
conn = sqlite3.connect("../Backend/InvestorCommitments/Database/investor_commitments.db")
cursor = conn.cursor()

cursor.execute('''
               CREATE TABLE IF NOT EXISTS investors (
                                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        name TEXT,
                                                        investoryType TEXT,
                                                        country TEXT,
                                                        dateAdded TEXT,
                                                        lastUpdated TEXT,
                                                        UNIQUE(name)
                   )
               ''')

cursor.execute('''
               CREATE TABLE IF NOT EXISTS commitments (
                                                          id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                          investorId INTEGER,
                                                          assetClass TEXT,
                                                          amount REAL,
                                                          currency TEXT,
                                                          FOREIGN KEY (investorId) REFERENCES investors(id)
                   )
               ''')

for _, row in df.iterrows():
    cursor.execute('''
                   INSERT OR IGNORE INTO investors (
        name, investoryType, country, dateAdded, lastUpdated
    ) VALUES (?, ?, ?, ?, ?)
                   ''', (
                       row['Investor Name'], row['Investory Type'], row['Investor Country'],
                       row['Investor Date Added'], row['Investor Last Updated']
                   ))

    cursor.execute('''
                   SELECT id FROM investors WHERE
                       name = ?
                   ''', (row['Investor Name'],))
    investor_id = cursor.fetchone()[0]

    cursor.execute('''
                   INSERT OR IGNORE INTO commitments (
                       investorId, assetClass, amount, currency
                   ) VALUES (?, ?, ?, ?)
                   ''', (
                       investor_id,
                       row['Commitment Asset Class'],
                       row['Commitment Amount'],
                       row['Commitment Currency']
                   ))


conn.commit()

cursor.execute("SELECT COUNT(*) FROM commitments")
total_commitments = cursor.fetchone()[0]

conn.close()

# A simple check to make sure we stored all the data
# Because of assumption 1 (please look at readme) I am able to check that we have the same number of commitments as rows in the csv file
total_of_rows_in_csv = len(df)
if total_commitments != total_of_rows_in_csv:
    print(f"Ingestion error: CSV has {total_of_rows_in_csv} total commitments, Commitments table has {total_commitments}")
    sys.exit("Aborting script due to data count mismatch.")

print(f"Data ingestion Completed Successfully. Total Commitments: {total_commitments}")


