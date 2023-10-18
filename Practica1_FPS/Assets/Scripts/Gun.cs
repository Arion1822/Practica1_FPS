using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    public int maxMagazineAmmo = 10; // Maximum ammo in a magazine
    public int maxTotalAmmo = 50;
    public int currentTotalAmmo; // Total ammo the player has
    public int currentMagazineAmmo; // Current ammo in the magazine

    public TMP_Text currentAmmoText;
    void Start()
    {
        currentMagazineAmmo = maxMagazineAmmo;
        currentTotalAmmo = maxTotalAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentMagazineAmmo > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentMagazineAmmo < maxMagazineAmmo && currentTotalAmmo > 0)
        {
            Reload();
        }
        
        DisplayHUD();
    }

    void Shoot()
    {
        // Define the maximum shooting distance
        float maxDistance = 100f;

        // Create a ray from the camera through the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Declare a RaycastHit variable to store information about the hit object
        RaycastHit hit;

        // Perform the raycast and check if it hits an object within the maxDistance
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            /*// Check if the hit object has a Drone component
            Drone drone = hit.collider.GetComponent<Drone>();
            if (drone != null)
            {
                // Apply damage to the drone
                drone.ReceiveDamage(damageAmount);
            }*/

            // Check if the hit object has a DartBoard component
            DartBoard dartBoard = hit.collider.GetComponent<DartBoard>();
            if (dartBoard != null)
            {
                // Destroy the DartBoard
                dartBoard.ReceiveDamage();
            }
        }
        
        currentMagazineAmmo--;
    }

    void Reload()
    {
        int bulletsToReload = maxMagazineAmmo - currentMagazineAmmo;
        int bulletsAvailable = Mathf.Min(bulletsToReload, currentTotalAmmo);
        
        currentTotalAmmo -= bulletsAvailable;
        currentMagazineAmmo += bulletsAvailable;
    }

    void DisplayHUD()
    {
        currentAmmoText.SetText(currentMagazineAmmo+"/"+currentTotalAmmo);
    }

    public void Refill()
    {
        currentMagazineAmmo = maxMagazineAmmo;
        currentTotalAmmo = maxTotalAmmo;
    }

    public bool HasFullAmmo()
    {
        return currentMagazineAmmo == maxMagazineAmmo && currentTotalAmmo == maxTotalAmmo;
    }
}
