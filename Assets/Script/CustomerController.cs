using UnityEngine;

public class CustomerController : MonoBehaviour
{
    private bool front = false;
    public Transform DestNode;
    int speed = 3;
    int q = 3;
    void Start()
    {
        DestNode = Variabless.qnodes[q];
    }

    void FixedUpdate()
    {
        if (q == 0&& Vector3.Distance(transform.position, DestNode.position) <= 0.2f)
            front = true;
        transform.position = Vector2.MoveTowards(transform.position, DestNode.position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, DestNode.position) <= 0.2f && q > 0 && !Variabless.occupied[q - 1])
        {
            Variabless.occupied[q] = false;
            Variabless.occupied[q - 1] = true;
            q--;
            DestNode = Variabless.qnodes[q];
        }
        else if (Vector3.Distance(Variabless.Player.position,Variabless.Reception.position) <= 0.2f&&front)
        {
            Variabless.occupied[q] = false;
            Destroy(gameObject);

        }
    }
    

}
