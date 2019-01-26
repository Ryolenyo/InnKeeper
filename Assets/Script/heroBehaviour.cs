using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroBehaviour : MonoBehaviour
{
    // DONT FORGET TO PLUS BOOKING TIME
    
    private int speed = 5;

    public Transform heroStartHall;
    public Transform heroRoom;
    public static Transform heroDest;


    // Start is called before the first frame update
    void Start()
    {
        heroDest = transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position != heroDest.position){
            transform.position = Vector2.MoveTowards(transform.position,heroDest.position,Time.deltaTime*speed);
        }

    }

}
