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

	// Use this for initialization
	void Start()
	{
		// find the current instance of the player script:
		card =(CardAsset) FindObjectOfType(typeof(CardAsset));
		emissionControl=(EmissionControl) FindObjectOfType(typeof(EmissionControl));

		attaqueText = GameObject.Find ("Attack").GetComponent<TextMesh> ();
		healthText = GameObject.Find ("Health").GetComponent<TextMesh> ();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnGazeEnter()
	{
		attaqueText.text = "Attack: " + card.Attack.ToString ();
		healthText.text = "Health: " + card.MaxHealth.ToString ();
		emissionControl.enableEmission ();

		Debug.Log ("Entrer Gaze");
		Debug.Log (card.Attack);
	}

	void OnGazeExit()
	{
		emissionControl.disableEmission ();
		Debug.Log ("Sortir Gaze");
	}
}
