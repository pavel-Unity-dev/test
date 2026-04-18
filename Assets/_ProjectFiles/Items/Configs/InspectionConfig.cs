using UnityEngine;

[CreateAssetMenu(fileName = "InspectionConfig", menuName = "Configs/Inspection Config")]
public class InspectionConfig : ScriptableObject
{
    public Vector3 inspectLocalPosition = new Vector3(-0.35f, -0.1f, 1.2f);
    public Vector3 inspectLocalEulerAngles = Vector3.zero;

    public Vector3 heldLocalPosition = new Vector3(0.45f, -0.45f, 0.9f);
    public Vector3 heldLocalEulerAngles = new Vector3(10f, -20f, 0f);

    public float inspectRotationSpeed = 100f;
}