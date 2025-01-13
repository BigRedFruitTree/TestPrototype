using UnityEngine;

public class Node
{
    public int Data;
    public Node Next;
    public Node Head;

    public Node(int data)
    {
        Data = data;
        Next = null;
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
