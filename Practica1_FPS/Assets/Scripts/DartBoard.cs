using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoard : MonoBehaviour
{
    private int scoreOnDestroy = 10;


    public void ReceiveDamage()
    {
        GameManager.Instance.AddScore(scoreOnDestroy);
        Destroy(gameObject);
    }
}
