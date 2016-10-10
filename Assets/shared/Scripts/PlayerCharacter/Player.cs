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

	private AlarmingPlayerDecal alarmingDecal;
	private DayNightLight sun;

	// Use this for initialization
	void Start ()
	{
		alarmingDecal = GetComponent<AlarmingPlayerDecal> ();
		sun = FindObjectOfType <DayNightLight> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameEvent.gameOver) {
			if (health > 0) {
				DecrementHunger (-1 * Time.deltaTime * hunger_scale);
				DecrementThirst (-1 * Time.deltaTime * thirst_scale);
				if (!sun.GetDayPhase ()) {
					DecrementHeat (-1 * Time.deltaTime * heat_scale);
				}
				if (hunger <= 0) {
					health -= hunger_scale * Time.deltaTime;
				}
				if (thirst <= 0) {
					health -= thirst_scale * Time.deltaTime;
				}
				if (heat <= 0) {
					health -= heat_scale * Time.deltaTime;
				}
				alarmingDecal.ActivateDecalsByStatus ("dying", health);
			} else {
				GameEvent.GameOver ();
			}
		}
	}

	public void DecrementHunger (float value)
	{
		IncrementValue (ref hunger, value);
	}

	public void DecrementThirst (float value)
	{
		IncrementValue (ref thirst, value);
	}

	public void DecrementHeat (float value)
	{
		IncrementValue (ref heat, value);
	}

	public void IncrementHeat (float value)
	{
		IncrementValue (ref heat, value);
	}

	private void IncrementValue (ref float base_value, float value)
	{
		
		if (base_value + value > 100) {
			base_value += value - (base_value + value - 100);
		} else {
			base_value += value;
		}
	}

	public void TakeDamage (int damage)
	{
		if (health > 0) {
			IncrementValue (ref health, damage * -1);
			alarmingDecal.ActivateDecalsByStatus ("gettingHurt", health);
			alarmingDecal.FadeCurrentDecal ();
		}
	}
}
