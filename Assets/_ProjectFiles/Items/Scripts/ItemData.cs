using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string displayName;
    [TextArea(3, 6)] public string description;
    public ItemKind itemKind = ItemKind.Generic;
    public bool canBeQuestTarget = true;
    public bool isKey = false;
}