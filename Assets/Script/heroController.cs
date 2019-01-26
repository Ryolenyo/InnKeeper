using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Transform hero;
    public Transform heroHall;
    public Transform heroTable;
    public Transform heroRoom;
    public Transform reception;

    public bool isGetRoom = false;
    public bool isHungry = false;
    public bool isCheckOut = false;
    public bool isAtHall = false;
    // Start is called before the first frame update

    public int step = 4; // nothing state

    public float time;
    public float nextEventTime;

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * Time.timeScale;

        //timeGenerator();

        if (isGetRoom)
        {
            if (!isAtHall)
            {
                goHall();
                isAtHall = true;
            }
            if (hero.position == heroHall.position)
            {
                HeroBehaviour.heroDest = heroRoom; 
                isGetRoom = false;
                isAtHall = false;
            }
        }

        if (isHungry)
        {
            if (!isAtHall)
            {
                goHall();
                isAtHall = true;
            }
            if (hero.position == heroHall.position)
            {
                HeroBehaviour.heroDest = heroTable;
                isHungry = false;
                isAtHall = false;
            }
        }

        if (isCheckOut)
        {
            if (!isAtHall)
            {
                goHall();
                isAtHall = true;
            }
            if (hero.position == heroHall.position)
            {
                HeroBehaviour.heroDest = reception;
                isCheckOut = false;
                isAtHall = false; 
            }
        }
    }

    void goHall()
    {
        HeroBehaviour.heroDest = heroHall;
    }

    void timeGenerator()
    {
        if (step == 0) //check in success, go to hero's room
        {
            isGetRoom = true;
            nextEventTime = Random.Range(5f,15f)+time;
        } 
        else if (step == 1) //eating
        {
            isHungry = true;
            nextEventTime = Random.Range(5f,10f)+time;
        }
        else if (step == 2)
        {
            isGetRoom = true;
            nextEventTime = Random.Range(5f,10f)+time;
        }
        else if (step == 3)
        {
            isCheckOut = true;
        }
    }

}
