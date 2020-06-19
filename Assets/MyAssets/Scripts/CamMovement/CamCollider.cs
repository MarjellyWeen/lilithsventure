using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollider : MonoBehaviour
{
    public CamCamera triggerCam;
    public Color ambientColor;
    private CamDad parent;


    // Start is called before the first frame update
    void Start()
    {
        // Gets the parent "CamDad" if present
        parent = GetComponentInParent<CamDad>();

        // Make sure this collider is a child of parent "CamDad", if not; exit the game
        if (parent == null)
        {
            AppHelper.Quit("CamCollider \"" + name + "\" needs to be a child of CamDad.");
        }

        // Make sure a triggerCam is set on this collider, if not; exit the game
        if (triggerCam == null)
        {
            AppHelper.Quit("CamCollider \"" + name + "\" needs an existing CamCamera to trigger.");
        }
    }

    // If the player triggers/enters the collider activate the triggerCam
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            parent.Activate(triggerCam, ambientColor);
        }
    }
}
