using UnityEngine;
using System.Collections;

public class FPSInput : MonoBehaviour
{
	public float speed = 6.0f;
	private CharacterController _charController;
	private float gravity = -9.8f;
	// Use this for initialization
	void Start ()
	{
		_charController = GetComponent<CharacterController> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!(GameEvent.isPause || GameEvent.isUiEnabled)) {
			float deltaX = Input.GetAxis ("Horizontal") * speed;
			float deltaZ = Input.GetAxis ("Vertical") * speed;
			Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
			movement = Vector3.ClampMagnitude (movement, speed);
			movement.y = gravity;
			movement *= Time.deltaTime;
			movement = transform.TransformDirection (movement);
			_charController.Move (movement);
		}
	}
}
