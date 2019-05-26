using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float viewLimit;

    public float movementspeed;
    public float NormalSpeed;
    public float FastSpeed;
    public float SlowSpeed;

    public float sensitivity;
    public float additionalMovementRotation;
    [Range(0,1)]public float positionalSmoothing;
    [Range(0, 1)] public float rotationalSmoothing;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        desiredPosition = transform.position;
    }
    Vector3 desiredRotation;
    Vector3 desiredPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fast"))
        {
            movementspeed = FastSpeed;
        }
        else if (Input.GetButton("Slow"))
        {
            movementspeed = SlowSpeed;
        }
        else
        {
            movementspeed = NormalSpeed;
        }


        Vector3 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized;

        desiredPosition += direction.x * transform.right * movementspeed *  Time.deltaTime;
        desiredPosition += direction.y * transform.forward * movementspeed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionalSmoothing);

        desiredRotation += new Vector3(-look.y,look.x,0);
        desiredRotation = new Vector3(Mathf.Clamp(desiredRotation.x, -viewLimit, viewLimit), desiredRotation.y, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(desiredRotation + new Vector3(additionalMovementRotation * direction.y, 0,additionalMovementRotation * -direction.x)), rotationalSmoothing);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z);
        if (transform.eulerAngles.z > additionalMovementRotation && transform.eulerAngles.z <= 180)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, additionalMovementRotation);
        }
        else if (transform.eulerAngles.z < 360 - additionalMovementRotation && transform.eulerAngles.z >= 180)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -additionalMovementRotation);
        }
    }
}
