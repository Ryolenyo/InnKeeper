using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFromNodeToNode : MonoBehaviour
{

    public static Transform DestNode;

    public static int speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        DestNode = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != DestNode.position){
            transform.position = Vector2.MoveTowards(transform.position,DestNode.position,Time.deltaTime*speed);
        }
    }
}
