using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{    
    public Sentences[] promt;
}

[System.Serializable]
public class Sentences
{
    public string name;
    [TextArea(3, 10)]
    public string sentence;
    public Color color;
}