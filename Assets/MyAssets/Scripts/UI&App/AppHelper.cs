using UnityEngine;
using UnityEditor;

public class AppHelper : ScriptableObject
{
    public static void Quit(string reason)
    {
        Debug.Log(reason);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}