using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDad : MonoBehaviour
{
    private CamCamera activeCam;
    
    public string activeText;

    // Start is called before the first frame update
    
    void Start()
    {
        // Gets the active CamCamera
        CamCamera[] cameras = GetComponentsInChildren<CamCamera>();

        // Simply checks if there's only one camera active, if not; exit the game
        if (cameras.Length > 1)
        {
            AppHelper.Quit("More than one active camera!");
        }
        else
        {
            // Activates the only active camera
            activeCam = cameras[0];
            activeText = activeCam.name;
        }
    }

    // Activates a new camera through "CamCollider"
    public void Activate(CamCamera camera, Color ambient)
    {
        // Deactivates the current cam
        activeCam.gameObject.SetActive(false);

        // Reference the new active cam
        activeCam = camera;
        activeText = activeCam.name;

        // Activate the new active cam
        activeCam.gameObject.SetActive(true);
        RenderSettings.ambientLight = ambient;
    }
}
