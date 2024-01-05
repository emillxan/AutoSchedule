import React, { useState, useEffect } from 'react';
import { getAllSquads, createSquad, editSquad, deleteSquad } from '../services/squadService'; // Импортируйте API-методы

function Squads() {
  const [squads, setSquads] = useState([]);

  useEffect(() => {
    fetchSquads();
  }, []);

  const [isEditing, setIsEditing] = useState(false);
  const [currentSquad, setCurrentSquad] = useState(null);

  // Функция для начала редактирования существующей группы
  const startEdit = (squad) => {
    setCurrentSquad(squad);
    setIsEditing(true);
  };

  // Функция для сохранения новой или отредактированной группы
  const saveSquad = (squadData) => {
    if (currentSquad) {
      handleEditSquad(currentSquad.id, squadData);
    } else {
      handleCreateSquad(squadData);
    }
    setIsEditing(false);
  };

  // Функция для отмены редактирования
  const cancelEdit = () => {
    setIsEditing(false);
    setCurrentSquad(null);
  };

  const fetchSquads = async () => {
    try {
      const response = await getAllSquads();
      setSquads(response);
    } catch (error) {
      console.error('Ошибка при получении списка групп:', error);
    }
  };

  // Функции для создания, обновления и удаления групп
  // createSquad(), editSquad(), deleteSquad()...
  const handleCreateSquad = async (squadData) => {
    try {
      const response = await createSquad(squadData); // createSquad - функция, вызывающая POST-запрос к API
      if (response.status === 200) {
        fetchSquads(); // Перезагружаем список групп после успешного создания
      }
    } catch (error) {
      console.error('Ошибка при создании группы:', error);
    }
  };
  
  const handleEditSquad = async (squadId, squadData) => {
    try {
      const response = await editSquad(squadId, squadData); // editSquad - функция, вызывающая PATCH-запрос к API
      if (response.status === 200) {
        fetchSquads(); // Перезагружаем список групп после успешного редактирования
      }
    } catch (error) {
      console.error('Ошибка при редактировании группы:', error);
    }
  };
  
  const handleDeleteSquad = async (squadId) => {
    try {
      const response = await deleteSquad(squadId); // deleteSquad - функция, вызывающая DELETE-запрос к API
      if (response.status === 200) {
        fetchSquads(); // Перезагружаем список групп после успешного удаления
      }
    } catch (error) {
      console.error('Ошибка при удалении группы:', error);
    }
  };


  return (
    <div className="container">
      <h2>Группы</h2>
       {/* Кнопка для добавления новой группы */}
      <button onClick={() => startEdit(null)}>Добавить группу</button>
      
      {isEditing && (
        <SquadForm
          initialSquad={currentSquad}
          onSave={saveSquad}
          onCancel={cancelEdit}
        />
      )}

      {/* Таблица групп и другой контент */}
      <table>
        <thead>
          <tr>
            <th>Номер группы</th>
            <th>Действия</th>
          </tr>
        </thead>
        <tbody>
          {squads.map((squad, index) => (
            <tr key={squad.id}>
              <td>{squad.number}</td>
              <td>
                {/* Кнопки для редактирования и удаления */}
                <button onClick={() => handleEditSquad(squad.id)}>Редактировать</button>
                <button onClick={() => handleDeleteSquad(squad.id)}>Удалить</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

function SquadForm({ initialSquad, onSave, onCancel }) {
  // Если редактируем существующую группу, то используем ее данные как начальное состояние
  const [squad, setSquad] = useState(initialSquad || { number: '', subjectIds: [] });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setSquad({ ...squad, [name]: value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    onSave(squad);
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Номер группы:
        <input
          type="text"
          name="number"
          value={squad.number}
          onChange={handleChange}
          required
        />
      </label>
      {/* Здесь можно добавить поля для ввода subjectIds и другой информации */}
      <button type="submit">Сохранить</button>
      <button type="button" onClick={onCancel}>Отмена</button>
    </form>
  );
}

export default Squads;
