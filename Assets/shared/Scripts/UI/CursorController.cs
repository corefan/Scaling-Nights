using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class CursorController : MonoBehaviour
{

	private Image _cursor;
	[SerializeField] private Sprite[] _icons;

	void Awake ()
	{
		Messenger<int>.AddListener (GameEvent.SHOW_CURSOR, Show);
		Messenger.AddListener (GameEvent.HIDE_CURSOR, Hide);
	}

	void OnDestroy ()
	{
		Messenger<int>.RemoveListener (GameEvent.SHOW_CURSOR, Show);
		Messenger.RemoveListener (GameEvent.HIDE_CURSOR, Hide);
	}
	// Use this for initialization
	void Start ()
	{
		_cursor = GetComponent <Image> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		_cursor.enabled = false;

	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Input.mousePosition;
	}

	public void Show (int index)
	{
		GameEvent.isCursorEnabled = true;
		Cursor.lockState = CursorLockMode.None;
		_cursor.sprite = _icons [index];
		_cursor.enabled = true;

	}

	public void Hide ()
	{
		GameEvent.isCursorEnabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		_cursor.enabled = false;

	}
}
