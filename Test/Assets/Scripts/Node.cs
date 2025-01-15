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
}
