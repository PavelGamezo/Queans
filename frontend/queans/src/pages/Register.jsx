import React from 'react';
import { useEffect, useState } from "react";
import { Button, Card, Field, Input, Stack, Flex } from "@chakra-ui/react";
import { PasswordInput } from '@/components/ui/password-input';
import { fetchRegister } from '@/services/register';
import { Link } from 'react-router-dom';

function RegisterForm () {
  const [errorMessage, setErrorMessage] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    let userDto = await fetchRegister(username, email, password);
    setErrorMessage("");
    console.log(userDto);
  } catch (err) {
    if (err.response) {
      console.log(err.response.data.title);
      setErrorMessage(err.response.data.title);
    } else {
      setErrorMessage("Не удалось подключиться к серверу");
    }
  }
};

  return(
    <Flex 
      minH="90vh"
      align="center"
      justify="center"
      bg="gray.50"
      p={4}>
      <Card.Root as="form" maxW="sm" onSubmit={handleSubmit}>
      <Card.Header>
        <Card.Title>Register</Card.Title>
        <Card.Description>
          Fill in the form below to create an account
        </Card.Description>
      </Card.Header>
      <Card.Body alignItems="center">
        <Stack gap="4" w="100">
            <Field.Root>
                <Field.Label>Nickname</Field.Label>
                <Input value={username} onChange={(e) => setUsername(e.target.value)}/>
            </Field.Root>
            <Field.Root>
                <Field.Label>Email</Field.Label>
                <Input value={email} onChange={(e) => setEmail(e.target.value)}/>
            </Field.Root>
            <Field.Root>
                <Field.Label>Password</Field.Label>
                <PasswordInput value={password} onChange={(e) => setPassword(e.target.value)}/>
            </Field.Root>
            <Button type="submit">Register</Button>
            {errorMessage && (
                <p style={{ color: "red", marginTop: "10px" }}>
                {errorMessage}
            </p>
            )}
            <p>Already have an account? </p><Link to="/login">Log In</Link>
        </Stack>
      </Card.Body>
    </Card.Root>
    </Flex>
  );
};

export default RegisterForm;