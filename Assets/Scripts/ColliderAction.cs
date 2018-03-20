using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAction : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");

        GameObject card = this.transform.GetChild(0).gameObject;
        card.transform.position = other.transform.position;
        card.transform.rotation = other.transform.rotation;
        card.transform.parent = other.transform.parent.transform;
        card.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
    }
}