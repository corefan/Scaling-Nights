using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class DialogController : MonoBehaviour
{
	[SerializeField]
	private GameObject[] dialogs;
	private int current_dialog;

	void Awake ()
	{
		Messenger<int>.AddListener (GameEvent.SHOW_DIALOG, Show);
		Messenger.AddListener (GameEvent.HIDE_DIALOG, Hide);
	}


	void OnDestroy ()
	{
		Messenger<int>.RemoveListener (GameEvent.SHOW_DIALOG, Show);
		Messenger.RemoveListener (GameEvent.HIDE_DIALOG, Hide);
	}

	// Use this for initialization
	void Start ()
	{
		current_dialog = -1;
		for (int i = 0; i < dialogs.Length; i++) {
			dialogs [i].SetActive (false);
		}

	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Show (int index)
	{
		Hide ();
		gameObject.SetActive (true);
		current_dialog = index;
		dialogs [index].SetActive (true);
	}

	public void Hide ()
	{
		gameObject.SetActive (false);
		if (current_dialog >= 0)
			dialogs [current_dialog].SetActive (false);
	}
}