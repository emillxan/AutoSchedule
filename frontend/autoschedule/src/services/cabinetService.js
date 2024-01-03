// src/api/cabinetService.js
import apiClient from './apiClient';

// Получение всех кабинетов
export const getAllCabinets = async () => {
  try {
    const response = await apiClient.get('/Cabinet/GetAll');
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Создание нового кабинета
export const createCabinet = async (cabinetData) => {
  try {
    const response = await apiClient.post('/Cabinet/Create', cabinetData);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Получение кабинета по ID
export const getCabinetById = async (id) => {
  try {
    const response = await apiClient.get(`/Cabinet/GetById?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Редактирование кабинета
export const editCabinet = async (id, cabinetData) => {
  try {
    const response = await apiClient.patch(`/Cabinet/Edit`, { id, ...cabinetData });
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Удаление кабинета
export const deleteCabinet = async (id) => {
  try {
    const response = await apiClient.delete(`/Cabinet/Delete?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};