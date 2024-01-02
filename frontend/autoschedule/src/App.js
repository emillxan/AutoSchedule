import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import { useEffect, useState } from 'react';

function App() {
  // const [lessonData, setLessonData] = useState([]);

  // useEffect(() => {
  //   axios.get(`https://localhost:7168/WeatherForecast`)
  //     .then(res => {
  //       console.log(res.data);
  //       setLessonData(res.data);
  //     })
  //     .catch(error => {
  //       console.error('Error fetching data:', error);
  //     });
  // }, []); 
  const [tasks, setTasks] = useState([]);
    
  useEffect(() => {
    axios.get(`https://localhost:7168/WeatherForecast`)
      .then(res => {
        console.log(res.data);
        setTasks(res.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []); 


  return (
    <div className="App">
      <header className="App-header">
        {tasks.map((order, index) => (
          // <div>{order.squad.id}</div>
          <div>Group: {order.squad.number} || {order.cabinet.number} || {order.teacher.name} || {order.time.substring(11,19)}</div>
        ))}
      </header>
    </div>
  );
}

export default App;
