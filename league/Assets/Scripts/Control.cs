using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	public float speed = 90f;
	public float turnSpeed = 5f;
	//public float hoverForce = 65f;
	//public float hoverHeight = 3.5f;
	
	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;

	private Vector3 forward;
	public GameObject carFront;
	public GameObject carBack;

	// Use this for initialization
	void Start () {
		carRigidbody = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		powerInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
	}

	void FixedUpdate(){
		carRigidbody.AddRelativeForce(0f, powerInput * speed, 0f);
		carRigidbody.AddRelativeTorque(turnInput * turnSpeed, 0f, 0f);
	}
}
