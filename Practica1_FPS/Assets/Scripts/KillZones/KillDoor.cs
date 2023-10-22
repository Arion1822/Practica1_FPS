using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDoor : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {

        Character character = other.collider.GetComponent<Character>();
        if (character != null)
        {
            Debug.Log("Character");
            character.Die();
        }
        else Debug.Log("No character"); 
    }
}
