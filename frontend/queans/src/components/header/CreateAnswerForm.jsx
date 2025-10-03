import { Editable, IconButton } from "@chakra-ui/react"
import { LuCheck, LuPencilLine, LuX } from "react-icons/lu"
import { useState } from "react"
import { Link } from "react-router-dom"
import api from "@/services/api"

const CreateAnswerForm = ({ questionId }) => {
  const [textValue, setValue] = useState("Click to answer");

  const handleSubmit = async () => {
    try {
      const response = await api.post(`https://localhost:7008/api/questions/${questionId}/answers`, {
        text: textValue,
      });

      console.log("Ответ успешно создан:", response.data);

      setValue(""); // очистка поля
    } catch (error) {
      console.error("Ошибка при создании ответа:", error);
    }
  };

  return (
    <Editable.Root
      value={textValue}
      onValueChange={setValue}
      onSubmit={handleSubmit}
      placeholder="Click to edit"
    >
      <Editable.Preview />
      <Editable.Input />
      <Editable.Control>
        <Editable.EditTrigger asChild>
          <IconButton variant="ghost" size="xs">
            <LuPencilLine />
          </IconButton>
        </Editable.EditTrigger>
        <Editable.CancelTrigger asChild>
          <IconButton variant="outline" size="xs">
            <LuX />
          </IconButton>
        </Editable.CancelTrigger>
        <Editable.SubmitTrigger asChild>
          <IconButton variant="outline" size="xs">
            <LuCheck />
          </IconButton>
        </Editable.SubmitTrigger>
      </Editable.Control>
    </Editable.Root>
  )
}

export default CreateAnswerForm;