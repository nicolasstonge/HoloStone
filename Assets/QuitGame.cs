using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour {

	public Button startButton;
	public GameObject LifePanel;
	CardManager manager;
	Text startButtonText;


	void Start () {
		Button btn = startButton.GetComponent<Button> ();


		btn.onClick.AddListener (QuitGameTask);

		manager =(CardManager) FindObjectOfType(typeof(CardManager));
		startButtonText = GameObject.Find ("StartButtonText").GetComponent<Text> ();
	}

	void Update()
	{
	}

	// Update is called once per frame
	void QuitGameTask()
	{
		Quit ();
	}

	public void Quit()
	{
			HideLifeBar ();
			startButtonText.text = "Lancer partie";
	}

	public void ShowLifeBar()
	{
		LifePanel.gameObject.SetActive (true);
	}

	public void HideLifeBar()
	{
		LifePanel.gameObject.SetActive (false);
	}
}
