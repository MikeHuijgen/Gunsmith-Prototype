using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttachmentButtonID : MonoBehaviour
{
    public Action<string> ClickedOnAttachment;
    private TMP_Text _buttonText;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(CLickedOnButton);
        _buttonText = GetComponentInChildren<TMP_Text>();
    }

    private void CLickedOnButton()
    {
        ClickedOnAttachment?.Invoke(_buttonText.text);
    }
}
