using System;
using UnityEngine;

public class LinkedList
{

    public bool Search(Node last, int key)
    {
        if (last == null)
        {
          return false;
        }

        Node head = last.next;
        Node curr = head;

        while (curr != last)
        {
          if (curr.data == key)
          {
            return true;
          }
            curr = curr.next;
        }

        if (last.data == key)
        {
           return true;
        }
          return false;
    }


    public int Main(int weapon)
    {
        Node first = new Node(1);
        first.next = new Node(2);
        first.next.next = new Node(3);
        Node last = first.next.next;
        last.next = first;
        IsCircular(last);
        return weapon;
    }

    public bool IsCircular(Node head) 
    {
        // If head is null, list is empty, circular
        if (last == null) return true;

        Node temp = first;

        // Traverse until the end is reached or
        // the next node equals the head
        while (last != null && last.next != temp) 
        {
           last = last.next;
        }
           

        // If end reached before finding head again,
        // list is not circular
        if (last == null || last.next == null) 
        {
            Debug.Log("OH NO");
        }
        // If head found again, list is circular
        Debug.Log("OH YEAH");
        return true;
    }
}
