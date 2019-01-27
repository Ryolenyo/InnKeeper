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

    [Header("Food")]
    public SpriteRenderer foodOnHead;
    public Sprite[] food;

    [Header("Order Bubble")]
    public SpriteRenderer orderBubble;
    public Sprite[] foodBubble;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        movement = GetComponent<NodeToNodeMovement>();
        instance.foodOnHead.sprite = null;
        instance.orderBubble.sprite = null;
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
            if (instance.hasFood) instance.foodOnHead.sprite = instance.food[foodNumber];
            else instance.foodOnHead.sprite = null;

            return true;
        }
        else return false;
    }

    public static bool SetHasOrder(bool value, int orderNumber)
    {
        if (instance.hasOrder != value)
        {
            instance.hasOrder = value;
            instance.orderNumber = orderNumber;
            if (instance.hasOrder) instance.orderBubble.sprite = instance.foodBubble[orderNumber];
            else instance.orderBubble.sprite = null;

            return true;
        }
        else return false;
    }
}
