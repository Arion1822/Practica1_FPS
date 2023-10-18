using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Character characterScript = other.GetComponent<Character>();
            if (characterScript != null)
            {
                CollectItem(characterScript);
            }
        }
    }
    
    public virtual void CollectItem(Character character)
    {
        Destroy(gameObject);
    }
}
