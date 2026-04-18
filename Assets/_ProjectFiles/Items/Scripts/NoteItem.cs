using UnityEngine;

public class NoteItem : MonoBehaviour
{
    [SerializeField] private WorldItem worldItem;

    [Header("Visual root")]
    [SerializeField] private Transform bookVisualRoot;

    [Header("Page pivots")]
    [SerializeField] private Transform leftPagePivot;
    [SerializeField] private Transform rightPagePivot;

    [Header("Animation")]
    [SerializeField] private float animationSpeed = 220f;

    [Header("Closed state")]
    [SerializeField] private Vector3 bookClosedEuler = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 leftClosedEuler = new Vector3(0f, -8f, 0f);
    [SerializeField] private Vector3 rightClosedEuler = new Vector3(0f, 8f, 0f);

    [Header("Opened state")]
    [SerializeField] private Vector3 bookOpenedEuler = new Vector3(15f, 0f, 0f);
    [SerializeField] private Vector3 leftOpenedEuler = new Vector3(0f, -165f, 0f);
    [SerializeField] private Vector3 rightOpenedEuler = new Vector3(0f, 165f, 0f);

    private Quaternion bookClosedRotation;
    private Quaternion bookOpenedRotation;

    private Quaternion leftClosedRotation;
    private Quaternion leftOpenedRotation;

    private Quaternion rightClosedRotation;
    private Quaternion rightOpenedRotation;

    private void Awake()
    {
        bookClosedRotation = Quaternion.Euler(bookClosedEuler);
        bookOpenedRotation = Quaternion.Euler(bookOpenedEuler);

        leftClosedRotation = Quaternion.Euler(leftClosedEuler);
        leftOpenedRotation = Quaternion.Euler(leftOpenedEuler);

        rightClosedRotation = Quaternion.Euler(rightClosedEuler);
        rightOpenedRotation = Quaternion.Euler(rightOpenedEuler);

        ApplyInstantClosedState();
    }

    private void Update()
    {
        if (worldItem == null || leftPagePivot == null || rightPagePivot == null)
            return;

        bool shouldBeOpen = worldItem.CurrentState == ItemState.Inspecting;

        Quaternion targetBookRotation = shouldBeOpen ? bookOpenedRotation : bookClosedRotation;
        Quaternion targetLeftRotation = shouldBeOpen ? leftOpenedRotation : leftClosedRotation;
        Quaternion targetRightRotation = shouldBeOpen ? rightOpenedRotation : rightClosedRotation;

        if (bookVisualRoot != null)
        {
            bookVisualRoot.localRotation = Quaternion.RotateTowards(
                bookVisualRoot.localRotation,
                targetBookRotation,
                animationSpeed * Time.deltaTime
            );
        }

        leftPagePivot.localRotation = Quaternion.RotateTowards(
            leftPagePivot.localRotation,
            targetLeftRotation,
            animationSpeed * Time.deltaTime
        );

        rightPagePivot.localRotation = Quaternion.RotateTowards(
            rightPagePivot.localRotation,
            targetRightRotation,
            animationSpeed * Time.deltaTime
        );
    }

    private void ApplyInstantClosedState()
    {
        if (bookVisualRoot != null)
            bookVisualRoot.localRotation = bookClosedRotation;

        if (leftPagePivot != null)
            leftPagePivot.localRotation = leftClosedRotation;

        if (rightPagePivot != null)
            rightPagePivot.localRotation = rightClosedRotation;
    }
}