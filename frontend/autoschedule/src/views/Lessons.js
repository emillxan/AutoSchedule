import React, { useState, useEffect } from 'react';
import { getLessonsBySquadId } from '../services/lessonService';

function Lessons() {
    const [lessons, setLessons] = useState([]);
    const squadId = 1; // Пример ID группы

    useEffect(() => {
        fetchLessons(squadId);
    }, [squadId]);

    const fetchLessons = async (id) => {
        try {
            const data = await getLessonsBySquadId(id);
            setLessons(data);
        } catch (error) {
            console.error('Ошибка при получении данных об уроках:', error);
        }
    };

    // Уникальные времена
    const uniqueTimes = [...new Set(lessons.map(lesson => new Date(lesson.time).toLocaleTimeString()))];

    // Дни недели (только рабочие дни)
    const daysOfWeek = ['Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница'];

    // Группировка уроков по дням недели и времени
    const lessonsByDayAndTime = lessons.reduce((acc, lesson) => {
        const dayIndex = new Date(lesson.time).getDay();
        const time = new Date(lesson.time).toLocaleTimeString();

        if (dayIndex === 0 || dayIndex === 6) {
            // Пропускаем воскресенье (0) и субботу (6)
            return acc;
        }

        if (!acc[dayIndex]) {
            acc[dayIndex] = {};
        }
        if (!acc[dayIndex][time]) {
            acc[dayIndex][time] = [];
        }
        acc[dayIndex][time].push(lesson);
        return acc;
    }, {});

    return (
        <div>
            <h2>Уроки для группы {squadId}</h2>
            <style>
                {`
                    table {
                        width: 100%;
                        border-collapse: collapse;
                        margin-top: 20px;
                    }
                    th, td {
                        border: 1px solid #ddd;
                        padding: 8px;
                        text-align: left;
                    }
                    th {
                        background-color: #4CAF50;
                        color: white;
                    }
                    tr:nth-child(even) {
                        background-color: #f2f2f2;
                    }
                    tr:hover {
                        background-color: #ddd;
                    }
                `}
            </style>
            <table>
                <thead>
                    <tr>
                        <th>День / Время</th>
                        {uniqueTimes.map(time => <th key={time}>{time}</th>)}
                    </tr>
                </thead>
                <tbody>
                    {daysOfWeek.map((day, index) => (
                        <tr key={day}>
                            <td>{day}</td>
                            {uniqueTimes.map(time => (
                                <td key={time}>
                                    {lessonsByDayAndTime[index + 1] && lessonsByDayAndTime[index + 1][time]
                                        ? lessonsByDayAndTime[index + 1][time].map((lesson, idx) => (
                                            <div key={idx}>
                                                <p>Предмет: {lesson.subject.name}</p>
                                                <p>Преподаватель: {lesson.teacher.name}</p>
                                                <p>Кабинет: {lesson.cabinet.number}</p>
                                            </div>
                                        ))
                                        : '—'}
                                </td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Lessons;
