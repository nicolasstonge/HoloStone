using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {

	Text playerLifeText;
	Text ennemyLifeText;
	CardManager manager;
	public int playerLifePoint;
	public int ennemyLifePoint;
	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);

		manager =(CardManager) FindObjectOfType(typeof(CardManager));
	}
	
	void Update()
	{
		playerLifePoint = manager.GetCurrentPlayerLife();
		ennemyLifePoint = manager.GetCurrentEnnemyLife();
		if (this.gameObject.activeSelf) 
		{
			playerLifeText = GameObject.Find ("PlayerLifePoint").GetComponent<Text> ();
			playerLifeText.text = playerLifePoint.ToString ();
			Debug.Log (playerLifeText.text);

			ennemyLifeText = GameObject.Find ("EnnemyLifePoint").GetComponent<Text> ();
			ennemyLifeText.text = ennemyLifePoint.ToString ();
			Debug.Log (ennemyLifeText.text);
		}

	}


}
