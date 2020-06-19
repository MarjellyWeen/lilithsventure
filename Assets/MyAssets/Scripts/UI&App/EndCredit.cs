using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredit : MonoBehaviour
{
    //public EndGameTrigger endGameTrigger;
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (endGameTrigger.gameHasEnded)
        //{
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            animator.updateMode = AnimatorUpdateMode.UnscaledTime; 
        //}
    }
}
