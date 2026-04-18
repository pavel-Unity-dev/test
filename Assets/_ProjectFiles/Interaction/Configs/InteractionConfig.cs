using UnityEngine;

[CreateAssetMenu(fileName = "InteractionConfig", menuName = "Configs/Interaction Config")]
public class InteractionConfig : ScriptableObject
{
    public float interactionDistance = 3f;
    public LayerMask interactableMask;
}