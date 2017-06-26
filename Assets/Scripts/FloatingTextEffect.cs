using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().sortingLayerName = "Behind Enemy";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
