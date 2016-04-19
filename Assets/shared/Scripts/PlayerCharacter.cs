using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

	private int _health;
	private int constHealth;
	public Slider slider;
	public Text gameOver;

	// Use this for initialization
	void Start () {
		gameOver.enabled = false;
		//FIXME
		_health = 5;
		constHealth = _health;
	}


	public void Hurt(int damage) {
		_health -= damage;
		slider.value -= damage * slider.maxValue / constHealth;
//		Debug.Log ("Healt: " + _health);
	}


	void Update () {
		if (_health == 0) {
			gameOver.enabled = true;
			Time.timeScale = 0;
		}

	}

}
