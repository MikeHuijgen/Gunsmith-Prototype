using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun attachment data")]
public class GunAttachmentData : ScriptableObject
{
    [Header("Attachment categories")]
    public AttachmentCategory[] categories;
}
