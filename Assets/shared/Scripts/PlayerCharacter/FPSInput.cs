using UnityEngine;
using System.Collections;

public class FPSInput : MonoBehaviour
{
	public float speed;
	public float walkSpeed = 2.0f;
	public float runSpeed = 8.0f;
	[SerializeField]
	private Animator _animator;
	private CharacterController _charController;
	private float gravity = -9.8f;
	// Use this for initialization
	void Start ()
	{
		speed = walkSpeed;
		_charController = GetComponent<CharacterController> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!(GameEvent.isPause || GameEvent.isUiEnabled)) {
			if (Input.GetButton ("Run")) {
				speed = runSpeed;
			} else {
				speed = walkSpeed;
			}
			float deltaX = Input.GetAxis ("Horizontal") * speed;
			float deltaZ = Input.GetAxis ("Vertical") * speed;
			Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
			_animator.SetFloat ("Speed", movement.magnitude);
			movement = Vector3.ClampMagnitude (movement, speed);
			movement.y = gravity;
			movement *= Time.deltaTime;
			movement = transform.TransformDirection (movement);

			_charController.Move (movement);
		}
	}
}
