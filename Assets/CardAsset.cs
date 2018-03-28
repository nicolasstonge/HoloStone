using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	[Header("SpellInfo")]
	public string SpellScriptName;
	public int specialSpellAmount;
	public TargetingOptions Targets;


    // STAT INGAME
    private int iCurrentHealth;
    private int iCurrentAttack;
    private bool bIsAlive;
    private bool bAlreadyAttack;

    void Start()
    {
        iCurrentHealth = MaxHealth;
        iCurrentAttack = Attack;
        bIsAlive = true;
        bAlreadyAttack = false;


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

        }
        else
        {
            iCurrentHealth = newLife;
        }
        TextMesh[] textmeshs = GetComponentsInChildren<TextMesh>();
        foreach(TextMesh textmesh in textmeshs)
        {
            if(textmesh.name == "Health")
            {
                textmesh.text = iCurrentHealth.ToString();
            }
        }

        
        return newLife;
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
}
