using UnityEngine;
using TMPro;


public class TurnTextBoxScript : MonoBehaviour
{
    public TextMeshPro component;
    public static string text = "Turn White";

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshPro>().text = text;
    }
}

