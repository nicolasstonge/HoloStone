using System.Collections;
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
                AudioSource audioSource2 = GameObject.Find("ErrorClic").GetComponent<AudioSource>();
                audioSource2.Play();
                return;
            }
            if (monster.GetComponent<CardAsset>().bFirstTurn)
            {
                Debug.Log(monster.name + " just spawned, cant use it");
                AudioSource audioSource2 = GameObject.Find("ErrorClic").GetComponent<AudioSource>();
                audioSource2.Play();
                return;
            }

            if (monster.GetComponent<CardAsset>().player)
            {
                Debug.Log("Player, GTFO");
                AudioSource audioSource2 = GameObject.Find("ErrorClic").GetComponent<AudioSource>();
                audioSource2.Play();
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
                AudioSource audioSource2 = GameObject.Find("ErrorClic").GetComponent<AudioSource>();
                audioSource2.Play();
                return;
            }
            selectedMonster = monster;

            AudioSource audioSource = GameObject.Find("ClicSound").GetComponent<AudioSource>();
            audioSource.Play();
            Debug.Log("Selected " + selectedMonster.name);


            selectedMonster.GetComponent<MonsterAnim>().enableOutline("green");
        }
        else // If second select
        {
            targetMonster = monster;
            Debug.Log("Selected " + targetMonster.name);

            if (selectedMonster != targetMonster)
            {
                AudioSource audioSource = GameObject.Find("ClicSound2").GetComponent<AudioSource>();
                audioSource.Play();

                if (targetMonster.GetComponent<CardAsset>().player)
                {
                    Debug.Log("Attacking a player!");
                    // Attack the Selected player
                    int lifeLeft = selectedMonster.GetComponent<CardAsset>().AttackPlayer(targetMonster);

                    selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
                    targetMonster.GetComponent<IA_Visualize>().getHit();
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
                AudioSource audioSource = GameObject.Find("ErrorClic").GetComponent<AudioSource>();
                audioSource.Play();
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

                    //selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
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

    public void resetSelection()
    {
        selectedMonster = null;
        targetMonster = null;
    }
}
