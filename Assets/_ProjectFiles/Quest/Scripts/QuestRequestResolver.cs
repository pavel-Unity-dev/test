using System.Collections.Generic;
using UnityEngine;

public class QuestRequestResolver : MonoBehaviour
{
    [SerializeField] private WorldItem[] availableItems;

    public ItemData GetRandomQuestItem()
    {
        List<ItemData> validItems = new List<ItemData>();

        for (int i = 0; i < availableItems.Length; i++)
        {
            WorldItem item = availableItems[i];

            if (item == null || item.ItemData == null)
                continue;

            ItemData itemData = item.ItemData;

            if (!itemData.canBeQuestTarget)
                continue;

            if (itemData.isKey || itemData.itemKind == ItemKind.Key)
                continue;

            if (itemData.itemKind == ItemKind.Note)
                continue;

            validItems.Add(itemData);
        }

        if (validItems.Count == 0)
            return null;

        int randomIndex = Random.Range(0, validItems.Count);
        return validItems[randomIndex];
    }
}