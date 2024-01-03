import axios from 'axios';
import { useEffect, useState } from 'react';


function Schedule() {
    const [scheduleList, setTasks] = useState([]);
    
  useEffect(() => {
    axios.get(`https://localhost:7168/WeatherForecast/GetLessonsBySquadId?id=1`)
      .then(res => {
        // console.log(res.data);
        setTasks(res.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
    }, []); 

    const uniqueTimes = [...new Set(scheduleList.map(item => new Date(item.time).toLocaleTimeString()))];

    // Получаем расписание, сгруппированное по дням недели и времени
    const scheduleByDay = scheduleList.reduce((acc, item) => {
        const day = item.dayOfWeek;
        const time = new Date(item.time).toLocaleTimeString();

        if (!acc[day]) {
            acc[day] = {};
        }

        if (!acc[day][time]) {
            acc[day][time] = [];
        }

        acc[day][time].push(item);
        return acc;
    }, {});

    const daysOfWeek = ['Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница'];

    const squadNumber = scheduleList.length > 0 ? scheduleList[0].squad.number : 'Неизвестно';

    return (
        <div>
            <h2>Расписание {squadNumber}</h2>
            <table className="schedule-table">
                <thead>
                    <tr>
                        <th>День / Время</th>
                        {uniqueTimes.map(time => (
                            <th key={time}>{time}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {daysOfWeek.map((day, dayIndex) => (
                        <tr key={day}>
                            <td>{day}</td>
                            {uniqueTimes.map(time => {
                                const daySchedule = scheduleByDay[dayIndex + 1] ? scheduleByDay[dayIndex + 1][time] : null;
                                return (
                                    <td key={time}>
                                        {daySchedule ? daySchedule.map((item, index) => (
                                            <div key={index}>
                                                <p>Предмет: {item.subject.name}</p>
                                                <p>Кабинет: {item.cabinet.number}</p>
                                                <p>Преподаватель: {item.teacher.name}</p>
                                            </div>
                                        )) : '—'}
                                    </td>
                                );
                            })}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
  }
  
  export default Schedule;

// (() => {
//             switch (order.squad.number) {
//                 case '2450r':
//                     return <p>Значение - A</p>;
//                 case 'b':
//                     return <p>Значение - B</p>;
//                 default:
//                     return <p>Значение неизвестно</p>;
//             }
//         })