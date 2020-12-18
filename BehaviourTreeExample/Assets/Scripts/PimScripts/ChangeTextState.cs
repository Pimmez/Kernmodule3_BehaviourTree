using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTextState : MonoBehaviour
{
    [SerializeField] private TMP_Text textGuard = null;
    [SerializeField] private TMP_Text textRoque = null;

    public string TextStringGuard { get { return textStringGuard; } set { textStringGuard = value; } }
    private string textStringGuard;
    public string TextStringRoque { get { return textStringRoque; } set { textStringRoque = value; } }
    private string textStringRoque;

    private void Awake()
    {
        //textGuard = GetComponent<TMP_Text>();
        //textRoque = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textGuard.text = "" + textStringGuard;
        textRoque.text = "" + textStringRoque;
    }
}