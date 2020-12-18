using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTextState : MonoBehaviour
{
    private TMP_Text text;

    public string TextString { get { return textString; } set { textString = value; } }
    private string textString;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = "Guard BT States :::: " + textString;    
    }
}