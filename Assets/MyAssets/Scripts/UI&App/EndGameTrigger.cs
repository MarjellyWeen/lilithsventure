using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject creditRoll;
    public static bool gameHasEnded = false;

    public GameObject playingGameAudio;

    void Update()
    {
        if (gameHasEnded)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

        void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("End of map, end of game");
            //UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
            Time.timeScale = 0.00001f;
            creditRoll.SetActive(true);
            gameHasEnded = true;
            playingGameAudio.SetActive(false);
            
        }
    }
}
