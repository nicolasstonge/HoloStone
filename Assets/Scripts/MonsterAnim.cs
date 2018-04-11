using cakeslice;
using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAnim : MonoBehaviour, IInputClickHandler
{

    NavMeshAgent nav;
    Animator animator;
    CombatManager combatManager;
    Vector3 initialPosition;
    Quaternion initialrotation;
    public GameObject targetmonster;
    Boolean attacking;
    Boolean moving;
    Boolean rotate;
    public Outline outliner;

    // Use this for initialization
    void Start()
    {


        nav = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponent<Animator>();
        combatManager = GameObject.Find("Board").GetComponent<CombatManager>();
        attacking = false;
        moving = false;
        rotate = false;
        outliner = gameObject.GetComponentInChildren<Outline>();
        outliner.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (!nav.pathPending && moving)
        {

            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {
                    if (attacking)
                    {
                        animator.SetTrigger("attack");
                        StartCoroutine("waitForAttack");
                        attacking = false;
                        moving = false;
                    }
                    else
                    {

                        nav.stoppingDistance = 0.08f;
                        moving = false;
                        rotate = true;
                        animator.SetTrigger("idle");
                        disableOutline();
                        targetmonster.GetComponent<MonsterAnim>().disableOutline();
                    }

                }
            }
        }

        if (rotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialrotation, Time.deltaTime * 2);


            if (transform.rotation == initialrotation)
            {

                rotate = false;
            }

        }
    }

    void OnEnable()
    {
        disableOutline();
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
        initialrotation = transform.rotation;
        animator.SetTrigger("run");
        nav.SetDestination(monster.transform.position);
        attacking = true;
        moving = true;
    }

    public void disableOutline()
    {
        outliner.enabled = false;
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

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        targetmonster.GetComponent<MonsterAnim>().getHit();
        nav.stoppingDistance = 0;
        nav.SetDestination(initialPosition);
        moving = true;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        combatManager.monsterSelected(transform.gameObject);
    }
}
