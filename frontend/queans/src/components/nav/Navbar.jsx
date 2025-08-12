import { Flex, Link, Button, IconButton, useDisclosure } from '@chakra-ui/react';

function Navbar() {
  const { isOpen, onOpen, onClose } = useDisclosure(); // For mobile menu

  return (
    <Flex as="nav" 
      align="center" 
      justify="space-between" 
      wrap="wrap" p={4} 
      color="white" 
      borderBottom="1px" 
      borderColor="gray.200"
      borderStyle="solid">
      
      <Link href="/" fontSize="xl" fontWeight="bold">
        Queans
      </Link>

      <IconButton
        display={{ base: 'flex', md: 'none' }}
        onClick={isOpen ? onClose : onOpen}
        variant="ghost"
        aria-label="Toggle Navigation"
      />

      <Flex display={{ base: 'none', md: 'flex' }} ml={4}>
        <Link href="/about" mr={4}>About</Link>
        <Link href="/services" mr={4}>Services</Link>
        <Button colorScheme="teal" variant="solid">Sign Up</Button>
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
          <Link href="/about" py={2}>About</Link>
          <Link href="/services" py={2}>Services</Link>
          <Button colorScheme="teal" variant="solid" mt={2}>Sign Up</Button>
        </Flex>
      )}
    </Flex>
  );
}

export default Navbar;