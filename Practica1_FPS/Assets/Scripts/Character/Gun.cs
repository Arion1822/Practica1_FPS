using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public int maxMagazineAmmo = 10;
    public int maxTotalAmmo = 50;
<<<<<<< Updated upstream:Practica1_FPS/Assets/Scripts/Character/Gun.cs
    public int currentTotalAmmo; // Total ammo the player has
    public int currentMagazineAmmo; // Current ammo in the magazine
    public GameObject decalPrefab;

=======
    public int currentTotalAmmo;
    public int currentMagazineAmmo;
    public float maxDistance = 100f;
>>>>>>> Stashed changes:Practica1_FPS/Assets/Scripts/Gun.cs

    public TMP_Text currentAmmoText;
    void Start()
    {
        currentMagazineAmmo = maxMagazineAmmo;
        currentTotalAmmo = maxTotalAmmo;

        GameObject[] dd;
        dd.Length;
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
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            DartBoardPart dartBoardPart = hit.collider.GetComponent<DartBoardPart>();
            if (dartBoardPart != null)
            {
                // Destroy the DartBoard
                dartBoardPart.ReceiveDamage();
            }

            DronePart dronePart = hit.collider.GetComponent<DronePart>();
            if (dronePart != null)
            {
                dronePart.DamageDrone();
                
                Li
            }
<<<<<<< Updated upstream:Practica1_FPS/Assets/Scripts/Character/Gun.cs

            else{
                CreateDecal(hit.point, hit.normal);
            }
=======
            
            //create a decal in hit 
>>>>>>> Stashed changes:Practica1_FPS/Assets/Scripts/Gun.cs
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
        GameManager.Instance.UpdateAmmoText(currentMagazineAmmo, currentTotalAmmo);
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

    void CreateDecal(Vector3 position, Vector3 normal)
{

    GameObject[] decals = GameObject.FindGameObjectsWithTag("Decal");

    if(decals.Length <25){
    // Instantiate the decal prefab at the hit point
    GameObject decal = Instantiate(decalPrefab, position, Quaternion.identity);

    // Rotate the decal to match the hit normal
    
    decal.transform.forward = normal;

    decal.transform.Rotate(Vector3.up, 180f);
     // Destroy the decal after 10 seconds
    Destroy(decal, 5f);
    }

   
}
}
