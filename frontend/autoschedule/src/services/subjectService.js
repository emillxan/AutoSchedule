// src/api/subjectService.js
import apiClient from './apiClient';

// Получение всех предметов
export const getAllSubjects = async () => {
  try {
    const response = await apiClient.get('/Subject/GetAll');
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Создание нового предмета
export const createSubject = async (subjectData) => {
  try {
    const response = await apiClient.post('/Subject/Create', subjectData);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Получение предмета по ID
export const getSubjectById = async (id) => {
  try {
    const response = await apiClient.get(`/Subject/GetById?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Редактирование предмета
export const editSubject = async (id, subjectData) => {
  try {
    const response = await apiClient.patch(`/Subject/Edit`, { id, ...subjectData });
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Удаление предмета
export const deleteSubject = async (id) => {
  try {
    const response = await apiClient.delete(`/Subject/Delete?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};