using UnityEngine;

public class Variabless : MonoBehaviour
{
    public static Transform Player;
    public Transform Playerr;
    public static Transform Reception;
    public Transform Receptionn;
    public static Transform[] qnodes;
    public static bool[] occupied = new bool[4];
    private void Awake()
    {
        qnodes = new Transform[transform.childCount];
        Debug.Log(transform.childCount);
        for (int i = 0; i < qnodes.Length; i++)
        {
            qnodes[i] = transform.GetChild(i);
        }
        for (int i = 0; i < 4; i++)
        {
            occupied[i] = false;
        }
    }
    private void FixedUpdate()
    {
        Player = Playerr;
        Reception = Receptionn;
        if (Vector3.Distance(Player.position, Reception.position) <= 0.2f && Spawner.customerCount > 0)
        {
            Spawner.customerCount--;
        }
    }
}
