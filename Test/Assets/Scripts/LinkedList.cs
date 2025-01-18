using UnityEngine;

public class LinkedList
{

    public static Node InsertAtBeginning(Node last, int value)
    {
        Node newNode = new Node(value);

        if (last == null)
        {
            newNode.next = newNode;
            return newNode;
        }

        newNode.next = last.next;
        last.next = newNode;

        return last;
    }

    public int Main (int weapon)
    {
        Node first = new Node(1);
        first.next = new Node(2);
        first.next.next = new Node(3);
        Node last = first.next.next;
        last.next = first;

        return weapon;
    }
}
