using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[RequireComponent(typeof(CharacterController))]
public class WolfsMovement: MonoBehaviour {

	private Animator _animator;
	public float rotSpeed = 15.0f;
	public float moveSpeed;
	public const float originalMoveSpeed = 6.0f;
	public const float maiuscSpeed = 15.0f;
	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -10.0f;
	public float _minFall = -1.5f;
	private float _vertSpeed;
	private ControllerColliderHit _contact;
	CharacterController _characterController;


	// Use this for initialization
	void Start () {
		_characterController = GetComponent <CharacterController> ();
		_animator = GetComponent <Animator> ();
		_vertSpeed = _minFall;
		moveSpeed = originalMoveSpeed;
	}

	// Update is called once per frame
	void Update () {
		Vector3 movement = Vector3.zero;
		float horInput = Input.GetAxis ("Horizontal");
		float vertInput = Input.GetAxis ("Vertical");

		if(horInput != 0 || vertInput != 0) {

			//		}

			if (Input.GetKey (KeyCode.LeftShift)) {
				moveSpeed = maiuscSpeed;
				movement.x = horInput * moveSpeed;
				movement.z = vertInput * moveSpeed;
				movement = Vector3.ClampMagnitude (movement, moveSpeed);
//				Quaternion tmp = target.rotation;
//				target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
//				movement = target.TransformDirection (movement);
//				target.rotation = tmp;
				//	transform.rotation = Quaternion.LookRotation (movement);
				Quaternion direction = Quaternion.LookRotation (movement);
				transform.rotation = Quaternion.Lerp (transform.rotation, direction, rotSpeed * Time.deltaTime);
			} else {
				moveSpeed = originalMoveSpeed;
				movement.x = horInput * moveSpeed;
				movement.z = vertInput * moveSpeed;
				movement = Vector3.ClampMagnitude (movement, moveSpeed);
//				Quaternion tmp = target.rotation;
//				target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
//				movement = target.TransformDirection (movement);
//				target.rotation = tmp;
				//	transform.rotation = Quaternion.LookRotation (movement);
				Quaternion direction = Quaternion.LookRotation (movement);
				transform.rotation = Quaternion.Lerp (transform.rotation, direction, rotSpeed * Time.deltaTime);
			}
			//		if (_characterController.isGrounded) {
			////			Debug.Log ("kdfgsfgdf");
			//			if (Input.GetButtonDown ("Jump")) {
			//				_vertSpeed = jumpSpeed;
			//			} else {
			//				_vertSpeed = _minFall;
			//			}
			//		} else {
			//			_vertSpeed += gravity * 5 * Time.deltaTime;
			//			if (_vertSpeed < terminalVelocity) {
			//				_vertSpeed = terminalVelocity;
			//			}
			//		}
			//		bool hitGround = false;
			//		RaycastHit hit;
			//		if (_vertSpeed < 0 && Physics.Raycast (transform.position, Vector3.down, out hit)) {
			//			float check = (_characterController.height + _characterController.radius) / 1.9f;
			//			hitGround = hit.distance <= check;
			//		}

			bool hitGround = false;
			RaycastHit hit;
			if (_vertSpeed < 0 && Physics.Raycast (transform.position, Vector3.down, out hit)) {
				float check = (_characterController.height + _characterController.radius) / 1.9f;
				hitGround = hit.distance <= check;
			}
			_animator.SetFloat ("Speed",movement.magnitude);

			if (hitGround) {
					_vertSpeed = _minFall;
			} else {
				_vertSpeed += gravity * 5 * Time.deltaTime;
				if (_vertSpeed < terminalVelocity) {
					_vertSpeed = terminalVelocity;
				}
//				
				if (_characterController.isGrounded) {
					if(Vector3.Dot (movement, _contact.normal) < 0) {
						movement = _contact.normal * moveSpeed;
					} else {
						movement += _contact.normal * moveSpeed;
					}
				}
			}


			movement.y = _vertSpeed;
			movement *= Time.deltaTime;
			_characterController.Move (movement);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		_contact = hit;
	}
}
