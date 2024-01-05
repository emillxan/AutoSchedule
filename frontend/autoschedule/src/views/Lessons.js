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
    const uniqueTimes = [...new Set(lessons.map(lesson => new Date(lesson.time).toLocaleTimeString()))].sort();

    // Дни недели
    const daysOfWeek = ['Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница'];

    // Функция для проверки идентичности уроков
    const areLessonsIdentical = (lesson1, lesson2) => {
        return lesson1.subject.name === lesson2.subject.name &&
               lesson1.teacher.name === lesson2.teacher.name &&
               lesson1.cabinet.number === lesson2.cabinet.number;
    };

    // Функция для отрисовки урока в ячейке
   // Остальной код компонента...

const renderLessonCell = (lessonsForTimeSlot, dayIndex) => {
    // Если нет уроков в слоте, возвращаем пустое пространство
    if (lessonsForTimeSlot.length === 0) {
        return '—';
    }

    // Если есть только один урок в слоте
    if (lessonsForTimeSlot.length === 1) {
        const lesson = lessonsForTimeSlot[0];
        const content = renderLessonDetails(lesson);
        
        // Если урок принадлежит к верхней неделе (weekType === 0)
        if (lesson.weekType === 0) {
            return (
                <div className="lesson-slot">
                    {content}
                    <hr className="divider" />
                    <div className="empty-lesson-slot"></div>
                </div>
            );
        } else { // Если урок принадлежит к нижней неделе (weekType === 1)
            return (
                <div className="lesson-slot">
                    <div className="empty-lesson-slot"></div>
                    <hr className="divider" />
                    {content}
                </div>
            );
        }
    } else {
        // Если в слоте более одного урока, нужно проверить, одинаковы ли они
        const [firstLesson, secondLesson] = lessonsForTimeSlot;
        const firstContent = renderLessonDetails(firstLesson);
        const secondContent = renderLessonDetails(secondLesson);

        // Если уроки одинаковы, отображаем только один
        if (areLessonsIdentical(firstLesson, secondLesson)) {
            return firstContent;
        } else {
            // Если уроки разные, отображаем оба с разделительной линией
            return (
                <div className="lesson-slot">
                    {firstContent}
                    <hr className="divider" />
                    {secondContent}
                </div>
            );
        }
    }
};
const renderLessonDetails = (lesson) => {
    // Проверяем, что объект урока определен и содержит необходимые данные
    if (!lesson || !lesson.subject || !lesson.teacher || !lesson.cabinet) {
        return null; // Возвращаем null, если данных недостаточно для отображения
    }

    // Возвращаем JSX с информацией об уроке
    return (
        <div className="lesson-details">
            <p>Предмет: {lesson.subject.name}</p>
            <p>Преподаватель: {lesson.teacher.name}</p>
            <p>Кабинет: {lesson.cabinet.number}</p>
        </div>
    );
};


// Остальной код компонента...

    return (
        <div className="container">
            <h2>Уроки для группы {squadId}</h2>
            <table>
                <thead>
                    <tr>
                        <th>День / Время</th>
                        {uniqueTimes.map(time => <th key={time}>{time}</th>)}
                    </tr>
                </thead>
                <tbody>
                    {daysOfWeek.map((day, dayIndex) => (
                        <tr key={day}>
                            <td>{day}</td>
                            {uniqueTimes.map(time => {
                                const lessonsForTimeSlot = lessons.filter(lesson => 
                                    new Date(lesson.time).toLocaleTimeString() === time && 
                                    lesson.dayOfWeek === dayIndex + 1
                                );
                                return (
                                    <td key={time}>
                                        {renderLessonCell(lessonsForTimeSlot, dayIndex)}
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

export default Lessons;
