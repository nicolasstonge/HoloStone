using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;



public class GazeHandler : Singleton<GazeHandler>
{
	private Color startColor;
	private CardAsset card;
	private EmissionControl emissionControl;
	public TextMesh attaqueText;
	public TextMesh healthText;
	public TextMesh descriptionText;
	public GameObject descriptionSprite;

	// Use this for initialization
	void Start()
	{
		// find the current instance of the player script:
		card =(CardAsset) FindObjectOfType(typeof(CardAsset));
		emissionControl=(EmissionControl) FindObjectOfType(typeof(EmissionControl));

		attaqueText = GameObject.Find ("Attack").GetComponent<TextMesh> ();
		healthText = GameObject.Find ("Health").GetComponent<TextMesh> ();
		//descriptionText = GameObject.Find ("Description").GetComponent<TextMesh> ();
		//descriptionSprite = GameObject.Find ("Description_Sprite");

	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnGazeEnter()
	{
		attaqueText.text = card.Attack.ToString ();
		healthText.text = card.MaxHealth.ToString ();
		//descriptionText.text = card.Description.ToString ();
		emissionControl.enableEmission ();
		//descriptionSprite.SetActive (true);


		Debug.Log ("Entrer Gaze");
		Debug.Log (card.Attack);
	}

	void OnGazeExit()
	{
		//descriptionSprite.SetActive (false);
		emissionControl.disableEmission ();
		Debug.Log ("Sortir Gaze");
	}
}
