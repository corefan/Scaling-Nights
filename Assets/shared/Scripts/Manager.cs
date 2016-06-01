using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Player))]
[RequireComponent (typeof(Inventory))]
public class Manager : MonoBehaviour
{
	public static Player player{ get; private set; }

	public static Inventory inventory{ get; private set; }

	public static CameraEffectController CameraEffect{ get; private set; }
	// Use this for initialization
	void Awake ()
	{
		player = GetComponent <Player> ();
		inventory = GetComponent<Inventory> ();
		CameraEffect = GameObject.Find ("MainCamera").GetComponent <CameraEffectController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
