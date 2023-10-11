using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingArea : MonoBehaviour
{
    private float points;
    public float timer;
    
    private void onTriggerEnter(Collider other){
    
    Destroy(other.gameObject);
    
    }
}
