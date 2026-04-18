using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLookConfig", menuName = "Configs/Player Look Config")]
public class PlayerLookConfig : ScriptableObject
{
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float minPitch = -80f;
    public float maxPitch = 80f;
}