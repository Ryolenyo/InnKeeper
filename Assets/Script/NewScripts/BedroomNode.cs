using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomNode : RoomNode
{
    [Header("Position Node")]
    public GameObject sleepNode;
    public GameObject cleanNode;

    [Header("Cleaness")]
    public float maxCleaness = 100;
    public float cleanRate = 34;
    private float currentCleaness;

    private bool isPlayerIn = false;
    private bool isCustomerCheckIn = false;

    // Start is called before the first frame update
    void Start()
    {
        currentCleaness = maxCleaness;
        UpdateCleanImage();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerIn)
        {
            currentCleaness += cleanRate * Time.deltaTime;
            currentCleaness = Mathf.Clamp(currentCleaness, 0, maxCleaness);
            UpdateCleanImage();
        }
    }

    public override void PlayerCommand(GameObject player)
    {
        if(!isCustomerCheckIn)
        {
            if(Player.HasCustomer())
            {
                Player.SetDestinationToReception();
                isCustomerCheckIn = true;
            }
            else
            {
                player.transform.position = cleanNode.transform.position;
                isPlayerIn = true;
            }
            
        }
        else
        {
            Debug.Log("Already has customer.");
            //alarm text
        }
    }

    public override void PlayerExitCommand(GameObject player)
    {
        player.transform.position = transform.position;
        isPlayerIn = false;
    }

    public override void CustomerCommand(GameObject customer)
    {
        customer.transform.position = sleepNode.transform.position;
        //customer.assignroom(this)
    }

    public override void CustomerExitCommand(GameObject customer)
    {
        customer.transform.position = transform.position;
    }

    public void Checkout()
    {
        isCustomerCheckIn = false;
        currentCleaness = 0;
        UpdateCleanImage();
    }

    private void UpdateCleanImage()
    {
        //SetImage
        Debug.Log(gameObject.name + " cleaness = " + currentCleaness);
    }
}
