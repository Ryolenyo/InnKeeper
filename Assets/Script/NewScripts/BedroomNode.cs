using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomNode : RoomNode
{
    [Header("Room Node")]
    public TableRoomNode tableRoomNode;

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
                isCustomerCheckIn = true;
                Player.instance.customer.GetComponent<Hero>().AssignRoom(this);

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
        customer.GetComponent<Hero>().isInRoom = true;
        if (!customer.GetComponent<Hero>().impression)
        {
            customer.GetComponent<Hero>().impression = true;
            customer.GetComponent<Hero>().Emotion += (int)((currentCleaness - 50));
        }
        
    }

    public override void CustomerExitCommand(GameObject customer)
    {
        customer.transform.position = transform.position;
        customer.GetComponent<Hero>().isInRoom = false;
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
