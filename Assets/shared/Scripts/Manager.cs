using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Inventory))]
public class Manager : MonoBehaviour
{
	public static Player player{ get; private set; }

	public static Inventory inventory{ get; private set; }

	public static CameraEffectController CameraEffect{ get; private set; }
	// Use this for initialization
	void Awake ()
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
		inventory = GetComponent<Inventory> ();
		CameraEffect = GameObject.Find ("MainCamera").GetComponent <CameraEffectController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public static void ShowDialogWithTimer (int index, MonoBehaviour instance)
	{
		instance.StartCoroutine (ShowDialogWithTimeOut (1));
	}

	static IEnumerator ShowDialogWithTimeOut (int index)
	{
		Messenger.Broadcast (GameEvent.HIDE_DIALOG);
		yield return new WaitForSeconds (1);
		Messenger <int>.Broadcast (GameEvent.SHOW_DIALOG, index);
		yield return new WaitForSeconds (2);
		Messenger.Broadcast (GameEvent.HIDE_DIALOG);
	}

}
