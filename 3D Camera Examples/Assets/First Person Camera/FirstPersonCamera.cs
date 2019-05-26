using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float MaxAngle;
    public Transform target;
    public float sensitivity;
    public Vector3 desiredRotation;
    [Range(0,1)]public float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        desiredRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        desiredRotation += new Vector3(-Input.GetAxisRaw("Mouse Y"),Input.GetAxisRaw("Mouse X"),0) * sensitivity;
        desiredRotation = new Vector3(Mathf.Clamp(desiredRotation.x, -MaxAngle, MaxAngle), desiredRotation.y, desiredRotation.z);
        Quaternion rot = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(desiredRotation), smoothing);
        transform.localEulerAngles = new Vector3(rot.eulerAngles.x,0, 0);
        target.eulerAngles = new Vector3(0,rot.eulerAngles.y,0);
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x,0,0);
    }
}
