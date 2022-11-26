using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;

    [TextArea(3, 10)] // Permet de créer des zonnes de texte plus grande
    public string[] sentences;


}
