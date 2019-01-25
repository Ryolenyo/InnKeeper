using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFromNodeToNode : MonoBehaviour
{

    public static Transform DestNode;

    public int speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != DestNode.position){
            transform.position = Vector2.MoveTowards(transform.position,DestNode.position,Time.deltaTime*speed);
        }
    }
}
