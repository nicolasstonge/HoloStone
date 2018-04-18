using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using HoloToolkit.Unity.InputModule;
using UnityEngine.AI;
using System;

public class DetectorAction : MonoBehaviour, ITrackableEventHandler, IInputClickHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    // Ref on the CardManager
    private CardManager mCardManager;
    // Ref on the CardAsset 
    private CardAsset mCardAsset;
    public int iPlayerOwned = -1;
    public bool bCardPlayed;
    // Function triggered when the object tracking state change (AKA detected or not detected)
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {

        }
        else
        {

        }
    }

    public void OnCardRemove()
    {
        // mCardManager is not available at Start(), if null, set it up
        if (!mCardManager)
        {
            mCardManager = this.transform.parent.GetComponent<CardManager>();
        }
        // Notify the CardManager
        mCardManager.RemoveCardFromDeck(mCardAsset);
    }

    void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");

        GameObject duplicateGameObject = (this.gameObject);
        Transform t = duplicateGameObject.transform;
        
        Transform place = other.transform.parent.transform;
        Debug.Log("Get playable");

        GameObject card = new GameObject();
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == "Playable")
            {
                card = t.GetChild(i).gameObject;
            }

        }
        try
        {
            bool test = card.GetComponent<NavMeshAgent>().Warp(place.position);
            Debug.Log("Collider - Wrap is" + test);
        }
        catch (Exception e)
        {
            Debug.Log("No Playable");
            Debug.Log(e);
            return;
        }
        
        
        card.transform.position = place.position;
        Debug.Log("Location : " + card.transform.position);
        Debug.Log("Location parent : " + card.transform.parent.transform.position);
        card.transform.rotation = place.rotation;
        card.transform.parent = place;
        card.SetActive(true);

        // mCardAsset is not available at Start(), if null, set it up
        if (!mCardAsset)
        {
            mCardAsset = card.GetComponent<CardAsset>();
        }
        // mCardManager is not available at Start(), if null, set it up
        if (!mCardManager)
        {
            mCardManager = this.transform.parent.GetComponent<CardManager>();
        }
        // Notify the CardManager
        mCardManager.AddCardToDeck(mCardAsset);
        AudioSource audioSource = GameObject.Find("Spawn").GetComponent<AudioSource>();
        audioSource.Play();
        //SetOwnerCarte();

    }

    private void SetOwnerCarte()
    {
        if (mCardManager.GetCurrentPlayerTurn())
        {
            iPlayerOwned = 1;
        }
        else
        {
            iPlayerOwned = 0;
        }
        Debug.Log("Player owner : " + iPlayerOwned);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
    }
    // Update is called once per frame
    void Update () {
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GetComponent<Animator>().SetTrigger("hit");
    }

}
