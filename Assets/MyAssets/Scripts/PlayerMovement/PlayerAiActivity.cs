using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAiActivity : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent myPlayer;

    [HideInInspector]
    public ClickToMove control;
    [HideInInspector]
    public SkinnedMeshRenderer visual;
    public AudioSource stepAudio;
    public FootSteps footsteps;

    public Transform busSeat;
    public bool followBus = false;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<NavMeshAgent>();
        control = GetComponent<ClickToMove>();
        visual = GetComponentInChildren<SkinnedMeshRenderer>();
        stepAudio = GetComponentInChildren<AudioSource>();
        footsteps = GetComponentInChildren<FootSteps>();

    }

    // Update is called once per frame
    void Update()
    {
        if (followBus)
        {
            myPlayer.speed = 17;
            control.enabled = false;
            visual.enabled = false;
            myPlayer.destination = busSeat.position;
            stepAudio.enabled = false;
            footsteps.enabled = false;

        }

    }

    void OnTriggerEnter(Collider col)
    {
        ObjectController objectController = GameObject.FindWithTag("Player").GetComponent<ObjectController>();

        if (col.tag == "Bus" && objectController.HasItem("BusTicket"))
        {

            followBus = true;

        }

    }
}
