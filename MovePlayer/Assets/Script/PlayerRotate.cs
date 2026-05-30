using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Transform cameraTransform;

    [Header("Settings")]
    [SerializeField] float rotationSpeed = 10f;

    void Update()
    {
        RotateTowardCameraForward();
    }

    void RotateTowardCameraForward()
    {
        Vector3 dir = cameraTransform.forward;
        dir.y = 0;

        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }
}