using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoardPart : MonoBehaviour
{
    public int scoreOnDestroy;


    public void ReceiveDamage()
    {
        GameManager.Instance.AddScore(scoreOnDestroy);
    }

    private void OnTriggerStay(Collider other)
    {
        throw new NotImplementedException();
    }
}
