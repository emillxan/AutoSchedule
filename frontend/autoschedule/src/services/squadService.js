// src/api/squadService.js
import apiClient from './apiClient';

// Получение всех групп
export const getAllSquads = async () => {
  try {
    const response = await apiClient.get('/Squad/GetAll');
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Создание новой группы
export const createSquad = async (squadData) => {
  try {
    const response = await apiClient.post('/Squad/Create', squadData);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Получение группы по ID
export const getSquadById = async (id) => {
  try {
    const response = await apiClient.get(`/Squad/GetById?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Редактирование группы
export const editSquad = async (id, squadData) => {
  try {
    const response = await apiClient.patch(`/Squad/Edit`, { id, ...squadData });
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Удаление группы
export const deleteSquad = async (id) => {
  try {
    const response = await apiClient.delete(`/Squad/Delete?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};