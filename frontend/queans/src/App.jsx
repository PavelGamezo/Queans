import { useEffect, useState } from 'react'
import Nav from './components/nav/Navbar'
import QuestionForm from './components/header/CreateQuestionForm'
import { Card, CardBody, CardFooter, CardHeader, Heading, Text } from '@chakra-ui/react'
import QuestionCard from './components/header/QuestionCard'
import { fetchQuestions } from './services/question'

function App() {
  const [questions, setQuestions] = useState([])

  useEffect(() => {
    const fetchData = async () => {
      let getQuestionsResponse = await fetchQuestions();
      setQuestions(getQuestionsResponse);
    };

    fetchData();
  }, [])

  return (
    <><Nav />
      <section className="p-8 flex flex-row justify-start items-start">
      <div className="flex flex-col w-1/3 gap-10">
        <QuestionForm />
      </div>
      <div className='flex flex-col w-2/3 gap-10'>
        <ul className='flex flex-col gap-5 flex-1'>
          {questions.map(q => (
            <li key={q.id}>
              <QuestionCard rating={q.rating} authorName={q.authorName} title={q.title} tags={q.tags}/>
            </li>
          ))}
        </ul>
      </div>
      </section>
    </>
  )
}

export default App
