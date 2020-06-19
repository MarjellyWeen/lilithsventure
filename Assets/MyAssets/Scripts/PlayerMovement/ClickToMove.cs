using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
    Animator mAnimator;
    NavMeshAgent agent;
    Rigidbody m_Rigidbody;

    //bool running = false;
    bool m_IsGrounded;

    float m_GroundCheckDistance = 0.1f;
    float m_ForwardAmount;
    float m_AnimSpeedMultiplier = 1f;

    Vector3 m_GroundNormal;



    Vector3 newPos, oldPos;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        newPos = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                agent.destination = hit.point;

                GetComponent<ObjectController>().currentItem = hit.transform.gameObject;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Move(agent.desiredVelocity);
                //running = false;

            }
            else
            {
                //running = true;
                Move(Vector3.zero);

            }

            //mAnimator.SetBool("running", running);
        }

        Vector3 currentVel = newPos - oldPos;
        float agentVelocity = (currentVel.magnitude*100000);

       /* if (agentVelocity < 0)
        {
            agentVelocity  *= -1;
        }*/
        mAnimator.SetFloat("Forward", agentVelocity);

        //Debug.Log(agentVelocity);
        oldPos = newPos;
    }

    public void Move(Vector3 move)
    {

        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);


        UpdateAnimator(move);
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            mAnimator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            mAnimator.applyRootMotion = false;
        }
    }

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters

        mAnimator.SetBool("OnGround", m_IsGrounded);


        if (m_IsGrounded && move.magnitude > 0)
        {
            mAnimator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            mAnimator.speed = 1;
        }
    }
}


