using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionControl : MonoBehaviour {

	public Material material;
	// Use this for initialization
	void Awake () {
		disableEmission ();
	}

	public void enableEmission()
	{
		material.EnableKeyword ("_EMISSION");
	}

	public void disableEmission()
	{
		material.DisableKeyword ("_EMISSION");
	}
	// Update is called once per frame
	void Update () {

	}
}
