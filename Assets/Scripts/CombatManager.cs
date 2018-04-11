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
            selectedMonster = monster;
            selectedMonster.GetComponent<MonsterAnim>().enableOutline("green");
        }
        else
        {
            targetMonster = monster;

            if (selectedMonster != targetMonster)
            {
                // Attack the Selected Monster
                int lifeLeft = selectedMonster.GetComponent<CardAsset>().AttackMonster(targetMonster);
                
                selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
                targetMonster.GetComponent<MonsterAnim>().enableOutline("red");
            }

            selectedMonster = null;
            targetMonster = null;
        }
    }
}
