using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFromNodeToNode : MonoBehaviour
{

    public static Transform NowNode;
    public Transform nowwww;
    public static Transform DestNode;
    public static Transform FinalNode;

    public Transform startHall;
    public static Transform myHall;
    public static Transform DestHall;

    public int speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        DestNode = transform;
        NowNode = transform;
        myHall = startHall;
        DestHall = startHall;
        FinalNode = transform;
    }

    // Update is called once per frame
    void Update()
    {
        nowwww = NowNode;
        if (transform.position != DestNode.position){
            transform.position = Vector2.MoveTowards(transform.position,DestNode.position,Time.deltaTime*speed);
        }
        else
        {
            NowNode = DestNode;
        }
    }
}
