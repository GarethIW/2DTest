using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    public float walkSpeed = 0.000001f;
    public float runSpeed = 20;
    public float airControl = 0.5f;
    public float jumpForce = 200;
    public float wallJumpForce = 200;
    public float wallJumpForceUp = 200;
    public float SpeedLimit = 0.0001f;
    public bool isGrounded = false;

    public LayerMask rayMask;
    public float groundedCheckDist = 1;

    private float Speed = 10;

    void FixedUpdate() {
        //Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
 
        isGrounded = Physics.Raycast (transform.position, -transform.up, groundedCheckDist, rayMask);
        Speed = walkSpeed;//Input.GetButton("Run") ? runSpeed : walkSpeed;
       
                //Rotation Handling
        transform.rotation = Quaternion.Euler(Vector3.up * Camera.main.transform.localEulerAngles.y);
 
                //Movement and jumping
        rigidbody.velocity = transform.TransformDirection (new Vector3 (h, 0, v).normalized) * Speed + (Vector3.up * ((Input.GetButtonDown ("Jump") && isGrounded) ? jumpForce : rigidbody.velocity.y));
 
        if(isGrounded == false)
        {
            rigidbody.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal") * airControl, 0, Input.GetAxis("Vertical") * airControl));
        }
 
                //Limit speed to max
        rigidbody.velocity = Mathf.Clamp (rigidbody.velocity.magnitude, 0, SpeedLimit) * rigidbody.velocity.normalized;
    }
}
