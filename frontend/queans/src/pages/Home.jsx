import React from 'react';
import { Link } from 'react-router-dom';
import { useEffect, useState } from 'react'
import QuestionCard from '@/components/header/QuestionCard';
import QuestionForm from '@/components/header/CreateQuestionForm';
import { fetchQuestions } from '@/services/question';

function Home () {
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
        <section className="p-8 flex flex-row justify-start items-start">
            <div className="flex flex-col w-1/3 gap-10">
            <QuestionForm />
            </div>
            <div className='flex flex-col w-2/3 gap-10'>
            <ul className='flex flex-col flex-1'>
                {questions.map(q => (
                  <Link to={`/questions/${q.id}`}>
                    <li key={q.id}>
                        <QuestionCard rating={q.rating} authorName={q.authorName} title={q.title} tags={q.tags}/>
                    </li>
                  </Link>
                ))}
            </ul>
            </div>
        </section>
    </>
  );
};

export default Home;