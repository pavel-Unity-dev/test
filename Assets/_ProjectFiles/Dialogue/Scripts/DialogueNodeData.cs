using System;
using System.Collections.Generic;

[Serializable]
public class DialogueNodeData
{
    public string nodeId;
    public string npcText;
    public List<DialogueChoiceData> choices = new List<DialogueChoiceData>();
}