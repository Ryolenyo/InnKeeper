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

    [Header("Bed Sprite")]
    public SpriteRenderer bed;
    public Sprite goodBedSprite;
    public Sprite normalBedSprite;
    public Sprite badBedSprite;


    private bool isPlayerIn = false;
    private bool isCustomerCheckIn = false;
    private bool cleanFlag = false;

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
            currentCleaness += cleanRate * Time.deltaTime * Time.timeScale;
            currentCleaness = Mathf.Clamp(currentCleaness, 0, maxCleaness);
            if(currentCleaness == maxCleaness && !cleanFlag)
            {
                SfxPlayer.PlaySfx(SfxEnum.BedroomCleanDone);
                cleanFlag = true;
            }
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
            if(currentCleaness / maxCleaness >= 0.75)
                SfxPlayer.PlaySfx(SfxEnum.HeroRoomGood);
            else if(currentCleaness / maxCleaness > 0.5)
                SfxPlayer.PlaySfx(SfxEnum.HeroRoomNormal);
            else
                SfxPlayer.PlaySfx(SfxEnum.HeroRoomBad);
        }
        
    }

    public override void CustomerExitCommand(GameObject customer)
    {
        customer.transform.position = transform.position;
        customer.GetComponent<Hero>().isInRoom = false;
        currentCleaness -= (maxCleaness / 2);
        UpdateCleanImage();
    }

    public void Checkout()
    {
        isCustomerCheckIn = false;
        cleanFlag = false;
    }

    private void UpdateCleanImage()
    {
        UpdateBedSprite();
        Debug.Log(gameObject.name + " cleaness = " + currentCleaness);
    }

    private void UpdateBedSprite()
    {
        if(currentCleaness == maxCleaness)
        {
            bed.sprite = goodBedSprite;
        }
        else if(currentCleaness >= maxCleaness / 2)
        {
            bed.sprite = normalBedSprite;
        }
        else
        {
            bed.sprite = badBedSprite;
        }
    }
}
