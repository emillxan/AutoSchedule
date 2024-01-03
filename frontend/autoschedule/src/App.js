import './App.css';
import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Cabinets from './views/Cabinets';
import Squads from './views/Squads';
import Teachers from './views/Teachers';
import Subjects from './views/Subjects';
import Lessons from './views/Lessons';


function App() {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li><Link to="/cabinets">Кабинеты</Link></li>
            <li><Link to="/squads">Группы</Link></li>
            <li><Link to="/teachers">Учителя</Link></li>
            <li><Link to="/subjects">Предметы</Link></li>
            <li><Link to="/lessons">Уроки</Link></li>
          </ul>
        </nav>

        <Routes className="container">
          <Route path="/cabinets" element={<Cabinets />} />
          <Route path="/squads" element={<Squads />} />
          <Route path="/teachers" element={<Teachers />} />
          <Route path="/subjects" element={<Subjects />} />
          <Route path="/lessons" element={<Lessons />} />
          <Route path="/" element={<div>Добро пожаловать в приложение AutoSchedule</div>} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
