// src/views/Cabinets.js
import React, { useState, useEffect } from 'react';
import { getAllCabinets, createCabinet, editCabinet, deleteCabinet } from '../services/cabinetService';
import CabinetForm from '../components/CabinetForm';

function Cabinets() {
  const [cabinets, setCabinets] = useState([]);
  const [editingCabinet, setEditingCabinet] = useState(null);

  useEffect(() => {
    fetchCabinets();
  }, []);

  const fetchCabinets = async () => {
    try {
      const data = await getAllCabinets();
      setCabinets(data);
    } catch (error) {
      console.error('Ошибка при получении данных о кабинетах:', error);
    }
  };

  const handleCreate = async (cabinetData) => {
    await createCabinet(cabinetData);
    fetchCabinets();
  };

  const handleEdit = async (cabinetData) => {
    await editCabinet(editingCabinet.id, cabinetData);
    setEditingCabinet(null);
    fetchCabinets();
  };

  const handleDelete = async (cabinetId) => {
    await deleteCabinet(cabinetId);
    fetchCabinets();
  };

  return (
    <div>
      <h1>Кабинеты</h1>
      {/* Форма для добавления нового кабинета */}
      <CabinetForm onSubmit={handleCreate} />
      {/* Форма для редактирования существующего кабинета */}
      {editingCabinet && (
        <CabinetForm onSubmit={handleEdit} initialData={editingCabinet} />
      )}
      <table>
      <thead>
        {/* ... заголовки таблицы ... */}
      </thead>
      <tbody>
        {cabinets.map(cabinet => (
          <tr key={cabinet.id}>
            <td>{cabinet.number}</td>
            <td>
              <button onClick={() => setEditingCabinet(cabinet)}>Изменить</button>
              <button onClick={() => handleDelete(cabinet.id)}>Удалить</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
    </div>
  );
}

export default Cabinets;
