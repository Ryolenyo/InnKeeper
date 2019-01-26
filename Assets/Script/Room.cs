using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
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
        if (player.position == PlayerBehaviour.myHall.position)
        {
            PlayerBehaviour.DestNode = PlayerBehaviour.DestHall;
            PlayerBehaviour.NowNode = PlayerBehaviour.myHall;
        }
        if (player.position == PlayerBehaviour.DestHall.position)
        {
            PlayerBehaviour.myHall = PlayerBehaviour.DestHall;
            PlayerBehaviour.DestNode = PlayerBehaviour.FinalNode;
            PlayerBehaviour.NowNode = PlayerBehaviour.DestHall;
        }
    }

    void OnMouseDown()
    {
        PlayerBehaviour.DestNode = PlayerBehaviour.myHall;
        PlayerBehaviour.FinalNode = transform;
        PlayerBehaviour.DestHall = destHall;
    }
}
