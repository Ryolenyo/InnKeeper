using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementStatusEnum
{
    Stop,
    Exiting,
    AtHall,
    Entering
}

public class NodeToNodeMovement : MonoBehaviour
{
    [Header("Paramenters")]
    public float speed = 5f;
    public bool isPlayer = false;

    [Header("Node List")]
    public GameObject currentNode;
    public GameObject exitHallNode;
    public GameObject enterHallNode;
    public GameObject targetNode;

    public MovementStatusEnum currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case MovementStatusEnum.Stop:
                break;
            case MovementStatusEnum.Exiting:
                MoveToNode(exitHallNode);
                if (transform.position == exitHallNode.transform.position) currentState = MovementStatusEnum.AtHall;
                break;
            case MovementStatusEnum.AtHall:
                MoveToNode(enterHallNode);
                if (transform.position == enterHallNode.transform.position) currentState = MovementStatusEnum.Entering;
                break;
            case MovementStatusEnum.Entering:
                MoveToNode(targetNode);
                if (transform.position == targetNode.transform.position)
                {
                    currentState = MovementStatusEnum.Stop;
                    currentNode = targetNode;
                    if (isPlayer) targetNode.GetComponent<RoomNode>().PlayerCommand(gameObject);
                    else targetNode.GetComponent<RoomNode>().CustomerCommand(gameObject);
                }
                break;
            default:
                break;
        }
    }

    public void SetDestination(GameObject destNode, GameObject hallNode)
    {
        
        if(currentNode == destNode && currentState == MovementStatusEnum.Stop)
        {
            if (isPlayer) currentNode.GetComponent<RoomNode>().PlayerCommand(gameObject);
            return;
        }

        if (targetNode == destNode)
        {
            return;
        }

        switch (currentState)
        {
            case MovementStatusEnum.Stop:
                exitHallNode = currentNode.GetComponent<RoomNode>().hallNode;
                enterHallNode = hallNode;
                targetNode = destNode;
                currentState = MovementStatusEnum.Exiting;

                if (isPlayer) currentNode.GetComponent<RoomNode>().PlayerExitCommand(gameObject);
                else currentNode.GetComponent<RoomNode>().CustomerExitCommand(gameObject);
                break;
            case MovementStatusEnum.Exiting:
                enterHallNode = hallNode;
                targetNode = destNode;
                if (destNode == currentNode)
                {
                    currentState = MovementStatusEnum.Entering;
                }
                break;
            case MovementStatusEnum.AtHall:
                enterHallNode = hallNode;
                targetNode = destNode;
                break;
            case MovementStatusEnum.Entering:
                if(destNode != targetNode)
                {
                    exitHallNode = enterHallNode;
                    enterHallNode = hallNode;
                    targetNode = destNode;
                    currentState = MovementStatusEnum.Exiting;
                }
                break;
            default:
                break;
        }
    }

    private void MoveToNode(GameObject destination)
    {
        /*Animation 
          Vector3 moveDirection =  (destination.transform.position - transform.position).normalized 
          animation(moveDirection.x, moveDirection.y)*/

        transform.position = Vector3.MoveTowards(transform.position
            , destination.transform.position, Time.deltaTime * speed);
    }
}
