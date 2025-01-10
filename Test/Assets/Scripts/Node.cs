using System;

public class Node
{
    public int Data;
    public Node Next;
    public Node Head;

    public Node(int data)
    {
        Data = data;
        Next = null;

        Node first = new Node(2);
        Node second = new Node(3);
        Node last = new Node(4);

        first.Next = second;
        second.Next = last;
        last.Next = first;
    }

    public void Insert(int value, Node Head)
    {
        Node searchNode = Head;
        while (searchNode.Next != null)
        {
            searchNode = searchNode.Next;
        }
        searchNode.Next = new Node(1);
       
    }
}
