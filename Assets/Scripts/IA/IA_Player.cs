using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Player : MonoBehaviour
{
    private int currentSlot = 0;
    private List<GameObject> handPlayer = new List<GameObject>();

    // Ref on the CardManager
    private CardManager mCardManager;
    private CombatManager mcombatManager;

    public System.Random rnd;



    void Start()
    {
        // Create random seed
        rnd = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTurnStart()
    {

        handPlayer.Add(Instantiate(DrawACard()));
        MakeAITurn();
        EndTurn();
    }

    public GameObject DrawACard()
    {
        GameObject[] ListDeck = GameObject.FindGameObjectsWithTag("DeckAI");
        // TODO : Change dummy random
        return ListDeck[rnd.Next(0, ListDeck.Length)];

    }

    public void placeCard(GameObject cardObject)
    {
        int i2 = 0;
        Debug.Log("IA Want to place a card");
        Debug.Log(cardObject.name);

        GameObject newCardObject = Instantiate(cardObject);
        Debug.Log("Using Slot " + currentSlot);
        if(currentSlot > 2)
        {
            Debug.Log("Err, Max Slot reached");
            return;
        }
        GameObject myspot = GetSlots()[currentSlot];
        Debug.Log("Got spot : " + myspot.name);
        currentSlot = currentSlot + 1;
        Debug.Log("Next Slot is " + currentSlot);



        Transform t = newCardObject.transform;
        GameObject card = new GameObject();
        //GameObject card = this.transform.GetChild(0).gameObject;
        Transform place = myspot.transform;
        Debug.Log("Looking for Playable tag");
        for (int i = 0; i < t.childCount; i++)
        {
            try
            {
                if (t.GetChild(i).gameObject.tag == "Playable")
                {
                    Debug.Log("Found Playable!");
                    card = t.GetChild(i).gameObject;
                }
            }
            catch( Exception e)
            {
                Debug.Log(e.ToString());
            }
        
        }
        try
        {
            bool test = card.GetComponent<NavMeshAgent>().Warp(place.position);
            Debug.Log("IA - Wrap is " + test);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }

        // Set parent spot
        //card.transform.parent.transform.position = place.position;

        // Set child spot
        card.transform.position = place.position; // new Vector3(0, 0, 0);
        
        Debug.Log("Location parent : " + card.transform.parent.transform.position);
        Debug.Log("Location child : " + card.transform.position);
        
        card.transform.parent = place;
        Debug.Log("Setting the card active... " + card.name);
        card.SetActive(true);
        card.transform.Rotate(Vector3.up, 180);
        Debug.Log("Should see the card now.");


        CardAsset cardAssetTmp = card.GetComponent<CardAsset>();
        Debug.Log("Got Card Asset.");
        // mCardManager is not available at Start(), if null, set it up
        if (!mCardManager)
        {
            Debug.Log("Get Card Manager.");
            //mCardManager = cardObject.transform.parent.GetComponent<CardManager>();
            GameObject cardManObj = GameObject.Find("CardManager");
            mCardManager = cardManObj.GetComponent<CardManager>();
            Debug.Log("Got Card Manager.");
        }
        Debug.Log("Add Cart to deck.");
        // Notify the CardManager
        if(mCardManager)
        {
            mCardManager.AddCardToDeck(cardAssetTmp);
            Debug.Log("Cart added to the Deck.");
        }
        else
        {
            Debug.Log("Err, no card Manager");
        }

        handPlayer.Remove(cardObject);
        Debug.Log("Cart removed from hand.");
    }

    public GameObject[] GetSlots()
    {
        GameObject[] ListSlots = GameObject.FindGameObjectsWithTag("IASlots");
        Debug.Log("IA - Got " + ListSlots.Length + " slots");
        // TODO : Change dummy random
        return ListSlots;

    }

    public void MakeAITurn()
    {
        // TODO ADD Logic for attack with cards
        if (handPlayer[0])
        {
            placeCard(handPlayer[0]);
        }
        // WARNING - Probably not working as expected
        List<CardAsset> CardsIA = GetMonsterOnBoard();
        if(CardsIA != null)
        {
            try
            {
                
                if (CardsIA.Count > 0 )
                {
                    foreach (CardAsset card in GetMonsterOnBoard())
                    {
                        int nbrEnnemies = GetEnnemiesOnBoard().Count;
                        // Check if its not first turn for the card
                        if (!card.bFirstTurn && nbrEnnemies > 0)
                        {
                            Debug.Log("Attack with " + card.name);
                            GameObject MonsterSelected = (card as MonoBehaviour).gameObject;
                            GameObject monsterTargeted = (GetEnnemiesOnBoard()[0] as MonoBehaviour).gameObject;
                            AttackWithMonsters(MonsterSelected, monsterTargeted);
                        }
                        // If no monster, lets attack the player
                        else if(!card.bFirstTurn && nbrEnnemies == 0 && mCardManager)
                        {
                            Debug.Log("Attack Player with " + card.name);
                            CardAsset currentPlayerStats = mCardManager.getPlayerStats();
                            GameObject MonsterSelected = (card as MonoBehaviour).gameObject;
                            GameObject monsterTargeted = (currentPlayerStats as MonoBehaviour).gameObject;
                            AttackWithMonsters(MonsterSelected, monsterTargeted);

                        }
                        else
                        {
                            Debug.Log("No cardManager");
                        }

                    }
                }

            }
            catch (Exception e)
            {
                Debug.Log("Err MakeAITurn");
                Debug.Log(e.ToString());
            }
        }


    }

    public void EndTurn()
    {
        FindObjectsOfType<CardManager>()[0].PassTurnPlayer();
    }

    public void AttackWithMonsters(GameObject selectedMonster, GameObject targetedMonster)
    {
        if(!mcombatManager)
        {
            Debug.Log("Getting Combat Manager");
            mcombatManager = GameObject.Find("Board").GetComponent<CombatManager>();
        }
        if (mcombatManager)
        { 
            if(selectedMonster && targetedMonster)
            {
                Debug.Log("Select card 1.. " + selectedMonster.name);
                mcombatManager.monsterSelected(selectedMonster, true);
                Debug.Log("Select card 2.. " + targetedMonster.name);
                mcombatManager.monsterSelected(targetedMonster, true);
            }
            if(!selectedMonster)
            {
                Debug.Log("No seletedMonster");
            }
            if (!targetedMonster)
            {
                Debug.Log("No targetedMonster");
            }
        }
        else
        {
            Debug.Log("Err, no combatManager to attack monster");
        }
    }

    public List<CardAsset> GetMonsterOnBoard()
    {
        return mCardManager.GetAICardOnBoard();
    }
    public List<CardAsset> GetEnnemiesOnBoard()
    {
        return mCardManager.GetPlayerCardOnBoard();
    }
}
