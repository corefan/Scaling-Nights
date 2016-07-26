using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class Player : MonoBehaviour
{
	public float health = 100;
	public float hunger = 100;
	public float thirst = 100;
	public float heat = 100;
	public float hunger_scale = 0.8f;
	public float thirst_scale = 1.2f;
	public float heat_scale = 1f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameEvent.gameOver) {
			if (health > 0) {
				hunger -= 1 * Time.deltaTime * hunger_scale;
				thirst -= 1 * Time.deltaTime * thirst_scale;
				heat -= 1 * Time.deltaTime * heat_scale;
				if (hunger < 0) {
					health -= hunger_scale * Time.deltaTime;
				}
				if (thirst < 0) {
					health -= thirst_scale * Time.deltaTime;
				}
				if (heat < 0) {
					health -= heat_scale * Time.deltaTime;
				}

			} else {
				Debug.Log ("END");
				GameEvent.GameOver ();
			}
		}
	}

	public void DecrementHunger (float value)
	{
		IncrementValue (ref hunger, value, false);
	}

	public void DecrementThirst (float value)
	{
		IncrementValue (ref thirst, value, false);
	}

	public void DecrementHeat (float value)
	{
		IncrementValue (ref heat, value, false);
	}

	private void IncrementValue (ref float base_value, float value, bool recursion)
	{
		
		if (base_value + value > 100) {
			base_value += value - (base_value + value - 100);
		} else {
			base_value = 100;
		}
		if (!recursion) {
			IncrementValue (ref health, value, true);
		}
	}

	public void TakeDamage (int damage)
	{
		if (health > 0) {
			health -= damage;
		}
	}
}
