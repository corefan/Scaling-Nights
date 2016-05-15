using UnityEngine;
using System.Collections;
using System.Deployment.Internal;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField]
	private Slider hunger;
	[SerializeField]
	private Slider thirst;
	[SerializeField]
	private Slider heat;
	// Use this for initialization
	void Start ()
	{
		_player = GameObject.Find ("Player").GetComponent <Player> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		hunger.value = _player.hunger;
		thirst.value = _player.thirst;
		heat.value = _player.heat;
	}
}
