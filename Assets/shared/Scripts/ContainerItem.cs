using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;

[RequireComponent (typeof(Collider))]
public class ContainerItem : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		Player player = other.GetComponent<Player> ();
		if (player != null) {
			Messenger.Broadcast (GameEvent.NEAR_INTERACTIVE);
		}
	}

	void OnTriggerExit (Collider other)
	{
		Player player = other.GetComponent<Player> ();
		if (player != null) {
			Messenger.Broadcast (GameEvent.FAR_INTERACTIVE);
		}
	}



}
