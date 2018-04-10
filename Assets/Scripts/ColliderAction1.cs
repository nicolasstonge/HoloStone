using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ColliderAction1 : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");

        Transform t = this.transform;
        GameObject card = new GameObject();
        Transform place = other.transform.parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == "Playable")
            {
                card = t.GetChild(i).gameObject;
            }

        }
        bool test = card.GetComponent<NavMeshAgent>().Warp(place.position);
        Debug.Log("Wrap is" + test);
        card.transform.position = place.position;
        card.transform.rotation = place.rotation;
        card.transform.parent = place;
        card.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
    }
}