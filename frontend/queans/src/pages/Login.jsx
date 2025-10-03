import React from 'react';
import { useEffect, useState } from "react";
import { Button, Card, Field, Input, Stack, Flex } from "@chakra-ui/react";
import { PasswordInput } from '@/components/ui/password-input';
import { fetchLogin } from '@/services/login';
import RegisterForm from './Register';
import { Link } from 'react-router-dom';

function LoginForm () {
  const [errorMessage, setErrorMessage] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    await fetchLogin(email, password);
    setErrorMessage("");
  } catch (err) {
    if (err.response) {
      setErrorMessage(err.response.data.title);
    } else {
      setErrorMessage("Не удалось подключиться к серверу");
    }
  }
};

  return(
    <Flex 
      minH="90vh"          // высота на весь экран
      align="center"        // вертикальное выравнивание
      justify="center"      // горизонтальное выравнивание
      bg="gray.50"          // фон (по желанию)
      p={4}>
      <Card.Root as="form" maxW="sm" onSubmit={handleSubmit}>
      <Card.Header>
        <Card.Title>Sign up</Card.Title>
        <Card.Description>
          Fill in the form below to create an account
        </Card.Description>
      </Card.Header>
      <Card.Body alignItems="center">
        <Stack gap="4" w="100">
          <Field.Root>
            <Field.Label>Email</Field.Label>
            <Input value={email} onChange={(e) => setEmail(e.target.value)}/>
          </Field.Root>
          <Field.Root>
            <Field.Label>Password</Field.Label>
            <PasswordInput value={password} onChange={(e) => setPassword(e.target.value)}/>
          </Field.Root>
          <Button type="submit">Login</Button>
          {errorMessage && (
            <p style={{ color: "red", marginTop: "10px" }}>
              {errorMessage}
            </p>
          )}
          <p>Don't have any account? </p><Link to="/register">Register</Link>
        </Stack>
      </Card.Body>
    </Card.Root>
    </Flex>
  );
};

export default LoginForm;