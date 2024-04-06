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
    [SerializeField] private GameObject rearGripAttachPoint;
    [SerializeField] private GameObject sightAttachPoint;
    [SerializeField] private GameObject barrelAttachPoint;
    [SerializeField] private GameObject magazineAttachPoint;
    [SerializeField]private GameObject _muzzleAttachPoint;
    
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
                DeleteOldAttachment(stockAttachPoint.transform);
                InstantiateNewAttachment(attachment, stockAttachPoint.transform);
                break;
            case "REARGRIP":
                DeleteOldAttachment(rearGripAttachPoint.transform);
                InstantiateNewAttachment(attachment, rearGripAttachPoint.transform);
                break;
            case "BARREL":
                var currentMuzzle = GetCurrentMuzzle();
                DeleteOldAttachment(barrelAttachPoint.transform);
                var newBarrel = InstantiateNewAttachment(attachment, barrelAttachPoint.transform);
                UpdateMuzzleAttachPoint(newBarrel, currentMuzzle);
                break;
            case "SIGHT":
                DeleteOldAttachment(sightAttachPoint.transform);
                InstantiateNewAttachment(attachment, sightAttachPoint.transform);
                break;
            case "MAGAZINE":
                DeleteOldAttachment(magazineAttachPoint.transform);
                InstantiateNewAttachment(attachment, magazineAttachPoint.transform);
                break;
            case "MUZZLE":
                DeleteOldAttachment(_muzzleAttachPoint.transform);
                InstantiateNewAttachment(attachment, _muzzleAttachPoint.transform);
                break;
        }
    }

    private static void DeleteOldAttachment(Transform targetTransform)
    {
        if (targetTransform.childCount <= 0) return;
        var oldAttachment = targetTransform.GetChild(0).gameObject;
        Destroy(oldAttachment);
    }

    private static GameObject InstantiateNewAttachment(GameObject newAttachment, Transform targetTransform)
    {
        return Instantiate(newAttachment, targetTransform);
    }

    private void UpdateMuzzleAttachPoint(GameObject newBarrel, GameObject muzzle)
    {
        _muzzleAttachPoint = newBarrel.transform.GetChild(0).gameObject;
        if (muzzle == null) return;
        InstantiateNewAttachment(muzzle, _muzzleAttachPoint.transform);
    }

    private GameObject GetCurrentMuzzle()
    {
        if (_muzzleAttachPoint == null || _muzzleAttachPoint.transform.childCount <= 0) return null;
        return _muzzleAttachPoint.transform.GetChild(0).gameObject;
    }
}
