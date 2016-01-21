using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
//	public float speed = 90f;
//	public float turnSpeed = 5f;
	public float hoverForce = 100f;
	public float hoverHeight = 3.5f;

//	private float powerInput;
//	private float turnInput;
	private Rigidbody carRigidbody;

	private Vector3 forward;
	public GameObject carFront;
	public GameObject carBack;
	public GameObject carLeft;
	public GameObject carRight;
	public float offset = 0;

	public float proportionalHeight=0;
	public float hitDistance=0;
	public bool groundHit = false;

	void Awake () 
	{
		carRigidbody = GetComponent <Rigidbody>();
		//inputLockout = true;
		forward = carFront.transform.position - carBack.transform.position;
	}
	
	void Update () 
	{
//		powerInput = Input.GetAxis ("Vertical");
//		turnInput = Input.GetAxis ("Horizontal");
	}
	
	void FixedUpdate()
	{
		forward = carFront.transform.position - carBack.transform.position;
		//Ray ray = new Ray (transform.position, -transform.up);
		Ray ray = new Ray (transform.position, -Vector3.up);

		//http://answers.unity3d.com/questions/191343/align-to-floor-in-local-x.html
		RaycastHit frontHit;
		RaycastHit backHit;
		//offset = 0.1f;
		Vector3 pos = transform.position;
		Physics.Raycast (pos + offset * transform.forward, -transform.up, out frontHit);
		Physics.Raycast (pos - offset * transform.forward, -transform.up, out backHit);

		RaycastHit hit;
		groundHit = Physics.Raycast (ray, out hit, hoverHeight);
		Debug.DrawRay (ray.origin, ray.direction, Color.red, 0);
		if (Physics.Raycast(ray, out hit, hoverHeight))
		{
			hitDistance = hit.distance;
			proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = hit.normal * hoverForce * proportionalHeight;
			carRigidbody.AddForce(appliedHoverForce, ForceMode.Force);

		}
	}
}
