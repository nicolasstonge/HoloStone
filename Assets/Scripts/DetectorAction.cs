using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DetectorAction : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    // Ref on the CardManager
    private CardManager mCardManager;
    // Ref on the CardAsset 
    private CardAsset mCardAsset;

    // Function triggered when the object tracking state change (AKA detected or not detected)
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            
            if(!mCardAsset)
            {
                mCardAsset = GetComponent<CardAsset>();
            }
            if(!mCardManager)
            {
                mCardManager = this.transform.parent.GetComponent<CardManager>();
            }
            // Notify the CardManager
            mCardManager.OnTrackedCard(mCardAsset);

        }
        else
        {
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
            mCardManager.LostTrackedCard(mCardAsset);
        }
    }


    void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
