using UnityEngine;

public class HandGestureManager : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    public GameObject menu;
    public Transform head;

    private bool menuShown = false;

    void Start()
    {
        menu.SetActive(true);
        menu.transform.position = head.position + head.forward * 1.0f;
        menu.transform.localScale = Vector3.one * 0.002f;
    }

    void Update()
    {
        // Debug.Log("SCRIPT RUNNING");
        if (leftHand == null || rightHand == null) return;

        float leftY = leftHand.position.y;
        float rightY = rightHand.position.y;

        Debug.Log($"L:{leftY} R:{rightY}");

        if (leftY > 0.8f && rightY > 0.8f)
        {
            if (menuShown == false)
            {
                ShowMenu();
                menuShown = true;
            }
        }
        else
        {
            menuShown = false;
        }
    }

    void ShowMenu()
    {
        menu.SetActive(true);

        Vector3 forward = head.forward;

        // position in front of face
        menu.transform.position = head.position + forward * 1.0f;

        // 👇 THIS is the key fix
        menu.transform.rotation = Quaternion.LookRotation(forward);

        // flip so UI faces user instead of away
        menu.transform.Rotate(0, 0, 0);
    }
}