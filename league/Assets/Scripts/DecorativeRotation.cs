using UnityEngine;
using System.Collections;

public class DecorativeRotation : MonoBehaviour {
	public Rigidbody rigidBody;
	public GameObject carFront;
	public GameObject carBack;
	private Vector3 turnVector;
	private float turnInput;
	public float rotationMulu;
	// Use this for initialization
	void Start () {
		turnVector = carFront.transform.position - carFront.transform.position;
		turnInput = Input.GetAxis ("Horizontal");
	}
	
	// Update is called once per frame
	void Update () {
		turnInput = Input.GetAxis ("Horizontal");
		turnVector = carFront.transform.position - carBack.transform.position;
		Debug.DrawRay (carFront.transform.position, turnVector*turnInput, Color.blue);
		Vector3 rotationForce = turnVector * turnInput;

		float tiltAngle = 30.0f * turnInput;
		float smooth = 2f;
		Quaternion target = Quaternion.Euler (0, 0, -1*tiltAngle);
		Transform objectTransform = GetComponent<Transform> ();
		objectTransform.rotation = Quaternion.Slerp (objectTransform.rotation,target,Time.deltaTime *smooth);
		//
		//objectTransform.Rotate (rotationForce*rotationMulu);
		//rigidBody.AddTorque (rotationForce, ForceMode.Acceleration);
	}
	void FixedUpdate(){

	}
}
