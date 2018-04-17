using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// CardManager to place on the scene, only one should be present.
// The cards should be a child GameObjet of the CardManager GameObject
public class CardManager : MonoBehaviour
{
    private List<CardAsset> mlistCards = new List<CardAsset>();

    int iInterval = 1;
    float fNextTime = 0;
    bool bTurnPlayer = true;
    bool bGameStarted = false;
    List<CardAsset> lCardsPlayer;
    List<CardAsset> lCardsAI;
    public int iPlayerLife = -1;    
    public int iAILife = -1;

    GameObject mYouLoose;
    GameObject mYouWon;

    private IA_Player ia;

    int iCurrentPlayerActionPoints = -1;

    const int STARTING_LIFE = 15;
    const int ACTION_POINT = 3;

	const int AI_STARTING_LIFE = 15;

	QuitGame quitgame;

    public List<CardAsset> GetAICardOnBoard()
    {
        return lCardsAI;
    }
    public List<CardAsset> GetPlayerCardOnBoard()
    {
        return lCardsPlayer;
    }

    public List<CardAsset> GetCurrentPlayerCards()
    {
        List<CardAsset> cardDeck;
        if (bTurnPlayer)
        {
            cardDeck = lCardsPlayer;
        }
        else
        {
            cardDeck = lCardsAI;
        }
        return cardDeck;
    }

    // When a new card pop on the board, this function is called
    public void AddCardToDeck(CardAsset card)
    {
        Debug.Log("Adding Card ");
        List<CardAsset> cardDeck;
        // Add card to the current turn's player
        if (bTurnPlayer)
        {
            cardDeck = lCardsPlayer;
        }
        else
        {
            cardDeck = lCardsAI;
        }
        cardDeck.Add(card);
    }

    // When a new card is removed from the board, this function is called
    public void RemoveCardFromDeck(CardAsset card)
    {
        List<CardAsset> cardDeck;
        if (card.GetComponent<DetectorAction>().iPlayerOwned == 1)
        {
            cardDeck = lCardsPlayer;
        }
        else
        {
            cardDeck = lCardsAI;
        }
        cardDeck.Remove(card);
    }

    public bool GetCurrentPlayerTurn()
    {
        return bTurnPlayer;
    }

	public int GetCurrentPlayerLife()
	{
		return iPlayerLife;
	}

	public int GetCurrentEnnemyLife()
	{
		return iAILife;
	}
	public bool GetGameStarted()
	{
		return bGameStarted;
	}

    void Start ()
    {

       
        StartGame();

        quitgame =(QuitGame) FindObjectOfType(typeof(QuitGame));
	}

    public void StartGame()
    {
        lCardsAI = new List<CardAsset>();
        lCardsPlayer = new List<CardAsset>();
        iPlayerLife = STARTING_LIFE;
        iAILife = STARTING_LIFE;

        bGameStarted = true;

		Debug.Log ("before"+iPlayerLife);
		Debug.Log ("before"+iAILife);
		Debug.Log ("Game Start");
    }

    public void PassTurnPlayer()
    {
        
        bTurnPlayer = !bTurnPlayer;
        iCurrentPlayerActionPoints = ACTION_POINT;
        List<CardAsset> cardDeck;

        if (bTurnPlayer)
        {
            cardDeck = lCardsPlayer;
        }
        else
        {
            cardDeck = lCardsAI;
        }
        try
        {
            if (cardDeck.Count > 0)
            {
                foreach (CardAsset card in cardDeck)
                {
                    Debug.Log("New Turn for " + card.name);
                    card.newTurn();
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Empty Cards");

        }


        if (!bTurnPlayer)
        {
            if (!ia)
            {
                ia = GetComponent<IA_Player>();
            }
            if (ia)
            {
                ia.OnTurnStart();
            }
            else
            {
                Debug.Log("Cant cant ref to IA_Player ia (CardManager)");
            }
            // Call AI to play a turn?
        }
    

    }

    public void getDamagePlayer(int damage)
    {
        if (bTurnPlayer)
        {
            iAILife = iAILife - damage;

            if(iAILife <= 0)
            {
                //YOU WON
                Debug.Log("YOU WON");

                GameObject[] listVictory = GameObject.FindGameObjectsWithTag("Victory");
                foreach (GameObject obj in listVictory)
                {
                    if(obj.name == "YouWin")
                    {
                        obj.SetActive(true);
                    }
                }

            }
        }
        else
        {
            iPlayerLife = iPlayerLife - damage;
            if (iPlayerLife <= 0)
            {
                //YOU LOOSE
                Debug.Log("YOU LOOSE");
                GameObject[] listVictory = GameObject.FindGameObjectsWithTag("Victory");
                foreach (GameObject obj in listVictory)
                {
                    if (obj.name == "YouLoose")
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update () {
        // Print the list of cards every secondes (For Debug purpose)
		if (iPlayerLife <= 0 || iAILife <= 0) { //Fin de partie
			//quitgame.Quit();
			if (iPlayerLife <= 0) { //Draw
				//Debug.Log ("its a draw");
			} else if (iPlayerLife <= 0) {
                Debug.Log("AI win");
            } else if (iAILife <= 0) {
                Debug.Log("Player win");
               
			}
			bGameStarted = false;

		} else {
			
			if (Time.time >= fNextTime)
            {
				Debug.Log ("List of currently In-Game cards :");
                Debug.Log("Player :");
                foreach (CardAsset cardInGame in lCardsPlayer) {
					Debug.Log (cardInGame.name);
				}
                Debug.Log("IA :");
                foreach (CardAsset cardInGame in lCardsAI)
                {
                    Debug.Log(cardInGame.name);
                }
                fNextTime += iInterval;
			}
		}
        

    }
}
