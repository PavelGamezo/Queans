import Login from '@/pages/login';
import { useState, useEffect } from "react";
import { Flex, Button, Link as ChakraLink, IconButton, useDisclosure, Text } from '@chakra-ui/react';
import { Link } from 'react-router-dom';

function Navbar() {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [token, setToken] = useState("");

  useEffect(() => {
    const savedToken = localStorage.getItem("token");
    if (savedToken) {
      setToken(savedToken);
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setToken("");
  };

  return (
    <Flex as="nav" 
      align="center" 
      justify="space-between" 
      wrap="wrap" p={4} 
      color="white" 
      borderBottom="1px" 
      borderColor="gray.200"
      borderStyle="solid">
      
      <ChakraLink to="/" href="/" fontSize="xl" fontWeight="bold">
        Queans
      </ChakraLink>

      <IconButton
        display={{ base: 'flex', md: 'none' }}
        onClick={isOpen ? onClose : onOpen}
        variant="ghost"
        aria-label="Toggle Navigation"
      />

      <Flex display={{ base: 'none', md: 'flex' }} ml={4}>
        <Link to="/services" mr={4}>About</Link>
        <ChakraLink to="/" mr={4}>Services</ChakraLink>
        {!token ? (
          <Link to="/login" mr={4}>
            <Button colorScheme="teal" variant="solid">
              Log In
            </Button>
          </Link>
        ) : (
          <Button colorScheme="red" variant="outline" onClick={handleLogout}>
            Log out
          </Button>
        )}
      </Flex>

      {isOpen && (
        <Flex
          direction="column"
          align="center"
          justify="center"
          bg="teal.500"
          w="100%"
          mt={4}
          display={{ base: 'flex', md: 'none' }}
        >
          <Link to="/about" py={2}>About</Link>
          <Link to="/" py={2}>Services</Link>
          <Link to="/">
            <Button colorScheme="teal" variant="solid" mt={2}>Log In</Button>
          </Link>
        </Flex>
      )}
    </Flex>
  );
}

export default Navbar;