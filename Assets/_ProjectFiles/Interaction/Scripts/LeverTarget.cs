using UnityEngine;

public class LeverTarget : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    public void SetState(bool isOn)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(isOn);
        }
    }
}