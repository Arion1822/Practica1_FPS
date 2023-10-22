using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePart : MonoBehaviour
{
    public int damage;
    
    public void DamageDrone()
    {
        GetComponentInParent<Drone>().ReceiveDamage(damage);
    }
}
