import api from "./api";

export async function fetchRegister(username ,email, password) {
  try {
    const response = await api.post("https://localhost:7008/api/register", {
        userName: username,
        userEmail: email,
        password: password,
    });

    const userDto = response.data;

    return userDto;

  } catch (error) {
    console.error("Ошибка при авторизации:", error);
    throw error;
  }
};