using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string dialogueId;
    public string startNodeId = "start";
    public bool startsFetchQuestOnEnd;
    public List<DialogueNodeData> nodes = new List<DialogueNodeData>();

    public DialogueNodeData GetNodeById(string nodeId)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].nodeId == nodeId)
                return nodes[i];
        }

        return null;
    }
}