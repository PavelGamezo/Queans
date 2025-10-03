import { useEffect, useState } from 'react';
import Nav from './components/nav/Navbar';
import { fetchQuestions } from './services/question';
import { HashRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/Login';
import Home from './pages/Home';
import Register from './pages/Register';
import QuestionDetails from './pages/QuestionDetails';

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
          <Route path='/register' element={<Register/>}/>
          <Route path="/questions/:id" element={<QuestionDetails />} />
        </Routes>
    </Router>
    </>
  )
}

export default App
