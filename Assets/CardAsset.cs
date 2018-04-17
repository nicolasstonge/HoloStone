using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TargetingOptions
{
	NoTarget,
	AllCreatures, 
	EnemyCreatures,
	YourCreatures, 
	AllCharacters, 
	EnemyCharacters,
	YourCharacters
}

public class CardAsset : MonoBehaviour 
{
	// this object will hold the info about the most general card
	[Header("General info")]
	//    public CharacterAsset characterAsset;  // if this is null, it`s a neutral card
	[TextArea(2,3)]
	public string Description;  // Description for spell or character
	public Sprite CardImage;
	public int ManaCost;

	[Header("Creature Info")]
	public int MaxHealth;   // =0 => spell card
	public int Attack;
	public int AttacksForOneTurn = 1;
	public bool Charge;
	public string CreatureScriptName;
	public int specialCreatureAmount;
    public bool player = false;

	[Header("SpellInfo")]
	public string SpellScriptName;
	public int specialSpellAmount;
	public TargetingOptions Targets;


    // STAT INGAME
    private int iCurrentHealth;
    private int iCurrentAttack;
    private bool bIsAlive;
    public bool bAlreadyAttack;

    void Start()
    {
        iCurrentHealth = MaxHealth;
        iCurrentAttack = Attack;
        bIsAlive = true;
        bAlreadyAttack = false;

        UpdateStats();

    }

    public int AttackMonster(GameObject monster)
    {
        int lifeLeft = monster.GetComponent<CardAsset>().TakeHit(iCurrentAttack);
        bAlreadyAttack = true;
        return lifeLeft;
    }

    public int GetCurrentHealth()
    {
        return iCurrentHealth;
    }

    public int TakeHit(int attack)
    {

        int newLife = iCurrentHealth - attack;

        if(newLife <= 0)
        {
            bIsAlive = false;
            iCurrentHealth = 0;
            if(!player)
            {
                GetComponent<MonsterAnim>().Die();
            }
            



        }
        else
        {
            iCurrentHealth = newLife;
        }

        // Don't forget to update stats for the visualization
        UpdateStats();

        if (player)
        {
            GameObject cardManObj = GameObject.Find("CardManager");
            CardManager cardManager = cardManObj.GetComponent<CardManager>();
            cardManager.getDamagePlayer(attack);
        }

        return newLife;
    }

    internal int AttackPlayer(GameObject targetPlayer)
    {
        int lifeLeft = targetPlayer.GetComponent<CardAsset>().TakeHit(iCurrentAttack);
        bAlreadyAttack = true;
        return lifeLeft;
    }

    public void newTurn()
    {
        bAlreadyAttack = false;
    }

    public void onDeath()
    {
        CardManager cardManager = this.transform.parent.GetComponent<CardManager>();
        // On previent le CardManager
        cardManager.RemoveCardFromDeck(this);

    }

    // Update stats in the sprite for health and attack
    public void UpdateStats()
    {
        TextMesh[] textmeshs = GetComponentsInChildren<TextMesh>();
        foreach (TextMesh textmesh in textmeshs)
        {
            if (textmesh.name == "Health")
            {
                textmesh.text = iCurrentHealth.ToString();
            }
            else if (textmesh.name == "Attack")
            {
                textmesh.text = iCurrentAttack.ToString();
            }
        }
    }
}
