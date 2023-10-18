using System.Collections;
using UnityEngine;

public class ActivateAreaShotting : MonoBehaviour {
	public GameObject area;


    void  Start(){

    } 
    void Update(){
        if(Input.GetKeyDown(KeyCode.G)){
            area.SetActive(true);
            gameObject.SetActive(false);
        }
    }
	

}
