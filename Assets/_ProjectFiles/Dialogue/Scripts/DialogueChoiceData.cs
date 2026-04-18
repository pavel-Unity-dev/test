using System;

[Serializable]
public class DialogueChoiceData
{
    public string choiceText;
    public string nextNodeId;
    public bool endsDialogue;
}