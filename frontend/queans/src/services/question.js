import axios from "axios"
import api from "./api";

export const fetchQuestions = async () =>{
    try{
        var response = await api.get("https://localhost:7008/api/Question/questions");
        
        return response.data;
    }
    catch(e){
        console.error(e);
    }
};


export async function fetchQuestionById(id) {
  const response = await api.get(`https://localhost:7008/api/Question/question/${id}`);
  console.log(response.data);
  return response.data;
}