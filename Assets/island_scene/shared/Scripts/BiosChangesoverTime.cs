using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BiosChangesoverTime : MonoBehaviour {
	public Slider hunger;
	public Slider thirst;
	public Slider heat;
	Player bios;
	// Use this for initialization
	void Start () {
		bios = GetComponent<Player> ();
		hunger.value = bios.hunger;
		thirst.value = bios.thirst;
		heat.value = bios.heat;
	}
	
	// Update is called once per frame
	void Update () {
		hunger.value = bios.hunger;
		thirst.value = bios.thirst;
		heat.value = bios.heat;
	}
}
