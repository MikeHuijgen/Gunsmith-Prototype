using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GunsmithSystem : MonoBehaviour
{
    [SerializeField] private GameObject gunsmithCanvas;
    [SerializeField] private GameObject attachmentRowCanvas;
    [SerializeField] private WeaponDataHolder[] weaponDataHolder;

    [Header("attach points")] 
    [SerializeField] private GameObject stockAttachPoint;
    
    private Dictionary<string, GunAttachmentData> _weaponDataDictionary;

    private GunAttachmentData _selectedGunAttachmentData;

    private static GunsmithSystem _instance;

    public static GunsmithSystem Instance => _instance;


    [Serializable]
    public struct  WeaponDataHolder
    {
        public string name;
        public GunAttachmentData gunAttachmentData;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
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

    public void AttachAttachment(GameObject attachment, string category)
    {
        category = category.ToUpper();
        switch (category)
        {
            case "STOCK":
                if (stockAttachPoint.transform.childCount > 0)
                {
                    var oldAttachment = stockAttachPoint.transform.GetChild(0).gameObject;
                    Destroy(oldAttachment);
                }
                Instantiate(attachment, stockAttachPoint.transform);
                break;
        }
    }
}
