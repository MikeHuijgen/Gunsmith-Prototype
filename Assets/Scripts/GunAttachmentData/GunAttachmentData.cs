using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun attachment data")]
public class GunAttachmentData : ScriptableObject
{
    public AttachmentData[] sights;
    public AttachmentData[] barrels;
    public AttachmentData[] muzzles;
    public AttachmentData[] stocks;
    public AttachmentData[] magazines;
    public AttachmentData[] underBarrels;
    public AttachmentData[] rearGrips;
}
