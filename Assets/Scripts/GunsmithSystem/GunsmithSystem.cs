using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsmithSystem : MonoBehaviour
{
    [SerializeField] private Canvas gunsmithCanvas;
    [SerializeField] private Canvas attachmentRowCanvas;
    [SerializeField] private WeaponDataHolder[] weaponDataHolder;
    
    private Dictionary<string, GunAttachmentData> _weaponDataDictionary;

    [Serializable]
    public struct  WeaponDataHolder
    {
        public string name;
        public GunAttachmentData gunAttachmentData;
    }

    private void Awake()
    {
        AddDataToDictionary();
    }

    private void AddDataToDictionary()
    {
        _weaponDataDictionary = new Dictionary<string, GunAttachmentData>();

        foreach (var weaponData in weaponDataHolder)
        {
            _weaponDataDictionary.Add(weaponData.name, weaponData.gunAttachmentData);
        }
    }

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
