using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyField : MonoBehaviour
{
    public Transform centerPoint;
    public GameObject player;
    PlayerController playerContr;
    public LayerMask playerLayer;

    SphereCollider currentCollider;
    SphereCollider[] colliderArray;
    bool playerInArea = false;
    public int playerIndex = 0;
    public float[] anxRadiusArray;
    public int[] CrossIncrease;
    public int[] DecreaseMultiplier;

    Collider[] playerhit;
    

    // Start is called before the first frame update
    void Start()
    {
        currentCollider = GetComponent<SphereCollider>();
        currentCollider.radius = anxRadiusArray[0];
        playerContr = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerInArea){
            PlayerArea();
        }
    }

    void PlayerArea() 
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position) - 1;
        //Debug.Log(distance);
        
        for (int i = 0; i < anxRadiusArray.Length; i++)
        {
            if (distance < anxRadiusArray[i] && distance > anxRadiusArray[i + 1] && playerIndex > i+1){
                    playerIndex = i + 1;
                }
            if (distance < anxRadiusArray[i] && distance > anxRadiusArray[i+1] && playerIndex < i+1)
            {
                playerContr.IncAnxiety(CrossIncrease[i]);
                playerIndex++;
                //Debug.Log("Player has entered zone " + (i+1) + " and has been given " + CrossIncrease[i] + " Anxiety");     
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().currentfield = this;
            playerInArea = true;
            Debug.Log("Player has Entered and been given " + CrossIncrease[0] + " Anxiety");
        }

    }
    private void OnTriggerExit(Collider other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().currentfield = null;
            playerIndex = 0;
            playerInArea = false;
            Debug.Log("Player has left");
        } 
    }

    //Bruges til at se området hvor fjender vil blive fundet.
    void OnDrawGizmosSelected()
    {
        //Siden overlappet er en cirkel bruger jeg en sphere.
        for (int i = 0; i < anxRadiusArray.Length; i++)
        {
        Gizmos.DrawWireSphere(centerPoint.position, anxRadiusArray[i]);
        }
    }
}
