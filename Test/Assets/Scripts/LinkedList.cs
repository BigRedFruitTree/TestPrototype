using UnityEngine;

public class LinkedList
{
    public Node head;
    public int weapon;
    
    public void Insert(int weapon)
    {
        Node newNode = new Node(weapon);
        Debug.Log("Low Taper Fade");
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

    public void GetWeaponAtIndex(int index)
    {

    }
}
