using UnityEngine;

[CreateAssetMenu(fileName = "ValveConfig", menuName = "Configs/Valve Config")]
public class ValveConfig : ScriptableObject
{
    public float maxProgress = 1f;
    public float holdFillSpeed = 0.5f;
    public float returnSpeed = 0.75f;

    public float maxValveAngle = 360f;
    public float maxDoorHeight = 3f;
}