using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public Button startButton;
	public GameObject LifePanel;
	CardManager manager;
	Text startButtonText;


	void Start () {
		Button btn = startButton.GetComponent<Button> ();


		btn.onClick.AddListener (StartGameTask);

		manager =(CardManager) FindObjectOfType(typeof(CardManager));
		startButtonText = GameObject.Find ("StartButtonText").GetComponent<Text> ();
	}
	
	void Update()
	{
		if (!manager.GetGameStarted()) 
		{
			startButtonText.text = "Lancer partie";
		} 
		else if (manager.GetGameStarted()) 
		{
			startButtonText.text = "Relancer partie";
		}
	}
	
	// Update is called once per frame
	void StartGameTask()
	{

		if (!manager.GetGameStarted()) //On commence une partie
		{
			manager.StartGame ();
			ShowLifeBar ();
		} 
		else if (manager.GetGameStarted()) //On relance la partie
		{
			manager.StartGame ();
		}
	}

	public void ShowLifeBar()
	{
		LifePanel.gameObject.SetActive (true);
	}

}
