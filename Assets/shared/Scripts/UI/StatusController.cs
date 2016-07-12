using UnityEngine;
using System.Collections;
using System.Deployment.Internal;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
	[SerializeField]
	private Slider hunger;
	[SerializeField]
	private Slider thirst;
	[SerializeField]
	private Slider heat;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		hunger.value = Manager.player.hunger;
		thirst.value = Manager.player.thirst;
		heat.value = Manager.player.heat;
	}
}
