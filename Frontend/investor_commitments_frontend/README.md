# Investor Commitments Frontend

This Application is the frontend part of the Technical test. It is a React + TypeScript application built with Vite. This frontend consumes a REST API served by the [Investor Commitments Backend](../../Backend/InvestorCommitments/README.MD).

## 📁 Project Structure

```
/investor_commitments_frontend/
├── src/
│ ├── pages/ # Page-level components (Investors, InvestorCommitments)
│ ├── models/ # Objects used in the components and API calls
│ ├── App.tsx # Root component with routing
│ ├── main.tsx # App entry point
│ └── public/ # Static assets
└── index.html # HTML template
```

## 🚀 Running the Application

### 1. Install Dependencies

Run the following commands in terminal:

```bash
cd Frontend/investor_commitments_frontend
npm install
```

### 2. Run the App

Run the following command in terminal:

```bash
npm run dev
```

The app will be available at:  
`http://localhost:5173` (default Vite port)

> Make sure the backend API is running at `http://localhost:8080`
