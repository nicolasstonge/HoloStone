using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAction : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}