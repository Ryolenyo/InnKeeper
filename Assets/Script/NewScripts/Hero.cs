﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public bool isCheckIn = false;
    public bool isInRoom = false;
    public bool isHungry = false;
    public bool isEating = false;
    public bool isCheckout = false;
    public bool isDone = false;
    public bool impression = false;

    public int Emotion = 50;
    public int roomQuality = 1; //Good = 1 , Little Mess = 0 , Bad = -1
    public int heroOrder; //Menu 0 , Menu 1 , Menu 2
    public int serveOrder; //Right +10 , Wrong -10

    private float time = 0;
    private float HungryTime;
    private float EatingTime;
    private float CheckoutTime;

    private BedroomNode roomNode;
    private TableRoomNode tableNode;
    private NodeToNodeMovement movement;

    [Header("State Time")]
    public float hungryMin = 5f;
    public float hungryMax = 8f;
    public float eatingMin = 2f;
    public float eatingMax = 5f;
    public float checkoutMin = 5f;
    public float checkoutMax = 10f;

    [Header("Time & Score")]
    public float checkinTime = 5f;
    public int checkinPenalty = 2;
    public float tableTime = 10f;
    public int tablePenalty = 5;
    public float checkoutTime = 5f;
    public int checkoutPenalty = 5;

    [Header("Emotion")]
    public SpriteRenderer emotionSprite;
    public Sprite goodEmotion;
    public Sprite normalEmotion;
    public Sprite badEmotion;

    // Start is called before the first frame update
    void Start()
    {
        //Random hero time
        heroOrder = Random.Range(0, 2);
        HungryTime = Random.Range(hungryMin, hungryMax);
        EatingTime = Random.Range(eatingMin, eatingMax);
        CheckoutTime = Random.Range(checkoutMin, checkoutMax);
        movement = GetComponent<NodeToNodeMovement>();
        movement.currentNode = ReceptionRoom.instance.gameObject;
        movement.exitHallNode = ReceptionRoom.instance.hallNode;
        UpdateEmotionSprite();
    }

    void Update()
    {
        //Waiting For Check In
        if (!isCheckIn)
        {
            time += Time.deltaTime * Time.timeScale;
            if (time > checkinTime) //Decrese Emotion Value When > 5 sec (-5 point per 5 sec)
            {
                Emotion -= checkinPenalty;
                time = 0;
                UpdateEmotionSprite();
            }
        }

        //Rest in the Room and Waiting for Hungry
        else if (isInRoom && !isHungry && !isCheckout)
        {
            if (!isEating)
            {
                time += Time.deltaTime * Time.timeScale;
                if (time > HungryTime)
                {
                    Debug.Log("I'm hungry.");
                    isHungry = true;
                    time = 0;

                    movement.SetDestination(roomNode.tableRoomNode.gameObject, roomNode.tableRoomNode.hallNode);

                    Debug.Log("I want Menu " + heroOrder);
                }
            }
            else //check out
            {
                time += Time.deltaTime * Time.timeScale;
                if (time > CheckoutTime)
                {
                    Debug.Log("I Gotta go.");
                    isCheckout = true;
                    time = 0;
                    roomNode.Checkout();
                    movement.SetDestination(ReceptionRoom.instance.gameObject, ReceptionRoom.instance.hallNode);
                }
            }
        }

        //Hero is hungry, go to the table
        else if (isHungry && !isEating)
        {

            time += Time.deltaTime * Time.timeScale;
            if (time > tableTime) //Decrese Emotion Value When > 10 sec (-5 point per 10 sec)
            {
                Emotion -= tablePenalty;
                time = 0;
                UpdateEmotionSprite();
            }
        }

        //Eating Duration
        else if (isHungry && isEating && !isInRoom)
        {
            time += Time.deltaTime * Time.timeScale;
            if (time > EatingTime)
            {
                movement.SetDestination(roomNode.gameObject, roomNode.hallNode);
                isHungry = false;
            }
        }

        //Hero at reception
        else if (isCheckout)
        {
            time += Time.deltaTime * Time.timeScale;
            if (time > checkoutTime) //Decrese Emotion Value When > 5 sec (-5 point per 5 sec)
            {
                Emotion -= checkoutPenalty;
                time = 0;
                UpdateEmotionSprite();
            }
        }
    }

    public void AssignRoom(BedroomNode roomNode)
    {
        isCheckIn = true;
        this.roomNode = roomNode;
        movement.SetDestination(roomNode.gameObject, roomNode.hallNode);
        Player.instance.customer = null;
    }

    public void AssignTable(TableRoomNode tableNode)
    {
        this.tableNode = tableNode;
        tableNode.hasOrder = true;
        tableNode.orderNumber = heroOrder;
    }

    public void ReceivedFood()
    {
        isEating = true;
        Emotion += 20;
        time = 0;
        UpdateEmotionSprite();
    }

    public void Checkout()
    {
        isDone = true;
        GameTracker.AddScore(Emotion);
        if(Emotion >= 70)
            SfxPlayer.PlaySfx(SfxEnum.ResultGood);
        else if(Emotion >= 30)
            SfxPlayer.PlaySfx(SfxEnum.ResultNormal);
        else
            SfxPlayer.PlaySfx(SfxEnum.ResultBad);
        Destroy(gameObject);
    }

    public void UpdateEmotionSprite()
    {
        if(Emotion >= 70)
        {
            emotionSprite.sprite = goodEmotion;
        }
        else if(Emotion >= 30)
        {
            emotionSprite.sprite = normalEmotion;
        }
        else
        {
            emotionSprite.sprite = badEmotion;
        }
    }
}
