namespace LinkedList;

public class MyLinkedList<T>
{
    public Node<T>? Head { get; private set; }
    
    public Node<T>? Tail { get; private set; }
    
    public int Count { get; private set; }

    public void AddLast(T value)
    {
        var newNode = new Node<T>(value);

        if (Head is null)
        {
            this.Head = newNode;
            this.Tail = newNode;
        }
        else
        {
            if (Tail is not null) 
            {
                this.Tail.Next = newNode;
                this.Tail = newNode;
            }
        }
        this.Count++;
    }

    public void AddFirst(T value)
    {
        var newNode = new Node<T>(value)
        {
            Next = Head
        };
        
        this.Head = newNode;
        
        if (Tail is null)
        {
            this.Tail = Head;
        }
        Count++;
    }

    public bool Remove(T value)
    {
        if (Head is null) return false;

        if (EqualityComparer<T>.Default.Equals(Head.Value, value))
        {
            this.Head = this.Head.Next;
            if (Head is null)
            {
                this.Tail = null;
            }
            
            this.Count--;
            return true;
        }

        var current = Head;
        while (current.Next is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next.Value, value))
            {
                if (current.Next == Tail)
                {
                    this.Tail = current;
                }
                
                current.Next = current.Next.Next;
                this.Count--;
                return true;
            }
            
            current = current.Next;
        }

        return false;
    }

    public bool Contains(T value)
    {
        var current = this.Head;
        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
                return true;
            
            current = current.Next;
        }
        return false;
    }

    public void Clear()
    {
        this.Head = null;
        this.Tail = null;
        this.Count = 0;
    }
    
    public void RemoveDuplicates()
    {
        if (this.Head is null || this.Head.Next is null) 
        {
            return;
        }

        var current = this.Head;

        while (current.Next is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, current.Next.Value))
            {
                if (current.Next == this.Tail)
                {
                    this.Tail = current;
                }

                current.Next = current.Next.Next;

                this.Count--;
            }
            else
            {
                current = current.Next;
            }
        }
    }
    
    public void Print()
    {
        var current = Head;

        while (current is not null)
        {
            Console.Write($"{current.Value}");
            current = current.Next;
        }
    
        Console.WriteLine();
    }
}
