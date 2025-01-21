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

        Debug.Log("weapon" + weapon);
        return weapon;
    }

}
