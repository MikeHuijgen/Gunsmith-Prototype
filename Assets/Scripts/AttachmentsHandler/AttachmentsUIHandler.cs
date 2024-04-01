using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class AttachmentsUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject attachmentSlot;
    [SerializeField] private GameObject attachmentHolder;
    
    private string _selectedCategory;
    private AttachmentCategory _attachmentCategory;
    private List<GameObject> _attachmentSlotsPool;
    private int poolSize = 20;

    private void Awake()
    {
        FillPool();
    }

    public void SetSelectedAttachmentCategory(string category)
    {
        _selectedCategory = category;
        _attachmentCategory = GunsmithSystem.Instance.GetAttachmentsFromCategory(_selectedCategory);
        SetUpAttachmentSlots();
    }

    private void FillPool()
    {
        _attachmentSlotsPool = new List<GameObject>();
        for (var i = 0; i < poolSize; i++)
        {
            var newAttachmentSlot = Instantiate(attachmentSlot, attachmentHolder.transform);
            newAttachmentSlot.SetActive(false);
            _attachmentSlotsPool.Add(newAttachmentSlot);
        }
    }

    public void ResetPool()
    {
        foreach (var attachmentSLot in _attachmentSlotsPool)
        {
            attachmentSLot.SetActive(false);
        }
    }

    private void SetUpAttachmentSlots()
    {
        var attachmentSlotAmount = _attachmentCategory.attachments.Length;

        for (var i = 0; i < attachmentSlotAmount; i++)
        {
            if (_attachmentSlotsPool[i].activeInHierarchy) continue;
            _attachmentSlotsPool[i].SetActive(true);
        }
    }
    
}
