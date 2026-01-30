namespace LinkedList;

public class Node<T>(T value)
{
    public T Value { get; set; } = value;
    public Node<T>? Next { get; set; }
}
