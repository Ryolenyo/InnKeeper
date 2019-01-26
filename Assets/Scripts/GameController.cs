using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] heroPrefabs;
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!flag)
        {
            spawnNewHero();
            spawnNewHero();
            flag = true;
        }
    }

    void spawnNewHero()
    {
        GameObject newHero = Instantiate(heroPrefabs[Random.Range(0, heroPrefabs.Length)]) as GameObject;
        Debug.Log(newHero);
        ReceptionRoom.instance.AddHeroCheckin(newHero.GetComponent<Hero>());
    }
}
