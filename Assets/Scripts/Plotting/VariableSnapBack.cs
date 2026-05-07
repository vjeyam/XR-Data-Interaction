using UnityEngine;

public class VariableSnapBack : MonoBehaviour
{
    [Header("Snap Settings")]
    public float snapRadius = 0.35f;
    public float snapSpeed = 8f;
    public bool snapRotation = true;

    [Header("State")]
    public bool isAwayFromHome = false;

    private Vector3 homePosition;
    private Quaternion homeRotation;

    private void Start()
    {
        homePosition = transform.position;
        homeRotation = transform.rotation;
    }

    private void Update()
    {
        float distanceFromHome = Vector3.Distance(transform.position, homePosition);
        isAwayFromHome = distanceFromHome > snapRadius;

        if (!isAwayFromHome)
        {
            SnapHome();
        }
    }

    private void SnapHome()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            homePosition,
            snapSpeed * Time.deltaTime
        );

        if (snapRotation)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                homeRotation,
                snapSpeed * Time.deltaTime
            );
        }
    }

    public bool IsBeingUsed()
    {
        return isAwayFromHome;
    }

    public void ForceSnapHome()
    {
        transform.position = homePosition;
        transform.rotation = homeRotation;
        isAwayFromHome = false;
    }
}