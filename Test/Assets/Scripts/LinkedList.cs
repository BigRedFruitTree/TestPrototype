using UnityEngine;

public class LinkedList
{
    public Node head;
    
    public void Insert(int weapon)
    {
        Node newNode = new Node(weapon);
        if(head == null)
        {
            head = newNode;
        } else
        {
            Node searchNode = head;
            while (searchNode.next != null)
            {
                searchNode = searchNode.next;
            }
            searchNode.next = newNode;
        }
    }

    public int GetWeaponAtIndex(int index)
    {
        Node current = head;
        int count = 0;

        while (current != null)
        {
            if(count == index)
            {
                Debug.Log("0 Gyatt");
                count = 0;
                return current.Data;
            }
            count++;
            current = current.next;
            
        }
        Debug.LogWarning("L rizz no aura");
        return index;
    }

    public int Count()
    {
        int count = 0;
        Node current = head;

        while (current != null)
        {
            count++;
            current = current.next;
        }
        return count;
    }
}
