using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour {

    GameObject parent;

	// Use this for initialization
	void Start () {

        parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == parent.GetComponent<MonsterAnim>().targetmonster)
        {

        }
    }
}
