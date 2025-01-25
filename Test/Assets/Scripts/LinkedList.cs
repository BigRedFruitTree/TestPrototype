using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList
{
    public Node head;
    public Node first;
    public Node current;
    public Node last;

    public int Main(int weapon)
    {
        first = new Node(1);
        first.next = new Node(2);
        first.next.next = new Node(3);
        last = first.next.next;
        last.next = first;
        current = first;

        return weapon;
    }

    public int Search()
    {
        if (current == first)
        {
            return 1;
        }
        if (current == first.next)
        {
            return 2;
        }
        if (current == first.next.next)
        {
            return 3;
        }
        return 0;
    }

    public void CycleWeapons()
    {
        Debug.Log("THICK OF IT");
        current = current.next;  
    }
}
