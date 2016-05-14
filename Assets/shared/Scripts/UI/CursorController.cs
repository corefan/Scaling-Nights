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
		Messenger<int>.AddListener (GameEvent.NEAR_INTERACTIVE, Show);
		Messenger.AddListener (GameEvent.FAR_INTERACTIVE, Hide);
		Messenger.AddListener (GameEvent.SHOW_UI, Show);
		Messenger.AddListener (GameEvent.HIDE_UI, Hide);
	}

	void OnDestroy ()
	{
		Messenger<int>.RemoveListener (GameEvent.NEAR_INTERACTIVE, Show);
		Messenger.RemoveListener (GameEvent.FAR_INTERACTIVE, Hide);
		Messenger.RemoveListener (GameEvent.SHOW_UI, Show);
		Messenger.RemoveListener (GameEvent.HIDE_UI, Hide);
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
		if (GameEvent.isUiEnabled || GameEvent.isPause) {
			transform.position = Input.mousePosition;
		}
	}

	public void Show ()
	{
		
		Cursor.lockState = CursorLockMode.None;
		_cursor.sprite = _icons [0];
		_cursor.enabled = true;

	}

	public void Show (int index)
	{
		
		Cursor.lockState = CursorLockMode.None;
		_cursor.sprite = _icons [index];
		_cursor.enabled = true;
	}

	public void Hide ()
	{
		Cursor.lockState = CursorLockMode.None;
		_cursor.enabled = false;
	}
}
