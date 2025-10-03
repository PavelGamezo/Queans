import {
  Avatar,
  Button,
  Card,
  HStack,
  Stack,
  Text,
} from "@chakra-ui/react";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { fetchQuestionById } from "@/services/question";
import CreateAnswerForm from "@/components/header/CreateAnswerForm";

export default function QuestionDetails() {
  const { id } = useParams();
  const [question, setQuestion] = useState(null);

  useEffect(() => {
    const loadQuestion = async () => {
      const data = await fetchQuestionById(id);
      setQuestion(data);
    };
    loadQuestion();
  }, [id]);

  if (!question) {
    return <Text>Загрузка...</Text>;
  }

  return (
    <Stack spacing={8} p={6}>
      {/* ВОПРОС */}
      <Card.Root w="100%" p={6} shadow="md" borderWidth="1px" borderRadius="lg">
        <Card.Body>
          <HStack mb={4} gap={4} align="center">
            <Avatar.Root size="xl">
              <Avatar.Image src="https://images.unsplash.com/photo-1511806754518-53bada35f930" />
              <Avatar.Fallback name={question.authorName} />
            </Avatar.Root>
            <Stack spacing={1}>
              <Text fontSize="xl" fontWeight="semibold">
                @{question.authorName}
              </Text>
              <Text color="gray.500">Рейтинг: {question.rating}</Text>
            </Stack>
          </HStack>

          <Card.Title fontSize="2xl" mb={2}>
            {question.title}
          </Card.Title>
          <Card.Description fontSize="lg" color="gray.700">
            {question.description}
          </Card.Description>
        </Card.Body>
        <Card.Footer justifyContent="flex-end" gap={2}>
            <Button variant="outline" size="sm">
            Answer
            </Button>
            <Button colorScheme="teal" size="sm">
            Vote
            </Button>
        </Card.Footer>
      </Card.Root>
      <CreateAnswerForm questionId={question.id}/>

      {/* ОТВЕТЫ */}
      <Stack spacing={4}>
        <Text fontSize="2xl" fontWeight="bold">
          Answers
        </Text>

        {question.answers && question.answers.length > 0 ? (
          <Stack spacing={4}>
            {question.answers.map((answer) => (
              <Card.Root
                key={answer.id}
                w="100%"
                p={4}
                shadow="sm"
                borderWidth="1px"
                borderRadius="md"
              >
                <Card.Body>
                  <HStack align="flex-start" spacing={4}>
                    <Avatar.Root size="lg" shape="rounded">
                      <Avatar.Image src="https://picsum.photos/200/300" />
                      <Avatar.Fallback name={answer.authorName} />
                    </Avatar.Root>
                    <Stack spacing={2} flex="1">
                      <Text fontWeight="semibold">@{answer.authorName}</Text>
                      <Text>{answer.text}</Text>
                      <Text fontSize="sm" color="gray.500">
                        👍 {answer.rating} •{" "}
                        {new Date(answer.createdAt).toLocaleDateString()}
                      </Text>
                    </Stack>
                  </HStack>
                </Card.Body>
              </Card.Root>
            ))}
          </Stack>
        ) : (
          <Text color="gray.500">There are no answers</Text>
        )}
      </Stack>
    </Stack>
  );
}