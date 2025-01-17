using UnityEngine;

public class Node
{
    public int Data;
    public Node next;

    public Node(int data)
    {
        Data = data;
        next = null;

        Node first = new Node(2);
        Node second = new Node(3);
        Node last = new Node(4);

        first.next = second;
        second.next = last;
        last.next = first;
    }
}
