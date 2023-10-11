using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float mouseXSensitivity = 120f;
    public float mouseYSensitivity = 120f;
    public float velocity = 0.01f;
    Camera camera;
    Rigidbody rb;

    private float rotation;
    private bool isGrounded;

    void Start(){
        camera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        float xr = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float yr = Input.GetAxis("Mouse Y") * mouseXSensitivity * Time.deltaTime;
        
        rotation -= yr;
        rotation = Mathf.Clamp(rotation, -90, 90);

        transform.Rotate(0, xr, 0);
        camera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);

        Move();
        isGrounded= RayCastGrounded();
        Jumping();

    }

    void Move(){
        float lateralMov = Input.GetAxis("Horizontal");
        float forwardMov = Input.GetAxis("Vertical");

        // Create a vector for movement based on input
        Vector3 inputMovement = new Vector3(lateralMov, 0, forwardMov);

        // Transform the input movement vector from local space to world space
        Vector3 worldMovement = transform.TransformDirection(inputMovement);

        // Set a movement speed factor to control the speed of movement
        float speed = 5f; // Adjust this value to control the movement speed

        // Apply speed to the movement vector
        Vector3 finalMovement = worldMovement * speed * Time.deltaTime;

        // Move the rigidbody using rb.MovePosition
        rb.MovePosition(rb.position + finalMovement);
    }


    private Vector2 velocityGunFollow;
    private float gunWeightX,gunWeightY;
    [Tooltip("Current weapon that player carries.")]

    private GameObject weapon;
    private GunScript gun;
/*
 * Rotating current weapon from here.
 * Checkig if we have a weapon, if we do, if its a gun it iwll fetch the gun and rotate it accordingly,
 * same goes for the sword.
 * Incase we dont have a weapon or gun or it didnt find it, it will write into the console that it cant find a weapon.
 */
void WeaponRotation(){
	if(!weapon){
		weapon = GameObject.FindGameObjectWithTag("Weapon");
		if(weapon){
			if(weapon.GetComponent<GunScript>()){
				try{
					gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<GunScript>();
				}catch(System.Exception ex){
					print("gun not found->"+ex.StackTrace.ToString());
				}
			}
		}
	}

}

public float jumpForce;
void Jumping(){
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			rb.AddRelativeForce (Vector3.up * jumpForce);

		}
	}



    private bool RayCastGrounded(){
		RaycastHit groundedInfo;
		if(Physics.Raycast(transform.position, transform.up *-1f, out groundedInfo, 1)){
			Debug.DrawRay (transform.position, transform.up * -1f, Color.red, 0.0f);
			if(groundedInfo.transform != null){
				
				return true;
			}
			else{
				
				return false;
			}
		}

		return false;
	}

}