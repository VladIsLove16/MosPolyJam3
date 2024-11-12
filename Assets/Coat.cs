using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coat : MonoBehaviour
{
    public int chanceToShow = 5;
    // Start is called before the first frame update
    void Awake()
    {
        if(Random.Range(0,100) > chanceToShow)
        {
            gameObject.SetActive(false);
        }
    }
}
