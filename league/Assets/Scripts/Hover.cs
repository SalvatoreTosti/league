using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour
{
	public float hoverForce = 100f;
	public float hoverHeight = 3.5f;

	private Rigidbody carRigidbody;

	private Vector3 forward;
	public GameObject carFront;
	public GameObject carBack;
	public GameObject carLeft;
	public GameObject carRight;
	public float offset = 0;

	public float proportionalHeight = 0;
	public float hitDistance = 0;
	public bool groundHit = false;

	public GameObject frontLeft;
	public GameObject frontRight;
	public GameObject backLeft;
	public GameObject backRight;

	void Awake ()
	{
		carRigidbody = GetComponent <Rigidbody> ();
		forward = carFront.transform.position - carBack.transform.position;
	}

	
	void FixedUpdate ()
	{
		forward = carFront.transform.position - carBack.transform.position;
		Ray ray = new Ray (transform.position, -Vector3.up);

		//http://answers.unity3d.com/questions/191343/align-to-floor-in-local-x.html
//		RaycastHit frontHit;
//		RaycastHit backHit;
		//offset = 0.1f;
//		Physics.Raycast (pos + offset * transform.forward, -transform.up, out frontHit);
//		Physics.Raycast (pos - offset * transform.forward, -transform.up, out backHit);

		RaycastHit hit;
		groundHit = Physics.Raycast (ray, out hit, hoverHeight);
		Debug.DrawRay (ray.origin, ray.direction, Color.red, 0);
		if (Physics.Raycast (ray, out hit, hoverHeight)) {
			hitDistance = hit.distance;
			proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = hit.normal * hoverForce * proportionalHeight;
			carRigidbody.AddForce (appliedHoverForce, ForceMode.Force);

		}

		AdjustForward ();
		UpdateVisibleHitAverage ();
	}

	private RaycastHit getDownHit (GameObject obj)
	{
		Ray ray = new Ray (obj.transform.position, -Vector3.up);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		return hit;
	}

	//Adjusts forward direction based on raycasts to ground
	private void AdjustForward ()
	{
		RaycastHit frontLeftHit = getDownHit (frontLeft);
		RaycastHit backLeftHit = getDownHit (backLeft);
		RaycastHit frontRightHit = getDownHit (frontRight);
		RaycastHit backRightHit = getDownHit (backRight);

		Vector3 left; 
		if (frontLeftHit.point == Vector3.zero ||
		    backLeftHit.point == Vector3.zero) {
			left = Vector3.zero;
		} else {
			left = frontLeftHit.point - backLeftHit.point;
		}

		Vector3 right;
		if (frontRightHit.point == Vector3.zero ||
		    backRightHit.point == Vector3.zero) {
			right = Vector3.zero;
		} else {
			right = frontRightHit.point - frontRightHit.point;
		}

		Vector3 avg = left + right / 2;
		if (avg == Vector3.zero) {
			print ("Forward direction is zero vector.");
		} else {
			transform.forward = avg;
		}	
	}

	private void UpdateVisibleHitAverage ()
	{
		Vector3 combinedNormals = new Vector3 ();
		Vector3 combinedHitPoints = new Vector3 ();
		GameObject[] testPoints = { frontLeft, frontRight, backLeft, backRight };
		foreach (GameObject point in testPoints) {
			Ray ray = new Ray (point.transform.position, -Vector3.up);
			RaycastHit hit;
			bool groundHit = Physics.Raycast (ray, out hit, hoverHeight);
			combinedNormals += hit.normal;
			combinedHitPoints += hit.point;

			if (groundHit) {
				Debug.DrawRay (ray.origin, ray.direction, Color.red, 0);
			}
		}
	}
}