using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;

    [TextArea(3, 10)] // Permet de cr�er des zonnes de texte plus grande
    public string[] sentences;


}
