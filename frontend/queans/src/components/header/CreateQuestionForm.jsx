import { Button, Card, Input, Textarea } from '@chakra-ui/react'

export default function CreateQuestionForm() {
    return (
        <Card.Root className="w-full flex flex-col gap-3">
            <Card.Header>
                <h3 className="font-bold text-xl">Ask a question</h3>
            </Card.Header>
            <Card.Body>
                <Input placeholder="Question title" />
                <Textarea placeholder="Description" />
            </Card.Body>
            <Card.Footer>
                <Button variant='subtle' flex='1'>Create</Button>
            </Card.Footer>
        </Card.Root>
    )
}