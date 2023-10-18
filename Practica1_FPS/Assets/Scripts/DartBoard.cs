using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoard : MonoBehaviour
{
    public int scoreOnDestroy;


    public void ReceiveDamage()
    {
        GameManager.Instance.AddScore(scoreOnDestroy);
        //Destroy(gameObject.transform.parent.gameObject);
    }
}
