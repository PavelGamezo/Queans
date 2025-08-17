import axios from "axios";
import api from "./api";

export async function fetchLogin(email, password) {
  try {
    const response = await api.get("https://localhost:7008/api/login", {
      params: {
        userEmail: email,
        password: password,
      },
    });

    const token = response.data;;

    localStorage.setItem("token", token)

  } catch (error) {
    console.error("Ошибка при авторизации:", error);
    throw error;
  }
};