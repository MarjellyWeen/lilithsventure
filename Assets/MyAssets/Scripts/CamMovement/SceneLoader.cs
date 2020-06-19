using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject activateCam;
    public GameObject [] disableCam;

    public bool firstSpawn = false;

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == "Player" && !firstSpawn )
        {
            Debug.Log("Entered Collider");

            foreach (GameObject cam in disableCam)
            {
                cam.SetActive(false);
            }

            activateCam.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            firstSpawn = false;
        }
    }

}
