using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
	public Text[] texts;

	void Awake ()
	{
		//Messenger.AddListener (GameEvent.NEAR_CONTAINER, HandCursor);
	}


	void OnDestroy ()
	{
		//Messenger.RemoveListener (GameEvent.NEAR_CONTAINER, HandCursor);
	}

	// Use this for initialization
	void Start ()
	{

	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
