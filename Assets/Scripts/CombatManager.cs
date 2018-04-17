﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public GameObject selectedMonster;
    public GameObject targetMonster;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void monsterSelected(GameObject monster)
    {
        if (selectedMonster == null)
        {
            // You cant select a monster you already play this turn
            if (monster.GetComponent<CardAsset>().bAlreadyAttack)
            {
                Debug.Log(monster.name + " already attacked");
                return;
            }
            if (monster.GetComponent<CardAsset>().bFirstTurn)
            {
                Debug.Log(monster.name + " just spawned, cant use it");
                return;
            }

            if (monster.GetComponent<CardAsset>().player)
            {
                Debug.Log("Player, GTFO");
                return;
            }
            

            GameObject cardManObj = GameObject.Find("CardManager");
            CardManager cardManager = cardManObj.GetComponent<CardManager>();

            bool isOwned = false;
            foreach (CardAsset card in cardManager.GetCurrentPlayerCards())
            {
                if (card == monster.GetComponent<CardAsset>())
                {
                    Debug.Log("Good Owner!");
                    isOwned = true;
                }
            }
            if (!isOwned)
            {
                Debug.Log("Bad Owner!");
                return;
            }
            selectedMonster = monster;
            Debug.Log("Selected " + selectedMonster.name);


            selectedMonster.GetComponent<MonsterAnim>().enableOutline("green");
        }
        else
        {
            targetMonster = monster;
            Debug.Log("Selected " + targetMonster.name);

            if (selectedMonster != targetMonster)
            {
                if (targetMonster.GetComponent<CardAsset>().player)
                {
                    Debug.Log("Attacking a player!");
                    // Attack the Selected player
                    int lifeLeft = selectedMonster.GetComponent<CardAsset>().AttackPlayer(targetMonster);

                    selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
                    //targetMonster.GetComponent<IA_Visualize>().enableOutline("red");
                }
                else
                {
                    Debug.Log("Attacking a Monster!");
                    Debug.Log("Attacking a player!");
                    // Attack the Selected Monster
                    int lifeLeft = selectedMonster.GetComponent<CardAsset>().AttackMonster(targetMonster);

                    selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
                    //targetMonster.GetComponent<MonsterAnim>().enableOutline("red");
                }
                selectedMonster.GetComponent<MonsterAnim>().disableOutline();


            }
            else
            {
                Debug.Log("Seems that " + selectedMonster.name + " == " + targetMonster.name);
            }
            selectedMonster.GetComponent<MonsterAnim>().disableOutline();
            selectedMonster = null;
            targetMonster = null;
        }
    }

    public void monsterSelected(GameObject monster, bool isIA)
    {
        if (selectedMonster == null)
        {
            if(monster.GetComponent<CardAsset>().player)
            {
                return;
            }
            
            selectedMonster = monster;
            
        }
        else
        {
            targetMonster = monster;

            if (selectedMonster != targetMonster)
            {
                if(targetMonster.GetComponent<CardAsset>().player)
                {
                    // Attack the Selected Monster
                    int lifeLeft = selectedMonster.GetComponent<CardAsset>().AttackPlayer(targetMonster);

                    selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
                }
                else
                {
                    // Attack the Selected Monster
                    int lifeLeft = selectedMonster.GetComponent<CardAsset>().AttackMonster(targetMonster);

                    selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
                }
                
            }

            selectedMonster = null;
            targetMonster = null;
        }
    }
}
