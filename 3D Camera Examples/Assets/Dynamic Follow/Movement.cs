using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(0, 1)] public float rotationSpeed;
    public DynamicFollow follow;
    public float MovementSpeed;
    public Rigidbody RB;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = follow.target;
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
        target.rotation = Quaternion.Slerp(target.rotation,Quaternion.Euler(0,follow.ROT.y,0), rotationSpeed);
        if (!(direction.x == 0 && direction.y == 0))
        {
            RB.velocity = new Vector3(0, RB.velocity.y, 0);
            RB.velocity += new Vector3(target.forward.x, 0, transform.forward.z) * MovementSpeed * direction.y;
            RB.velocity += new Vector3(target.right.x, 0, transform.right.z) * MovementSpeed * direction.x;
        }
    }
}
