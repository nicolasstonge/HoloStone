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
        }
        else
        {
            targetMonster = monster;

            if (selectedMonster != targetMonster)
            {
                selectedMonster.GetComponent<MonsterAnim>().attackTarget(monster);
            }

            selectedMonster = null;
            targetMonster = null;
        }
    }
}
