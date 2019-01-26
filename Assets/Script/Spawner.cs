using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject customer;
    public static int customerCount = 0;
    void Start()
    {
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        if (customerCount < 3)
        {
            customerCount++;
            Instantiate(customer, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }
}
