using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Transform player;
    public Transform destHall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.position == moveFromNodeToNode.myHall.position)
        {
            moveFromNodeToNode.DestNode = moveFromNodeToNode.DestHall;
            moveFromNodeToNode.NowNode = moveFromNodeToNode.myHall;
        }
        if (player.position == moveFromNodeToNode.DestHall.position)
        {
            moveFromNodeToNode.myHall = moveFromNodeToNode.DestHall;
            moveFromNodeToNode.DestNode = moveFromNodeToNode.FinalNode;
            moveFromNodeToNode.NowNode = moveFromNodeToNode.DestHall;
        }
    }

    void OnMouseDown()
    {
        moveFromNodeToNode.DestNode = moveFromNodeToNode.myHall; 
        moveFromNodeToNode.FinalNode = transform;
        moveFromNodeToNode.DestHall = destHall;
    }
}
