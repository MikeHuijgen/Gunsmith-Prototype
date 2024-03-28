using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsmithSystem : MonoBehaviour
{
    [SerializeField] private Canvas gunsmithCanvas;
    [SerializeField] private Canvas attachmentRowCanvas;

    public void EnableAttachmentCanvas()
    {
        attachmentRowCanvas.enabled = true;
        gunsmithCanvas.enabled = false;
    }

    public void EnableGunsmithCanvas()
    {
        gunsmithCanvas.enabled = true;
        attachmentRowCanvas.enabled = false;
    }
}
