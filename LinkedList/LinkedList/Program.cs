using LinkedList;

var list = new MyLinkedList<int>();

list.AddLast(1);
list.AddLast(1);
list.AddLast(2);
list.AddLast(3);
list.AddLast(3);
list.AddLast(3);

Console.WriteLine("Before removing duplicates:");
list.Print();

list.RemoveDuplicates();

Console.WriteLine("After removing duplicates:");
list.Print();
