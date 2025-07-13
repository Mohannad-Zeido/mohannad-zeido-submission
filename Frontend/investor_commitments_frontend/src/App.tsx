import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Investors from './pages/investors/Investors.tsx';
import InvestorCommitments from './pages/investor_commitments/InvestorCommitments.tsx';
import "./App.css"

function App() {

  return (
      <Router>
          <Routes>
              <Route path="/" element={<Investors />} />
              <Route path="/investors" element={<Investors />} />
              <Route path="/investor-commitments/:id" element={<InvestorCommitments />} />
          </Routes>
      </Router>
  )
}

export default App
