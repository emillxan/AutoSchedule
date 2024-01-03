import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import { useEffect, useState } from 'react';
import Schedule from './component/Schedule';

function App() {
  const [tasks, setTasks] = useState([]);
    
  useEffect(() => {
    axios.get(`https://localhost:7168/WeatherForecast`)
      .then(res => {
        // console.log(res.data);
        setTasks(res.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []); 


  return (
    <div className="App">
      <header className="App-header">
       <Schedule></Schedule>
      </header>
    </div>
  );
}

export default App;
