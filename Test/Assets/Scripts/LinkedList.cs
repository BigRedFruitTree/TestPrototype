using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList
{
    public Node head;
    public Node current;
    public Node first;

    public int Search()
    {
        if(current == first)
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
    //Low Taper Fade
    public int Main(int weapon)
    {
        Node first = new Node(1);
        first.next = new Node(2);
        first.next.next = new Node(3);
        Node last = first.next.next;
        last.next = first;

        return weapon;
    }

    public void CycleWeapons()
    {
        while (current != null)
        {
            current = current.next;
            StartCoroutine("Wait");
        }
    }

    private void StartCoroutine(string v)
    {
        throw new NotImplementedException();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(60);
    }
}
