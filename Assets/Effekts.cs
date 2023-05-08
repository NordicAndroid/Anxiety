using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effekts : MonoBehaviour
{
    public GameObject[] Healtlevels;
    public int hitCount;
 

    public GameObject Bordereffekt;

    private void Start()
    {
        hitCount = 1;   
    }

    private void Update()
    {
        for (int i = 1; i < Healtlevels.Length; i++)
        {

        Healtlevels[i].SetActive(false);
        Healtlevels[hitCount].SetActive(true);

        }
    }

    public void HitsInc()
    {
        hitCount++;
    }
}
