using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public GameObject carFront;
	public GameObject carBack;
	public GameObject carLeft;
	public GameObject carRight;

	public GameObject decorativeFront;

	public Vector3 forwardVector;
	public Vector3 turnVector;

	public float powerInput;
	public float powerMultipler = 5.0f;
	public float turnInput;
	public float turnMultipler = 1.0f;

	private float brakeInput;
	private float normalsLerpTime;
	private float currentNormalsLerpTime;
	private Vector3 lerpVectorStart;
	private Vector3 lerpVectorEnd;


	// Use this for initialization
	void Start () {
		forwardVector = carFront.transform.position - carBack.transform.position;
		forwardVector.Normalize();
		turnVector = carLeft.transform.position - carRight.transform.position;
		turnVector.Normalize();
		powerInput = 0;
		turnInput = 0f;
		brakeInput = 0;
	}
	
	// Update is called once per frame
	void Update () {
		updateForward ();
		powerInput = Input.GetAxis ("Vertical");
		Debug.DrawRay (carFront.transform.position, forwardVector*powerInput, Color.blue);
		updateRotation ();
		turnInput = Input.GetAxis ("Horizontal");
		Debug.DrawRay (carFront.transform.position, turnVector*turnInput, Color.green);
		Debug.DrawRay (carFront.transform.position, Vector3.up, Color.cyan);
		Debug.DrawRay (carBack.transform.position, Vector3.up, Color.cyan);
		Debug.DrawRay (carLeft.transform.position, Vector3.up, Color.cyan);
		Debug.DrawRay (carRight.transform.position, Vector3.up, Color.cyan);



//		if (Input.GetButtonDown ("Jump")) {
//			print("Jumping!");
//			brakeInput = true;
//		}
//		if (Input.GetButtonUp ("Jump")) {
//			brakeInput = false;
//		}
		//Debug.DrawRay (ray.origin, ray.direction, Color.red, 0);
	}
	void FixedUpdate(){
		Rigidbody carRigidbody = GetComponent<Rigidbody> ();
		Vector3 forwardForce = forwardVector * powerInput * powerMultipler;
		carRigidbody.AddForce (forwardForce, ForceMode.Acceleration);

		Vector3 turnForce = Vector3.up * turnInput * powerMultipler;
		print (turnForce);
		carRigidbody.AddTorque (turnForce, ForceMode.Acceleration);
		
		
		RaycastHit hit;
		Ray ray = new Ray (decorativeFront.transform.position, Vector3.down);
		Physics.Raycast (ray, out hit, 10f);

		
//		currentNormalsLerpTime += Time.deltaTime;
//		if (currentNormalsLerpTime > normalsLerpTime) {
//			currentNormalsLerpTime = normalsLerpTime;
//		}
//		float perc = currentNormalsLerpTime / normalsLerpTime;
//		transform.up = Vector3.Lerp(transform.up, hit.normal, perc);

		//transform.up = Vector3.Lerp(transform.up, hit.normal, Time.deltaTime*30);

	
//		float tiltAngle = 30.0f * turnInput;
//		float smooth = 2.0f;
//		Quaternion target = Quaternion.Euler (0, 0, tiltAngle);
//		Transform objectTransform = GetComponent<Transform> ();
//		objectTransform.rotation = Quaternion.Slerp (objectTransform.rotation,target,Time.deltaTime *smooth);
	}

	private void updateForward(){
		forwardVector = carFront.transform.position - carBack.transform.position;
		forwardVector.Normalize();
	}
	private void updateRotation(){
		turnVector = carLeft.transform.position - carRight.transform.position;
		turnVector.Normalize();
	}

}
