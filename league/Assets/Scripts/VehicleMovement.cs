using UnityEngine;
using System.Collections;

public class VehicleMovement : MonoBehaviour {
	//https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/hover-car-physics
	
	public float speed = 10.0f;
	public float turnSpeed = 30.0f;
	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;
	public float brakeSpeed = 30.0f;
	public float offset = 0.1f;
	public bool inputLockout;
	public Vector3 current_movement; //not to be used, just for looking in editor.
	public Vector3 position; //for looking in editor
	public GameObject carFront;
	public GameObject carBack; 
	
	private float powerInput;
	private float turnInput;
	public bool brakeInput = false;
	private Rigidbody carRigidbody;
	public Vector3 forward;
	
	public float lerpSpeed;
	public float rotSpeed = 30.0f;
	
	void Awake ()
	{
		carRigidbody = GetComponent<Rigidbody> ();
		current_movement = carRigidbody.velocity;
		position = carRigidbody.position;
		inputLockout = true;
		forward = carFront.transform.position - carBack.transform.position;
		
	}
	
	
	void Update ()
	{
		if (inputLockout) { 
			powerInput = 0;
			turnInput = 0;
		} else {
			powerInput = -1 * Input.GetAxis ("Vertical");
			turnInput = Input.GetAxis ("Horizontal");
			
			if (Input.GetButtonDown ("Jump")) {
				print("Jumping!");
				brakeInput = true;
			}
			if (Input.GetButtonUp ("Jump")) {
				brakeInput = false;
			}
			
		}
	}
	
	
	
	void FixedUpdate ()
	{
		position = carRigidbody.position;
		forward = carFront.transform.position - carBack.transform.position;
		//forward.Normalize();
		Ray ray = new Ray (transform.position, -transform.up);
		//http://answers.unity3d.com/questions/191343/align-to-floor-in-local-x.html
		RaycastHit frontHit;
		RaycastHit backHit;
		//offset = 0.1f;
		Vector3 pos = transform.position;
		Physics.Raycast (pos + offset * transform.forward, -Vector3.up, out frontHit);
		Physics.Raycast (pos - offset * transform.forward, -Vector3.up, out backHit);
		transform.forward = frontHit.point - backHit.point;
		//if(fraction < 1){
		//	fraction += Time.deltaTime;
		//transform.forward = Vector3.Lerp (transform.forward, frontHit.point - backHit.point, Time.deltaTime);
		//http://answers.unity3d.com/questions/730755/lerp-between-two-vectors.html
		//Debug.DrawRay (backHit);
		
		
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, hoverHeight)) { //if within distance hoverHeight
			//rigidbody.position = new Vector3 (rigidbody.position.x, hit.point.y + hoverHeight, rigidbody.position.z);
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			carRigidbody.AddForce (appliedHoverForce, ForceMode.Acceleration);
		} else {
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = -1 * Vector3.up * proportionalHeight *hoverForce;
			carRigidbody.AddForce (appliedHoverForce, ForceMode.Acceleration);
			
		}
		if (brakeInput) {
			//Thanks to Patrick for his help with the braking code!
			Vector3 vel = transform.InverseTransformDirection (carRigidbody.velocity.normalized) * -1 * brakeSpeed;
			vel.y = 0;
			carRigidbody.AddRelativeForce (vel, ForceMode.VelocityChange);
		}
		carRigidbody.AddRelativeForce (0f, 0f, powerInput * speed, ForceMode.VelocityChange);
		carRigidbody.AddRelativeTorque (0f, turnInput * turnSpeed, 0f, ForceMode.VelocityChange);
		carRigidbody.AddRelativeTorque (0f, 0f, turnInput * rotSpeed);	///This applies a rotation to the craft so it banks around turns.
		
		
	}

}
