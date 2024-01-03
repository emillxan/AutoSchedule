import React, { useState } from 'react';

function CabinetForm({ onSubmit, initialData }) {
    const [number, setNumber] = useState(initialData ? initialData.number : '');
  
    const handleSubmit = (event) => {
      event.preventDefault();
      onSubmit({ number });
    };
  
    return (
      <form onSubmit={handleSubmit}>
        <label>
          Номер кабинета:
          <input
            type="text"
            value={number}
            onChange={(e) => setNumber(e.target.value)}
            required
          />
        </label>
        <button type="submit">Сохранить</button>
      </form>
    );
  }

export default CabinetForm;