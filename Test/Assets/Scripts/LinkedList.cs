using UnityEngine;

public class LinkedList
{
    public Node head;
    public int weapon;
    public PlayerController pc;
    
    public void Insert(int weapon)
    {
        Node newNode = new Node(weapon);
        if(head == null)
        {
            head = newNode;
        } else
        {
            Node searchNode = head;
            while (searchNode.Next != null)
            {
                searchNode = searchNode.Next;
            }
            searchNode.Next = new Node(1);
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
                return current.Data;
            }
            count++;
            current = current.Next;
            
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
            current = current.Next;
        }
        return count;
    }
}
