﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionRoom : RoomNode
{

    [Header("Queue Position Node")]
    public GameObject queuePosition0;
    public GameObject queuePosition1;
    public GameObject queuePosition2;

    [Header("Checkout Position Node")]
    public GameObject checkoutPosition0;
    public GameObject checkoutPosition1;
    public GameObject checkoutPosition2;
    public GameObject checkoutPosition3;

    [Header("Player Position Node")]
    public GameObject playerPositionNode;
    public GameObject playerCustomerPositionNode;

    public static ReceptionRoom instance;
    private List<Hero> checkinList;
    private List<Hero> checkoutList;

    // Start is called before the first frame update

    void Start()
    {
        instance = this;
        checkinList = new List<Hero>();
        checkoutList = new List<Hero>();
    }

    void OnDestroy()
    {
        instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PlayerCommand(GameObject player)
    {
        player.transform.position = playerPositionNode.transform.position;

        while (checkoutList.Count > 0)
        {
            Hero currentHero = checkoutList[0];
            checkoutList.RemoveAt(0);
            currentHero.Checkout();
        }

        if(checkinList.Count > 0 && !Player.instance.customer)
        {
            Player.instance.customer = checkinList[0].gameObject;
            checkinList[0].transform.position = playerCustomerPositionNode.transform.position;
            checkinList[0].GetComponent<Animator>().SetTrigger("SW");
            checkinList.RemoveAt(0);
        }

        UpdateHeroPosition();
        player.GetComponent<Animator>().SetTrigger("SE");
    }

    public override void PlayerExitCommand(GameObject player)
    {
        player.transform.position = transform.position;
    }


    public override void CustomerCommand(GameObject customer)
    {
        if(customer.GetComponent<Hero>().isCheckout)
        {
            checkoutList.Add(customer.GetComponent<Hero>());
            SfxPlayer.PlaySfx(SfxEnum.HeroCheckout);
            UpdateHeroPosition();
        }
    }


    public bool IsCheckinFull()
    {
        return checkinList.Count >= 3;
    }

    public void AddHeroCheckin(Hero hero)
    {
        if(!IsCheckinFull())
        {
            checkinList.Add(hero);
            UpdateHeroPosition();
            SfxPlayer.PlaySfx(SfxEnum.HeroCheckIn);
        }
    }

    public void UpdateHeroPosition()
    {
        if (checkinList.Count >= 1)
        {
            checkinList[0].transform.position = queuePosition0.transform.position;
            checkinList[0].GetComponent<Animator>().SetTrigger("NW");
        }
        if (checkinList.Count >= 2)
        {
            checkinList[1].transform.position = queuePosition1.transform.position;
            checkinList[1].GetComponent<Animator>().SetTrigger("NW");
        }
        if (checkinList.Count >= 3)
        {
            checkinList[2].transform.position = queuePosition2.transform.position;
            checkinList[2].GetComponent<Animator>().SetTrigger("NW");
        }

        if (checkoutList.Count >= 1)
        {
            checkoutList[0].transform.position = checkoutPosition0.transform.position;
            checkoutList[0].GetComponent<Animator>().SetTrigger("NW");
        }
        if (checkoutList.Count >= 2)
        {
            checkoutList[1].transform.position = checkoutPosition1.transform.position;
            checkoutList[1].GetComponent<Animator>().SetTrigger("NW");
        }
        if (checkoutList.Count >= 3)
        {
            checkoutList[2].transform.position = checkoutPosition2.transform.position;
            checkoutList[2].GetComponent<Animator>().SetTrigger("NW");
        }
        if (checkoutList.Count >= 4)
        {
            checkoutList[3].transform.position = checkoutPosition3.transform.position;
            checkoutList[3].GetComponent<Animator>().SetTrigger("NW");
        }
    }
}
