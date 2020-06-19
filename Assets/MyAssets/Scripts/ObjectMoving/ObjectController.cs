using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectController : MonoBehaviour
{


    public InventoryItem[] itemList;
    public GameObject shapeShifter;
    public GameObject shifterPickup;


    public GameObject currentItem;


    public bool hasShapeshifter = false;

    public AudioSource audioSource;

    public AudioClip[] audioFiles;

    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ShapeShifter();
    }


    // Controls the shapeshifter which gives hints
    void ShapeShifter()
    {
        //if (!hasShapeshifter)
        //{
        //    return;
        //}

        if (Input.GetButtonDown("Fire2"))
        {
            if (shapeShifter.activeInHierarchy == false)
            {
                shapeShifter.SetActive(true);
                audioSource.clip = audioFiles[0];
                audioSource.PlayOneShot(audioSource.clip);
                animator.SetBool("activateShifter", true);
            }

            else
            {
                shapeShifter.SetActive(false);
                audioSource.clip = audioFiles[1];
                audioSource.PlayOneShot(audioSource.clip);
                animator.SetBool("activateShifter", false);
            }
        }

    }

    public bool HasRemoveItem(string tag)
    {
        foreach (InventoryItem item in itemList)
        {
            if (item.itemTag.ToString() == tag && item.hasItem)
            {
                item.Remove();
                return true;
            }
        }

        return false;
    }

    public void PickUpItem(string tag)
    {
        foreach (InventoryItem item in itemList)
        {
            if (item.itemTag.ToString() == tag && !item.hasItem)
            {
                item.PickUp();
            }
        }
    }

    public bool HasItem(string tag)
    {
        foreach (InventoryItem item in itemList)
        {
            if (item.itemTag.ToString() == tag && item.hasItem)
            {
                return true;
            }
        }

        return false;
    }

    public void PlayAudio(AudioClip audioFile)
    {

        audioSource.PlayOneShot(audioFile);
    }

    private void OnTriggerStay(Collider col)
    {
        //do not run pickup code if 
        //audio is playing or
        //left mouse button(fire1) is not clicked or
        //collision object is not current item

        if (audioSource.isPlaying || col.gameObject != currentItem)
        {
            return;
        }

        PickUpItem(col.tag);



        //if (col.tag == "Shapeshifter" && col.gameObject == currentItem)
        //{
        //    Debug.Log("You have picked up your shapeshifter");
        //    hasShapeshifter = true;
        //    DisableObjectVisual(shifterPickup);
        //}

    }

    private void DisableObjectVisual(GameObject element)
    {
        MeshRenderer[] meshRenderers = element.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }

        element.GetComponent<Collider>().enabled = false;
    }
}




[Serializable]
public class InventoryItem
{
    public enum ItemTags { Blank, Breakfast, Money, Muffin, BusTicket }
    public ItemTags itemTag;
    public bool hasItem = false;

    public GameObject itemObject;

    public GameObject itemIcon;
    public AudioClip audioFile;

    public ItemTags requiredTag;

    private ObjectController controller;

    public void PickUp()
    {
        bool canPickup = false;

        if (controller == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            controller = player.GetComponent<ObjectController>();
        }

        if (requiredTag.ToString() != "Blank")
        {
            if (controller.HasRemoveItem(requiredTag.ToString()))
            {
                canPickup = true;
            }

            else
            {
                Debug.Log("You are missing the item" + requiredTag.ToString());
            }
        }
        else
        {
            canPickup = true;
        }

        if (canPickup)
        {
            hasItem = true;
            controller.PlayAudio(audioFile);
            controller.animator.SetTrigger("Grab");
            itemIcon.SetActive(true);
            DisableObjectVisual();

            Debug.Log("Picked up item " + itemTag.ToString());
        }
    }

    public void Remove()
    {
        hasItem = false;
        itemIcon.SetActive(false);
    }

    private void DisableObjectVisual()
    {
        if (itemObject == null || itemObject.GetComponent<MeshRenderer>() == null)
        {
            return;
        }

        itemObject.GetComponent<MeshRenderer>().enabled = false;           
        itemObject.GetComponent<Collider>().enabled = false;
    }

}
