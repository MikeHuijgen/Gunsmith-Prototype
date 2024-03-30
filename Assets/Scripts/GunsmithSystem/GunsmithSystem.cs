using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunsmithSystem : MonoBehaviour
{
    [SerializeField] private GameObject gunsmithCanvas;
    [SerializeField] private GameObject attachmentRowCanvas;
    [SerializeField] private WeaponDataHolder[] weaponDataHolder;
    
    private Dictionary<string, GunAttachmentData> _weaponDataDictionary;

    private GunAttachmentData _selectedGunAttachmentData;

    [Serializable]
    public struct  WeaponDataHolder
    {
        public string name;
        public GunAttachmentData gunAttachmentData;
    }

    private void Awake()
    {
        AddDataToDictionary();
        SelectCurrentAttachmentData();
    }

    private void AddDataToDictionary()
    {
        _weaponDataDictionary = new Dictionary<string, GunAttachmentData>();

        foreach (var weaponData in weaponDataHolder)
        {
            _weaponDataDictionary.Add(weaponData.name, weaponData.gunAttachmentData);
        }
    }

    private void SelectCurrentAttachmentData()
    {
        if (_weaponDataDictionary.Count <= 0) return;

        var nameChild = transform.GetChild(0).name;

        _weaponDataDictionary.TryGetValue(nameChild, out var foundData);
        _selectedGunAttachmentData = foundData;
    }

    public AttachmentCategory GetAttachmentsFromCategory(string category)
    {
        return _selectedGunAttachmentData.categories.FirstOrDefault(attachmentCategory => attachmentCategory.name.ToUpper() == category.ToUpper());
    }
}
