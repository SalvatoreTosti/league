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
		normalsLerpTime = 1f;
		//currentNormalsLerpTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		updateForward ();
		powerInput = Input.GetAxis ("Vertical");
		Debug.DrawRay (carFront.transform.position, forwardVector*powerInput, Color.blue);
		updateRotation ();
		turnInput = Input.GetAxis ("Horizontal");
		Debug.DrawRay (carFront.transform.position, turnVector*turnInput, Color.green);
		Debug.DrawRay (carFront.transform.position, transform.up, Color.cyan);
		Debug.DrawRay (carBack.transform.position, transform.up, Color.cyan);
		Debug.DrawRay (carLeft.transform.position, transform.up, Color.cyan);
		Debug.DrawRay (carRight.transform.position, transform.up, Color.cyan);



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
		Vector3 forwardForce = Vector3.forward * powerInput * powerMultipler;
		carRigidbody.AddForce (forwardForce, ForceMode.Acceleration);

		Vector3 turnForce = transform.up * turnInput * powerMultipler;

		carRigidbody.AddTorque (turnForce, ForceMode.Acceleration);
		
		
		RaycastHit hit;
		Ray ray = new Ray (decorativeFront.transform.position, Vector3.down);
		Physics.Raycast (ray, out hit, 9000f);


//		print (transform.up);
//		print (hit.normal);
//		print (currentNormalsLerpTime);
//		print (transform.up == hit.normal);

		//vectorsClose (transform.up, hit.normal,true);

//		if (!vectorsClose (transform.up, hit.normal,true)) {
//			currentNormalsLerpTime += Time.deltaTime;
//			float perc = currentNormalsLerpTime / normalsLerpTime;
//			transform.up = Vector3.Lerp (transform.up, hit.normal, perc);
//		} else {
//			currentNormalsLerpTime = 0;
//		}

		//if (currentNormalsLerpTime > normalsLerpTime) {
		//	currentNormalsLerpTime = normalsLerpTime;
		//}



		//transform.up = Vector3.Lerp(transform.up, hit.normal, Time.deltaTime*30);

	
//		float tiltAngle = 30.0f * turnInput;
//		float smooth = 2.0f;
//		Quaternion target = Quaternion.Euler (0, 0, tiltAngle);
//		Transform objectTransform = GetComponent<Transform> ();
//		objectTransform.rotation = Quaternion.Slerp (objectTransform.rotation,target,Time.deltaTime *smooth);
	}

	private bool vectorsClose(Vector3 vectorA, Vector3 vectorB,bool debug){
		float percDiffAllowed = 0.05f;
		if ((vectorA - vectorB).sqrMagnitude <= (vectorA * percDiffAllowed).sqrMagnitude) {
			if(debug){Debug.Log("Less than 5% difference.");}
			return true;
		} else {
			if(debug){Debug.Log("More than 5% difference.");}
			return false;
		}
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
