using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float hunger = 100;
	public float thirst = 100;
	public float heat = 100;
	public float hunger_scale = 1.5f;
	public float thirst_scale = 1.5f;
	public float heat_scale = 1.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		hunger -= 1 * Time.deltaTime / hunger_scale;
		thirst -= 1 * Time.deltaTime / thirst_scale;
		heat -= 1 * Time.deltaTime / heat_scale;
	}
}
