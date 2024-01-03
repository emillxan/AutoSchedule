// src/api/teacherService.js
import apiClient from './apiClient';

// Получение всех учителей
export const getAllTeachers = async () => {
  try {
    const response = await apiClient.get('/Teacher/GetAll');
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Создание нового учителя
export const createTeacher = async (teacherData) => {
  try {
    const response = await apiClient.post('/Teacher/Create', teacherData);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Получение учителя по ID
export const getTeacherById = async (id) => {
  try {
    const response = await apiClient.get(`/Teacher/GetById?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Редактирование учителя
export const editTeacher = async (id, teacherData) => {
  try {
    const response = await apiClient.patch(`/Teacher/Edit`, { id, ...teacherData });
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

// Удаление учителя
export const deleteTeacher = async (id) => {
  try {
    const response = await apiClient.delete(`/Teacher/Delete?id=${id}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};