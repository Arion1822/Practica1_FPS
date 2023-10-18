using System.Collections;
using UnityEngine;

public class ActivateAreaShotting : MonoBehaviour {
	public GameObject area;

    private void OnTriggerStay(Collider other)
    {
     if(Input.GetKeyDown(KeyCode.G)){
            area.SetActive(true);
            gameObject.SetActive(false);
        }
    }
	

}
