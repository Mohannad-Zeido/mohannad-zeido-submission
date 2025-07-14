# 💼 Investor Commitments – Technical Assessment Submission

This repository contains a full-stack application that demonstrates ingestion, storage, and display of investor commitments data. It has been developed as part of a **technical test submission** by **Mohannad Zeido**.

## 📁 Project Structure

```
.
├── Backend/ # ASP.NET Core 9 API (Investor + Commitment management)
├── Frontend/ # React + Vite + TypeScript frontend (Ant Design)
├── DataIngestionScript/ # Python script for CSV ingestion into SQLite
└── docker-compose.yaml # Container orchestration for backend and frontend
```

## 🔧 Overview of Components

### 🧠 Backend (`Backend/`) [README](Backend/InvestorCommitments/README.MD)

- Built in **.NET 9**
- Provides RESTful API for:
- retrieving investors
- retrieving investor commitments
- Uses **SQLite** as a lightweight local database
- API is documented using **Swagger**

### 💻 Frontend (`Frontend/`) [README](Frontend/investor_commitments_frontend/README.md)

- Built using **React**, **Vite**, and **TypeScript**
- UI styled with **Ant Design**
- Lists investors and their commitments

### 🐍 Data Ingestion Script (`DataIngestionScript/`) [README](DataIngestionScript/README.md)

- Python script that reads a CSV file and populates the SQLite database

### 🐳 Docker Compose (`docker-compose.yaml`)

Manages both frontend and backend services using Docker.

To spin up the stack locally run the following:

```bash
docker-compose up --build
```

## 📌 Notes

- The SQLite database is lightweight and suitable for demo purposes. In a real production setting, a managed relational database (e.g., PostgreSQL) should be used.
- The system assumes investor names are unique (enforced by a `UNIQUE(name)` constraint in SQLite).

## ✅ Deliverables

- Backend REST API with SQLite database
- Frontend dashboard with React + Ant Design
- Data ingestion script in Python
- Dockerized setup with `docker-compose.yaml`
