using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PassYourTurn : MonoBehaviour, IInputClickHandler
{

    CardManager manager;

    // Variable for moving the button
    float speed = 3f;
    float height = 0.01f;
    bool bActivated = false;
    float timer = 0;
    // Position before starting moving
    Vector3 vPos;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        manager.PassTurnPlayer();
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        bActivated = true;
    }

    // Use this for initialization
    void Start()
    {
        manager = FindObjectsOfType<CardManager>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(bActivated)
        {
            // If first time, get the time
            if(timer == 0)
            {
                timer = Time.time;
                vPos = transform.position;
            }
            // Get the Transform's position
            Vector3 pos = transform.position;
            // To make it smooth, we use a nice sin wave
            float newY = pos.y + Mathf.Sin(Time.time * speed)*0.01f;
            transform.position = new Vector3(pos.x, newY, pos.z) ;

            
            if(Time.time-timer > 2)
            {
                bActivated = false;
                transform.position = vPos;
            }
        }

    }
}
