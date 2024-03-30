using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentsUIHandler : MonoBehaviour
{
    [SerializeField] private GunsmithSystem gunsmithSystem;
    private string _selectedCategory;
    private AttachmentCategory _attachmentCategory;

    public void SetSelectedAttachmentCategory(string category)
    {
        _selectedCategory = category;
        _attachmentCategory = gunsmithSystem.GetAttachmentsFromCategory(_selectedCategory);
        print(_attachmentCategory.name);
    }
    
}
