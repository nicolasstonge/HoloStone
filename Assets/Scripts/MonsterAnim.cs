using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAnim : MonoBehaviour {

    NavMeshAgent nav;
    Animator animator;
    CombatManager combatManager;
    Vector3 initialPosition;
    public GameObject targetmonster;

	// Use this for initialization
	void Start () {

        
        nav = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
        combatManager = GameObject.Find("Board").GetComponent<CombatManager>();
	}
	
	// Update is called once per frame
	void Update () {


        
    }

    public void getHit()
    {
        animator.SetTrigger("hit");
    }

    public void OnMouseDown()
    {
        combatManager.monsterSelected(transform.gameObject); 
    }

    public void attackTarget(GameObject monster)
    {
        targetmonster = monster;
        initialPosition = transform.position;
        animator.SetTrigger("run");
        nav.SetDestination(monster.transform.position);   
    }

    

    
}
