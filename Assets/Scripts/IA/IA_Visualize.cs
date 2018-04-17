using cakeslice;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Visualize : MonoBehaviour, IInputClickHandler
{


    CombatManager combatManager;
    Vector3 initialPosition;
    Quaternion initialrotation;
    public GameObject targetmonster;

    public Outline outliner;

    // Use this for initialization
    void Start()
    {

        combatManager = GameObject.Find("Board").GetComponent<CombatManager>();
        outliner = gameObject.GetComponentInChildren<Outline>();
        outliner.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void disableOutline()
    {
        if (outliner)
        {
            outliner.enabled = false;
        }
        else
        {
            return;
        }
    }

    public void enableOutline(string color)
    {
        outliner.enabled = true;

        if (color == "green")
        {
            outliner.color = 1;
        }
        else
        {
            outliner.color = 0;
        }

    }

    void OnEnable()
    {
        disableOutline();
    }


    public void getHit()
    {
       // TODO
    }

    public void OnMouseDown()
    {
        combatManager.monsterSelected(transform.gameObject);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        combatManager.monsterSelected(transform.gameObject);
    }
}
