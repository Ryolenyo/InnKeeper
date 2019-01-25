using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Transform player;
    public Transform HallNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position == HallNode.position)
        {
            moveFromNodeToNode.DestNode = transform;
            Debug.Log("yo");
        }
    }

    void OnMouseDown()
    {
        moveFromNodeToNode.DestNode = HallNode;
    }
}
