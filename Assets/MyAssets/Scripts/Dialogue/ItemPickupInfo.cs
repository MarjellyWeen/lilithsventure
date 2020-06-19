using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickupInfo : MonoBehaviour
{
    public ObjectController objectController;
    public Animator animator;
    public GameObject dialogueWindow;

    public enum ItemTags { Money, Muffin, BusTicket, Empty }
    public ItemTags requiredTag;
    public ItemTags giveTag;

    public Sentences giveItemSucceed;
    public Sentences giveItemFail;
    public Sentences hasItem;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        
    }

    void OnMouseDown() 
    {
        audioSource.PlayOneShot(audioClip);

        //Debug.Log("Clicked an NPC");
        if (objectController.HasItem(giveTag.ToString()))
        {
            DisplayDialogue(hasItem);
            return;
        }

        if (objectController.HasItem(requiredTag.ToString()))
        {
            //give item
            objectController.PickUpItem(giveTag.ToString());
            //show succes dialogue
            DisplayDialogue(giveItemSucceed);
            //play HandingOver animation
            animator.SetTrigger("GiveItem");

            
        }
        else
        {
            DisplayDialogue(giveItemFail);
            
            //show fail dialogue
        }
        //dialogueWindow.SetActive(true);
        //GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    void DisplayDialogue(Sentences sentence)
    {
        dialogueWindow.SetActive(true);

        Dialogue dialogue = new Dialogue();
        Sentences[] sentences = new Sentences[1];

        Debug.Log(dialogue);
        sentences[0] = sentence;
        dialogue.promt = sentences;
        //dialogue.promt.SetValue(sentence, 0);

        DialogueManager.instance.StartDialogue(dialogue);
    }
}
