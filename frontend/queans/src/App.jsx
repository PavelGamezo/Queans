import { useEffect, useState } from 'react'
import Nav from './components/nav/Navbar'
import QuestionForm from './components/header/CreateQuestionForm'
import { Card, CardBody, CardFooter, CardHeader, Heading, Text } from '@chakra-ui/react'
import QuestionCard from './components/header/QuestionCard'
import { fetchQuestions } from './services/question'
import { HashRouter as Router, Routes, Route } from 'react-router-dom'
import Login from './pages/Login'
import Home from './pages/Home'

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
    <>
      <Router>
        <Nav />
        <Routes>
          <Route path='/' element={<Home/>}/>
          <Route path='/login' element={<Login/>}/> 
        </Routes>
    </Router>
    </>
  )
}

export default App
