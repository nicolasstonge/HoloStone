using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using HoloToolkit.Unity.InputModule;

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

        GameObject card = this.transform.GetChild(0).gameObject;
        card.transform.position = other.transform.position;
        card.transform.rotation = other.transform.rotation;
        card.transform.parent = other.transform.parent.transform;
        card.SetActive(true);

        // mCardAsset is not available at Start(), if null, set it up
        if (!mCardAsset)
        {
            mCardAsset = GetComponent<CardAsset>();
        }
        // mCardManager is not available at Start(), if null, set it up
        if (!mCardManager)
        {
            mCardManager = this.transform.parent.GetComponent<CardManager>();
        }
        // Notify the CardManager
        mCardManager.AddCardToDeck(mCardAsset);
        SetOwnerCarte();

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
