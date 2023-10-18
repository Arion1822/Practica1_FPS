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
