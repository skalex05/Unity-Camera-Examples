using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFollow : MonoBehaviour
{
    public float viewLimit;
    public Transform target;
    public Vector3 TargetPositionOffset;
    Quaternion desiredRotation;
    public float Sensitivity;
    [Range(0, 1)] public float rotationSmoothing;
    [Range(0, 20)] public float maxDistanceToTarget;

    Vector3 targetPositionOffset;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector3 ROT = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        ROT = ROT + new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Sensitivity;
        ROT = new Vector3(Mathf.Clamp(ROT.x, -viewLimit, viewLimit), ROT.y, ROT.z);
        desiredRotation = Quaternion.Euler(ROT);

        Vector3 newrot = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSmoothing).eulerAngles;
        transform.rotation = Quaternion.Euler(newrot);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        float distance = maxDistanceToTarget;

        if (Physics.Raycast(target.position + TargetPositionOffset.x * transform.right + transform.forward * TargetPositionOffset.z + new Vector3(0, TargetPositionOffset.y, 0), -transform.forward, out RaycastHit hit, maxDistanceToTarget))
        {
            distance = hit.distance - 0.05f;
        }
        if (Physics.OverlapSphere(target.position + TargetPositionOffset.x * transform.right + transform.forward * TargetPositionOffset.z + new Vector3(0, TargetPositionOffset.y, 0), 0.001f).Length >= 1)
        {
            foreach (Collider c in Physics.OverlapSphere(target.position + TargetPositionOffset.x * transform.right + transform.forward * TargetPositionOffset.z + new Vector3(0, TargetPositionOffset.y, 0), 0.001f))
            {
                if (c.gameObject == target)
                {
                    break;
                }
            }
            targetPositionOffset = Vector3.Lerp(targetPositionOffset, Vector3.zero, rotationSmoothing);
        }
        else
        {
            targetPositionOffset = Vector3.Lerp(targetPositionOffset, TargetPositionOffset, rotationSmoothing);
        }
        transform.position = target.position + targetPositionOffset.x * transform.right + transform.forward * targetPositionOffset.z + new Vector3(0, targetPositionOffset.y, 0) + -transform.forward * distance;
    }
}
