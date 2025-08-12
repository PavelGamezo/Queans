import axios from "axios"

export const fetchQuestions = async () =>{
    try{
        var response = await axios.get("https://localhost:7008/api/Question/questions");
        
        return response.data;
    }
    catch(e){
        console.error(e);
    }
};