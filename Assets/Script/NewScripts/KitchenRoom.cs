﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenRoom : RoomNode
{
    public float cookTime = 1;
    private float currentTime = 0;

    [Header("Food Position Node")]
    public SpriteRenderer [] foodPosition;

    [Header("Food Sprite")]
    public Sprite[] foodSprite;

    private List<int> orderReceived;
    private List<int> foodDone;

    // Start is called before the first frame update
    void Start()
    {
        orderReceived = new List<int>();
        foodDone = new List<int>();
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(orderReceived.Count > 0)
        {
            currentTime += Time.deltaTime * Time.timeScale;
            if(currentTime >= cookTime)
            {
                Debug.Log("Food Done");
                SfxPlayer.PlaySfx(SfxEnum.KitchenFoodDone);
                currentTime = 0;
                foodDone.Add(orderReceived[0]);
                orderReceived.RemoveAt(0);
                UpdateSprite();
            }
        }
    }

    public override void PlayerCommand(GameObject player)
    {

        if(Player.instance.hasOrder)
        {
            orderReceived.Add(Player.instance.orderNumber);
            Player.SetHasOrder(false, 0);
            Debug.Log("Order Recieved");
        }

        if(!Player.instance.hasFood && foodDone.Count > 0)
        {
            Player.SetHasFood(true, foodDone[0]);
            foodDone.RemoveAt(0);
            UpdateSprite();
            Debug.Log("Food Send");
        }
        player.GetComponent<Animator>().SetTrigger("NE");
    }

    private void UpdateSprite()
    {
        for(int i = 0; i < 3; i++)
        {
            if(foodDone.Count > i)
            {
                foodPosition[i].sprite = foodSprite[foodDone[i]];
            }
            else
            {
                foodPosition[i].sprite = null;
            }
        }
    }
}
