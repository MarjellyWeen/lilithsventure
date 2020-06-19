using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorTrigger : MonoBehaviour
{
 public GameObject workingSupervisor;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Supervisor getAI = workingSupervisor.GetComponent<Supervisor>();
            getAI.currentBehavior = Supervisor.Behavior.Mentor;
        }
    }
}
