import axios from 'axios';
import { useEffect, useState } from 'react';
import Slot from './Slot';

function Schedule() {
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
      <div className="">
        {tasks.map((order, index) => (
          // <div>{order.squad.id}</div>
          //<div>Group: {order.squad.number} || {order.cabinet.number} || {order.subject.name} || {order.teacher.name} || {order.time.substring(8,10)} || {order.time.substring(11,19)}</div>
          (() => {
            switch (order.squad.number) {
                case '2450r':
                    return <p>Значение - A</p>;
                case 'b':
                    return <p>Значение - B</p>;
                default:
                    return <p>Значение неизвестно</p>;
            }
        })()
        ))}
      </div>
    );
  }
  
  export default Schedule;