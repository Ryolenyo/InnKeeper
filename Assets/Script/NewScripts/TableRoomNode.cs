using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableRoomNode : RoomNode
{
    [Header("Room Node")]
    public BedroomNode bedroomNode;

    [Header("Position Node")]
    public GameObject customer;
    public GameObject plate;

    [Header("Order Bubble")]
    public SpriteRenderer orderBubble;
    public Sprite unknownBubble;
    public Sprite[] foodBubble;

    public bool hasFood;
    public bool hasCustomer;
    public bool hasOrder;
    public int foodNumber;
    public int orderNumber;

    public Hero hero;

    private void Start()
    {
        orderBubble.sprite = null;
    }

    public override void PlayerCommand(GameObject player)
    {

        if(hasCustomer && hasOrder)
        {
            if(Player.SetHasOrder(true, orderNumber))
            {
                Debug.Log("Get Order");
                hasOrder = false;
                orderBubble.sprite = foodBubble[orderNumber];
            }
            else
            {
                //waring order
            }
        }

        if(hasCustomer && !hasFood && orderNumber == Player.instance.foodNumber)
        {
            if(Player.SetHasFood(false, -1))
            {
                Debug.Log("Sent Food");
                hasFood = true;
                foodNumber = orderNumber;
                hero.ReceivedFood();
                SfxPlayer.PlaySfx(SfxEnum.HeroGetFood);
                orderBubble.sprite = null;
                //set food image
                //remove food image from player
            }
        }
    }

    public override void PlayerExitCommand(GameObject player)
    {
    }

    public override void CustomerCommand(GameObject customer)
    {
        hasCustomer = true;
        customer.transform.position = this.customer.transform.position;
        hero = customer.GetComponent<Hero>();
        hero.AssignTable(this);
        SfxPlayer.PlaySfx(SfxEnum.HeroHungry);
        orderBubble.sprite = unknownBubble;
    }

    public override void CustomerExitCommand(GameObject customer)
    {
        hasCustomer = false;
        hasFood = false;
        customer.transform.position = transform.position;
        //remove food image
    }
}
