import {
  Avatar,
  Button,
  Card,
  HStack,
  Stack,
  Strong,
  Text,
} from "@chakra-ui/react"

export default function QuestionCard({ rating, authorName, title, tags }) {
  return (
    <Card.Root>
      <Card.Body>
        <HStack mb="6" gap="3">
          <Avatar.Root>
            <Avatar.Image src="https://images.unsplash.com/photo-1511806754518-53bada35f930" />
            <Avatar.Fallback name="Nate Foss" />
          </Avatar.Root>
          <Stack gap="0">
            <Text fontWeight="semibold" textStyle="sm">
              @{authorName} 
            </Text>
            <Text fontWeight="semibold" textStyle="sm">
              Rate: {rating} 
            </Text>
          </Stack>
        </HStack>
        <Card.Title>
            <Strong color="fg">
                {title}
            </Strong>
        </Card.Title>
        <Card.Description>
            <a href="#" className='flex flex-col flex-1'>
                {tags.map(t => (
                    <Text>#{t.name}</Text>
            ))}
            </a>
        </Card.Description>
      </Card.Body>
      <Card.Footer>
        <Button variant="subtle" colorPalette="blue" flex="1">
          Answer
        </Button>
      </Card.Footer>
    </Card.Root>
  )
}