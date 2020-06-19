using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusDriving : MonoBehaviour
{
    NavMeshAgent bus;

    BoxCollider busCollider;

    public Transform pointA;
    public Transform pointB;

    float distance;

    public PlayerAiActivity playerController;


    public Transform exitBus;
    public GameObject playerNoEntryCol;

    //public ObjectController objectController;



    // Start is called before the first frame update
    void Start()
    {
        bus = GetComponent<NavMeshAgent>();
        busCollider = GetComponent<BoxCollider>();
        //playerController = GetComponent<PlayerAiActivity>();
        
        bus.destination = pointA.position;
    }

    void Update()
    {
        distance = Vector3.Distance(bus.transform.position, pointB.transform.position);

        if (distance <= 0.6f)
        {
            
            playerController.followBus = false;
            playerController.myPlayer.speed = 4;
            playerController.visual.enabled = true;
            //playerController.myPlayer.destination = exitBus.position;
            playerController.control.enabled = true;
            playerController.stepAudio.enabled = true;
            playerController.footsteps.enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        ObjectController objectController = col.gameObject.GetComponent<ObjectController>();

        if (col.tag == "Player" && objectController.HasItem("BusTicket"))
        {

            Debug.Log("Player Entered bus");
            
            playerNoEntryCol.SetActive(true);
            bus.destination = pointB.position;           


        }

    }
}
