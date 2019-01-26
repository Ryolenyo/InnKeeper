using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Node List")]
    public GameObject reception;

    private static Player instance;
    private NodeToNodeMovement movement;
    private GameObject customer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        movement = GetComponent<NodeToNodeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetDestination(GameObject destNode, GameObject hallNode)
    {
        instance.movement.SetDestination(destNode, hallNode);
    }

    public static void SetDestinationToReception()
    {
        instance.movement.SetDestination(instance.reception,
            instance.reception.GetComponent<RoomNode>().hallNode);
    }

    public static bool HasCustomer()
    {
        return instance.customer;
    }

    private void OnDestroy()
    {
        instance = null;
    }

}
