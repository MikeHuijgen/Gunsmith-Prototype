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
    private int _poolSize = 20;
    private GameObject _selectedAttachment;

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
        for (var i = 0; i < _poolSize; i++)
        {
            var newAttachmentSlot = Instantiate(attachmentSlot, attachmentHolder.transform);
            newAttachmentSlot.SetActive(false);
            _attachmentSlotsPool.Add(newAttachmentSlot);
            newAttachmentSlot.GetComponent<AttachmentButtonID>().ClickedOnAttachment += newFun;
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
            if (_attachmentSlotsPool[i].activeInHierarchy || _attachmentCategory.attachments[i] == null) continue;
            _attachmentSlotsPool[i].GetComponentInChildren<TMP_Text>().text = _attachmentCategory.attachments[i].name;
            _attachmentSlotsPool[i].SetActive(true);
        }
    }

    private void newFun(string attachmentName)
    {
        foreach (var attachment in _attachmentCategory.attachments)
        {
            if (attachment.name != attachmentName) continue;
            _selectedAttachment = attachment.attachmentMesh;
        }
        GunsmithSystem.Instance.AttachAttachment(_selectedAttachment, _selectedCategory);
    }
}
