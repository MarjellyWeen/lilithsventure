using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supervisor : MonoBehaviour
{
    public enum Behavior { DoNothing, Mentor, GoOutside }
    public Behavior currentBehavior;

    NavMeshAgent mentorAI;
    Animator mAnimator;

    public Transform targetA;
    public Transform targetB;

    public GameObject dialogueWindow;
    public GameObject triggerCol;
    public ItemPickupInfo itemPickupInfo;

    float distance;

    bool isTalking = false;

    //public Animator dialogueAnim;

    // Start is called before the first frame update

    void Start()
    {
        mentorAI = GetComponent<NavMeshAgent>();
        mAnimator = GetComponentInChildren<Animator>();
        itemPickupInfo = GetComponent<ItemPickupInfo>();

        //itemPickupInfo.enabled = false;

        //mentorAI.destination = targetB.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBehavior)
        {
            case Behavior.DoNothing:

                mentorAI.destination = targetB.position;
                mentorAI.speed = 8;

                break;

            case Behavior.Mentor:

                mentorAI.destination = targetA.position;
                mentorAI.speed = 2;
                StartTalking();

                break;

            case Behavior.GoOutside:

                mentorAI.destination = targetB.position;
                mentorAI.speed = 2;
                DoneTalking();               

                break;
        }
    }

    void StartTalking()
    {
        distance = Vector3.Distance(mentorAI.transform.position, targetA.transform.position);

        if (isTalking)
        {
            return;
        }

        if (distance <= 0.6f)
        {
        
            dialogueWindow.SetActive(true);
            GetComponent<DialogueTrigger>().TriggerDialogue();
            Destroy(triggerCol);
            isTalking = true;
            mAnimator.SetBool("Talking", true);
            mAnimator.SetBool("Walking", false);
            itemPickupInfo.enabled = false;
        }
        else
        {
            mAnimator.SetBool("Walking", true);
            isTalking = false;
            itemPickupInfo.enabled = true;
        }
    }

    void DoneTalking()
    {
        distance = Vector3.Distance(mentorAI.transform.position, targetB.transform.position);

        if (distance <= 0.6f)
        {
            mAnimator.SetBool("Talking", false);
            mAnimator.SetBool("Walking", false);
            itemPickupInfo.enabled = true;
        }

        else
        {
            mAnimator.SetBool("Talking", false);
            mAnimator.SetBool("Walking", true);
        }
    }

}
