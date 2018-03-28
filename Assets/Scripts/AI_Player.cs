using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Player : MonoBehaviour {

    private List<GameObject> handPlayer = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTurnStart()
    {

        handPlayer.Add(DrawACard());
        MakeAITurn();
        EndTurn();
    }

    public GameObject DrawACard()
    {
        GameObject[] ListDeck = GameObject.FindGameObjectsWithTag("DeckAI");
        // TODO : Change dummy random
        return ListDeck[0];

    }

    public void MakeAITurn()
    {
        // TODO ADD Logic for attack with cards
        // WARNING - Probably not working as expected
        foreach(CardAsset card in GetMonsterOnBoard())
        {
            GameObject MonsterSelected = card.transform.parent.GetComponent<GameObject>();
            GameObject monsterTargeted = GetEnnemiesOnBoard()[0].transform.parent.GetComponent<GameObject>();
            AttackWithMonsters(MonsterSelected, monsterTargeted);
        }
    }

    public void EndTurn()
    {
        FindObjectsOfType<CardManager>()[0].PassTurnPlayer();
    }

    public void AttackWithMonsters(GameObject selectedMonster, GameObject targetedMonster)
    {
        CombatManager combatManager = GameObject.Find("Board").GetComponent<CombatManager>();
        combatManager.monsterSelected(selectedMonster);
        combatManager.monsterSelected(targetedMonster);
    }

    public List<CardAsset> GetMonsterOnBoard()
    {
        return FindObjectsOfType<CardManager>()[0].GetAICardOnBoard();
    }
    public List<CardAsset> GetEnnemiesOnBoard()
    {
        return FindObjectsOfType<CardManager>()[0].GetPlayerCardOnBoard();
    }
}
