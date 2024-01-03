import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7168',
  // Дополнительные настройки
});

export default apiClient;