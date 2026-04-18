using UnityEngine;

public class QuestUIBinder : MonoBehaviour
{
    [SerializeField] private FetchQuestController fetchQuestController;
    [SerializeField] private QuestTrackerUI questTrackerUI;

    private void OnEnable()
    {
        if (fetchQuestController != null)
        {
            fetchQuestController.OnQuestStarted += HandleQuestStarted;
            fetchQuestController.OnQuestCompleted += HandleQuestCompleted;
            fetchQuestController.OnQuestCleared += HandleQuestCleared;
        }
    }

    private void OnDisable()
    {
        if (fetchQuestController != null)
        {
            fetchQuestController.OnQuestStarted -= HandleQuestStarted;
            fetchQuestController.OnQuestCompleted -= HandleQuestCompleted;
            fetchQuestController.OnQuestCleared -= HandleQuestCleared;
        }
    }

    private void HandleQuestStarted(ItemData itemData)
    {
        if (questTrackerUI != null)
            questTrackerUI.ShowQuest(itemData);
    }

    private void HandleQuestCompleted(ItemData itemData)
    {
        if (questTrackerUI != null)
            questTrackerUI.MarkCompleted(itemData);
    }

    private void HandleQuestCleared()
    {
        if (questTrackerUI != null)
            questTrackerUI.Hide();
    }
}