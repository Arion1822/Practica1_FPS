using System.Collections;
using UnityEngine;

public class ActivateAreaShotting : MonoBehaviour {
	public GameObject area;
	void OnTriggerEnter(Collider other) {
        area.SetActive(true);
    }

}
