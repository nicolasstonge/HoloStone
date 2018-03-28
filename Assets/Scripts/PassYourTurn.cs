using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PassYourTurn : MonoBehaviour, IInputClickHandler
{

    CardManager manager;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        manager.PassTurnPlayer();
    }

    // Use this for initialization
    void Start()
    {
        manager = FindObjectsOfType<CardManager>()[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
