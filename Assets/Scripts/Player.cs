using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public float SpeedLimit = 1f;
    public bool isGrounded = false;

    public LayerMask rayMask;
    public float groundedCheckDist = 1;

    public float Speed = 10;

    public Transform Sprite;

    public Collider CurrentTrigger;


    private float turntarget = 12f;

    private float hintAlpha = 0f;

    

    void FixedUpdate() {
        //Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h > 0f) turntarget = -12f;
        if (h < 0f) turntarget = 12f;

        Sprite.localScale = Vector3.Lerp(Sprite.transform.localScale, new Vector3(turntarget, 12f, 1f), 0.25f);

        //isGrounded = Physics.Raycast (transform.position, -transform.up, groundedCheckDist, rayMask);
        Speed = walkSpeed;//Input.GetButton("Run") ? runSpeed : walkSpeed;
       
                //Rotation Handling
        //transform.rotation = Quaternion.Euler(Vector3.up * Camera.main.transform.localEulerAngles.y);
 
                //Movement and jumping
        rigidbody.velocity = transform.TransformDirection(new Vector3(h, 0, v).normalized)*Speed;// + (Vector3.up * ((Input.GetButtonDown ("Jump") && isGrounded) ? jumpForce : rigidbody.velocity.y));
 
        //if(isGrounded == false)
        //{
        //    rigidbody.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal") * airControl, 0, Input.GetAxis("Vertical") * airControl));
        //}
 
                //Limit speed to max
       // rigidbody.velocity = Mathf.Clamp (rigidbody.velocity.magnitude, 0, SpeedLimit) * rigidbody.velocity.normalized;

        if (hintAlpha > 0f) hintAlpha -= 0.1f;
        hintAlpha = Mathf.Clamp(hintAlpha, 0f, 1f);
        transform.FindChild("Hint UI/Hint").GetComponent<Text>().color = Color.white*hintAlpha;

        if (Input.GetButtonDown("Use"))
        {
            Use();
        }
    }

    public void Use()
    {
        if (CurrentTrigger == null) return;

        switch (CurrentTrigger.name)
        {
            case "LadderTop":
                //transform.position = CurrentTrigger.transform.position;
                transform.position = GameObject.Find("LadderBottom").transform.position;
                break;
            case "LadderBottom":
                //transform.position = CurrentTrigger.transform.position;
                transform.position = GameObject.Find("LadderTop").transform.position;
                break;
            case "Stairs1Top":
                //transform.position = CurrentTrigger.transform.position;
                transform.position = GameObject.Find("Stairs1Bottom").transform.position;
                break;
            case "Stairs1Bottom":
                //transform.position = CurrentTrigger.transform.position;
                transform.position = GameObject.Find("Stairs1Top").transform.position;
                break;
        }
    }

    public void OnTriggerStay(Collider trigger)
    {
        switch(trigger.name)
        {
            case "LadderTop":
            case "LadderBottom":
                HintText("Use Ladder");
                break;
            case "Stairs1Top":
            case "Stairs1Bottom":
                HintText("Use Stairs");
                break;
        }

        CurrentTrigger = trigger;
    }

    public void OnTriggerExit(Collider trigger)
    {
        CurrentTrigger = null;
    }

    public void HintText(string text)
    {
        transform.FindChild("Hint UI/Hint").GetComponent<Text>().text = text;
        hintAlpha +=0.2f;
    }
}
