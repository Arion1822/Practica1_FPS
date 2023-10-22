using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        GameManager.Instance.GoToFollowingScene();
    }
            
}    
