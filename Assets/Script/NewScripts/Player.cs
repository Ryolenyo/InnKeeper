using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Node List")]
    public GameObject reception;

    public static Player instance;
    private NodeToNodeMovement movement;
    public GameObject customer;
    public bool hasFood { get; private set; }
    public int foodNumber;
    public bool hasOrder { get; private set; }
    public int orderNumber;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        movement = GetComponent<NodeToNodeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetDestination(GameObject destNode, GameObject hallNode)
    {
        instance.movement.SetDestination(destNode, hallNode);
    }

    public static void SetDestinationToReception()
    {
        instance.movement.SetDestination(instance.reception,
            instance.reception.GetComponent<RoomNode>().hallNode);
    }

    public static bool HasCustomer()
    {
        return instance.customer;
    }

    private void OnDestroy()
    {
        instance = null;
    }


    public static bool SetHasFood(bool value, int foodNumber)
    {
        if (instance.hasFood != value)
        {
            instance.hasFood = value;
            instance.foodNumber = foodNumber;
            return true;
            //set bubble image
        }
        else return false;
    }

    public static bool SetHasOrder(bool value, int orderNumber)
    {
        if (instance.hasOrder != value)
        {
            instance.hasOrder = value;
            instance.orderNumber = orderNumber;
            //set bubble image
            return true;

        }
        else return false;
    }
}
