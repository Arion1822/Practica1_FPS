using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float mouseXSensitivity = 120f;
    public float mouseYSensitivity = 120f;
    public float speed = 5f;

    Camera camera;
    Rigidbody rb;

    private float rotation;
    private bool isGrounded;

    private Gun gun;
    
    public int maxLife = 100;
    public int maxShield = 100;
    public int currentLife;
    public int currentShield;

    void Start(){
        camera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();


        currentLife = maxLife;
        currentShield = maxShield;
    }

    void Update(){
<<<<<<< HEAD
        Rotate();
        Move();
    }

    void Rotate(){
=======
        isGrounded= RayCastGrounded();
>>>>>>> a546c3d131e830adc2dc3133afa0dbc6314813e7
        float xr = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float yr = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;
        
        rotation -= yr;
        rotation = Mathf.Clamp(rotation, -90, 90);

        transform.Rotate(0, xr, 0);
        camera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);
<<<<<<< HEAD
=======

        if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			rb.AddRelativeForce (Vector3.up * jumpForce);

		}
        Move();
>>>>>>> a546c3d131e830adc2dc3133afa0dbc6314813e7
    }

    void Move(){
        float lateralMov = Input.GetAxis("Horizontal");
        float forwardMov = Input.GetAxis("Vertical");

        Vector3 inputMovement = new Vector3(lateralMov, 0, forwardMov);

        Vector3 worldMovement = transform.TransformDirection(inputMovement);

        Vector3 finalMovement = worldMovement * speed * Time.deltaTime;

        rb.MovePosition(rb.position + finalMovement);
    }

<<<<<<< HEAD
    public void RefillAmmo()
    {
        gun.Refill();
    }

    public bool HasFullAmmo()
    {
        return gun.HasFullAmmo();
    }

    public void ReceiveDamage(int damage)
    {

        if (currentShield > 0)
        {
            int shieldDamage = (int) (damage * 0.75f);
            int lifeDamage = (int) (damage * 0.25f);

            currentShield = Mathf.Max(currentShield - shieldDamage, 0);
            currentLife = Mathf.Max(currentLife - lifeDamage, 0);
        }
        else
        {
            currentLife = Mathf.Max(currentLife - damage, 0);
        }
    }

    public bool HasFullLife()
    {
        return currentLife == maxLife;
    }

    public void RefillLife()
    {
        currentLife = maxLife;
    }
    
    public bool HasFullShield()
    {
        return currentShield == maxShield;
    }

    public void RefillShield()
    {
        currentShield = maxShield;
    }
}
=======
/*
 * Rotating current weapon from here.
 * Checkig if we have a weapon, if we do, if its a gun it iwll fetch the gun and rotate it accordingly,
 * same goes for the sword.
 * Incase we dont have a weapon or gun or it didnt find it, it will write into the console that it cant find a weapon.
 */


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
>>>>>>> a546c3d131e830adc2dc3133afa0dbc6314813e7
