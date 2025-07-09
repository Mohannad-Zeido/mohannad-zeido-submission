import sqlite3
import pandas as pd
import sys

df = pd.read_csv("data.csv")

# I want to store the DB file in the Backend Server so using that path here.
# Realistically that server would either expose endpoints to accept the data and store it in an actual DB like Postgres.
# But for the sake of this assessment I created a simple ingestion script
conn = sqlite3.connect("../Backend/InvestorCommitments.API/Database/investor_commitments.db")
cursor = conn.cursor()

cursor.execute('''
               CREATE TABLE IF NOT EXISTS investors (
                                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                        investor_name TEXT,
                                                        investory_type TEXT,
                                                        investor_country TEXT,
                                                        investor_date_added TEXT,
                                                        investor_last_updated TEXT,
                                                        UNIQUE(investor_name)
                   )
               ''')

# Create commitments table
cursor.execute('''
               CREATE TABLE IF NOT EXISTS commitments (
                                                          id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                          investor_id INTEGER,
                                                          commitment_asset_class TEXT,
                                                          commitment_amount REAL,
                                                          commitment_currency TEXT,
                                                          FOREIGN KEY (investor_id) REFERENCES investors(id)
                   )
               ''')

for _, row in df.iterrows():
    cursor.execute('''
                   INSERT OR IGNORE INTO investors (
        investor_name, investory_type, investor_country, investor_date_added, investor_last_updated
    ) VALUES (?, ?, ?, ?, ?)
                   ''', (
                       row['Investor Name'], row['Investory Type'], row['Investor Country'],
                       row['Investor Date Added'], row['Investor Last Updated']
                   ))

    cursor.execute('''
                   SELECT id FROM investors WHERE
                       investor_name = ?
                   ''', (row['Investor Name'],))
    investor_id = cursor.fetchone()[0]

    cursor.execute('''
                   INSERT OR IGNORE INTO commitments (
                       investor_id, commitment_asset_class, commitment_amount, commitment_currency
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


