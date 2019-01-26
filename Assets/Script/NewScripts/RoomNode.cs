using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public GameObject hallNode;

    

    public virtual void PlayerCommand(GameObject player)
    {
        Debug.Log("Player at " + gameObject.name);
    }

    public virtual void PlayerExitCommand(GameObject player)
    {
        Debug.Log("Player exit " + gameObject.name);
    }

    public virtual void CustomerCommand(GameObject customer)
    {
        Debug.Log("Customer at " + gameObject.name);
    }

    public virtual void CustomerExitCommand(GameObject customer)
    {
        Debug.Log("Customer exit " + gameObject.name);
    }

    void OnMouseDown()
    {
        Player.SetDestination(this.gameObject, hallNode);
    }
}
