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
    int iPlayerLife = -1;
    int iAILife = -1;

    int iCurrentPlayerActionPoints = -1;

    const int STARTING_LIFE = 5;
    const int ACTION_POINT = 3;


    // When a new card pop on the board, this function is called
    public void AddCardToDeck(CardAsset card)
    {
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


    void Start () {
		
	}

    public void StartGame()
    {
        lCardsAI = new List<CardAsset>();
        lCardsPlayer = new List<CardAsset>();
        iPlayerLife = STARTING_LIFE;
        iAILife = STARTING_LIFE;

        bGameStarted = true;
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
        foreach(CardAsset card in cardDeck)
        {
            card.newTurn();
        }

        if (!bTurnPlayer)
        {
            // Call AI to play a turn?
        }

    }
    // Update is called once per frame
    void Update () {
        // Print the list of cards every secondes (For Debug purpose)
        if (Time.time >= fNextTime)
        {
            Debug.Log("List of currently In-Game cards :");
            foreach (CardAsset cardInGame in mlistCards)
            {
                Debug.Log(cardInGame.Description);
            }
            fNextTime += iInterval;
        }
        

    }
}
