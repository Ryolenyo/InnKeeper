﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableRoomNode : RoomNode
{
    [Header("Room Node")]
    public BedroomNode bedroomNode;

    [Header("Position Node")]
    public GameObject customer;
    public GameObject plate;

    public bool hasFood;
    public bool hasCustomer;
    public bool hasOrder;
    public int foodNumber;
    public int orderNumber;

    public Hero hero;

    public override void PlayerCommand(GameObject player)
    {

        if(hasCustomer && hasOrder)
        {
            if(Player.SetHasOrder(true, orderNumber))
            {
                Debug.Log("Get Order");
                hasOrder = false;
                //show food bubble
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
                //set food image
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
    }

    public override void CustomerExitCommand(GameObject customer)
    {
        hasCustomer = false;
        hasFood = false;
        customer.transform.position = transform.position;
    }
}
