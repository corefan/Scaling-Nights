using UnityEngine;
using System.Collections;
using System;

public class Consumable : Item
{
	public string player_field;
	public float value;

	void Awake ()
	{
		tag = "Consumable";
	}
	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public new void Use ()
	{
		base.Use ();
		Debug.Log ("USE");
		switch (player_field.ToUpper ()) {
		case "HUNGER":
			Manager.player.DecrementHunger (value);
			Destroy (gameObject);
			break;
		case "THIRST":
			Manager.player.DecrementThirst (value);
			Destroy (gameObject);
			break;
		default:
			Debug.Log ("parameter doesn't match");
			break;
		}
	}
}
