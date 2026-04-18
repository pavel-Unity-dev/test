using System;
using UnityEngine;

public class FetchQuestController : MonoBehaviour
{
    public ItemData RequestedItem { get; private set; }
    public bool IsQuestActive { get; private set; }
    public bool IsQuestCompleted { get; private set; }

    public event Action<ItemData> OnQuestStarted;
    public event Action<ItemData> OnQuestCompleted;
    public event Action OnQuestCleared;

    public void StartQuest(ItemData requestedItem)
    {
        if (requestedItem == null)
            return;

        RequestedItem = requestedItem;
        IsQuestActive = true;
        IsQuestCompleted = false;

        OnQuestStarted?.Invoke(RequestedItem);
    }

    public bool IsRequestedItem(WorldItem item)
    {
        if (!IsQuestActive || IsQuestCompleted)
            return false;

        if (RequestedItem == null || item == null || item.ItemData == null)
            return false;

        return item.ItemData == RequestedItem;
    }

    public void CompleteQuest()
    {
        if (!IsQuestActive || RequestedItem == null)
            return;

        IsQuestCompleted = true;
        OnQuestCompleted?.Invoke(RequestedItem);
    }

    public void ClearQuest()
    {
        RequestedItem = null;
        IsQuestActive = false;
        IsQuestCompleted = false;

        OnQuestCleared?.Invoke();
    }
}