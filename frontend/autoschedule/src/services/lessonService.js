import apiClient from './apiClient';

// Получение уроков по ID группы
export const getLessonsBySquadId = async (squadId) => {
  try {
    const response = await apiClient.get(`/Lesson/GetLessonsBySquadId?id=${squadId}`);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};