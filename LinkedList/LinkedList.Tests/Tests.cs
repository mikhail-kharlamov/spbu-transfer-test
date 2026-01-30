namespace LinkedList.Tests;

[TestFixture]
public class MyLinkedListTests
{
    private MyLinkedList<int> list;

    [SetUp]
    public void Setup()
    {
        list = new MyLinkedList<int>();
    }

    [Test]
    public void Constructor_CreatesEmptyList()
    {
        Assert.Multiple(() =>
        {
            Assert.That(list.Head, Is.Null);
            Assert.That(list.Tail, Is.Null);
            Assert.That(list.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void AddLast_AddsToEmptyList_SetsHeadAndTail()
    {
        list.AddLast(10);

        Assert.Multiple(() =>
        {
            Assert.That(list.Head?.Value, Is.EqualTo(10));
            Assert.That(list.Tail?.Value, Is.EqualTo(10));
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.Head, Is.SameAs(list.Tail));
        });
    }

    [Test]
    public void AddLast_MultipleItems_UpdatesTailCorrectly()
    {
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        Assert.Multiple(() =>
        {
            Assert.That(list.Head?.Value, Is.EqualTo(1));
            Assert.That(list.Tail?.Value, Is.EqualTo(3));
            Assert.That(list.Count, Is.EqualTo(3));
            
            Assert.That(list.Head?.Next?.Value, Is.EqualTo(2));
            Assert.That(list.Head?.Next?.Next?.Value, Is.EqualTo(3));
        });
    }

    [Test]
    public void AddFirst_AddsToEmptyList_SetsHeadAndTail()
    {
        list.AddFirst(10);

        Assert.Multiple(() =>
        {
            Assert.That(list.Head?.Value, Is.EqualTo(10));
            Assert.That(list.Tail?.Value, Is.EqualTo(10));
            Assert.That(list.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public void AddFirst_MultipleItems_UpdatesHeadCorrectly()
    {
        list.AddFirst(1);
        list.AddFirst(2);

        Assert.Multiple(() =>
        {
            Assert.That(list.Head?.Value, Is.EqualTo(2));
            Assert.That(list.Tail?.Value, Is.EqualTo(1));
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.Head?.Next, Is.SameAs(list.Tail));
        });
    }

    [Test]
    public void Remove_ItemInMiddle_RemovesCorrectly()
    {
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        bool result = list.Remove(2);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.Head?.Next?.Value, Is.EqualTo(3));
        });
    }

    [Test]
    public void Remove_Head_UpdatesHead()
    {
        list.AddLast(1);
        list.AddLast(2);

        bool result = list.Remove(1);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(list.Head?.Value, Is.EqualTo(2));
            Assert.That(list.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public void Remove_Tail_UpdatesTail()
    {
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        bool result = list.Remove(3);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.Tail?.Value, Is.EqualTo(2));
            Assert.That(list.Tail?.Next, Is.Null);
        });
    }

    [Test]
    public void Remove_OnlyElement_ClearsList()
    {
        list.AddLast(1);

        bool result = list.Remove(1);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(list.Head, Is.Null);
            Assert.That(list.Tail, Is.Null);
            Assert.That(list.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void Remove_NonExistentItem_ReturnsFalse()
    {
        list.AddLast(1);
        list.AddLast(2);

        bool result = list.Remove(99);

        Assert.That(result, Is.False);
        Assert.That(list.Count, Is.EqualTo(2));
    }

    [Test]
    public void Contains_FindsElements()
    {
        list.AddLast(10);
        list.AddLast(20);

        Assert.That(list.Contains(10), Is.True);
        Assert.That(list.Contains(20), Is.True);
        Assert.That(list.Contains(30), Is.False);
    }

    [Test]
    public void Clear_ResetsList()
    {
        list.AddLast(1);
        list.AddLast(2);
        
        list.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(list.Head, Is.Null);
            Assert.That(list.Tail, Is.Null);
            Assert.That(list.Count, Is.EqualTo(0));
        });
    }
    
    [Test]
    public void RemoveDuplicates_NoDuplicates_DoesNothing()
    {
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        list.RemoveDuplicates();

        Assert.That(list.Count, Is.EqualTo(3));
        Assert.That(list.Head?.Next?.Value, Is.EqualTo(2));
    }

    [Test]
    public void RemoveDuplicates_WholeListIsDuplicates_LeavesOne()
    {
        list.AddLast(1);
        list.AddLast(1);
        list.AddLast(1);

        list.RemoveDuplicates();

        Assert.Multiple(() =>
        {
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.Head?.Value, Is.EqualTo(1));
            Assert.That(list.Tail?.Value, Is.EqualTo(1));
            Assert.That(list.Head?.Next, Is.Null);
        });
    }

    [Test]
    public void RemoveDuplicates_DuplicatesAtEnd_UpdatesTail()
    {
        // 1 -> 2 -> 2 -> 2
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(2);
        list.AddLast(2);

        list.RemoveDuplicates();

        Assert.Multiple(() =>
        {
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.Head?.Value, Is.EqualTo(1));
            Assert.That(list.Tail?.Value, Is.EqualTo(2));
            Assert.That(list.Tail?.Next, Is.Null);
        });
    }

    [Test]
    public void RemoveDuplicates_MixedDuplicates_CleansCorrectly()
    {
        list.AddLast(1);
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddLast(3);

        list.RemoveDuplicates();

        Assert.Multiple(() =>
        {
            Assert.That(list.Count, Is.EqualTo(3));
            
            var current = list.Head;
            Assert.That(current?.Value, Is.EqualTo(1));
            
            current = current?.Next;
            Assert.That(current?.Value, Is.EqualTo(2));
            
            current = current?.Next;
            Assert.That(current?.Value, Is.EqualTo(3));
            
            Assert.That(current, Is.SameAs(list.Tail));
        });
    }
    
    [Test]
    public void RemoveDuplicates_EmptyOrSingleList_DoesNothing()
    {
        list.RemoveDuplicates();
        Assert.That(list.Count, Is.EqualTo(0));

        list.AddLast(1);
        list.RemoveDuplicates();
        Assert.That(list.Count, Is.EqualTo(1));
    }

}
