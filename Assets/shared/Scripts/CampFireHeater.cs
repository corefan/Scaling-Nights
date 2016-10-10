using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SphereCollider))]
public class CampFireHeater : MonoBehaviour
{
	public Player player;
	public float heatValue = 5;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player").GetComponent <Player> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerStay (Collider otherCollider)
	{
		if (otherCollider.gameObject.GetComponent<Player> () != null) {
			player.IncrementHeat (heatValue * Time.deltaTime);
		}

	}

}
