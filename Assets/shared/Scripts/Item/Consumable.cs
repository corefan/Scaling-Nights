using UnityEngine;
using System.Collections;
using System;

public class Consumable : Item
{
	public string player_field;
	public float value;

	void Awake ()
	{
		tag = "Lootable";
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
		switch (player_field.ToUpper ()) {
		case "HUNGER":
			Manager.player.DecrementHunger (value);
			break;
		case "THIRST":
			Manager.player.DecrementThirst (value);
			break;
		default:
			Debug.Log ("parameter doesn't match");
			break;
		}
	}
}
