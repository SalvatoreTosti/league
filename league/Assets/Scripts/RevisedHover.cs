using UnityEngine;
using System.Collections;

public class RevisedHover : MonoBehaviour {
	public float hoverForce = 100f;
	public float hoverHeight = 3.5f;
	private Rigidbody carRigidbody;
	
	public float proportionalHeight=0;

	public GameObject[] testPoints;
	public GameObject frontLeft;
	public GameObject frontRight;
	public GameObject backLeft;
	public GameObject backRight;

	public Vector3 visibleAvgNormal = new Vector3();
	public Quaternion visibleQuat = new Quaternion();
	void Awake () 
	{
		carRigidbody = GetComponent <Rigidbody>();
	}
	
	void Update () 
	{
	}
	
	void FixedUpdate()
	{
		Vector3 combinedNormals = new Vector3 ();
		Vector3 combinedHitPoints = new Vector3 ();
		foreach (GameObject point in testPoints) {
			Ray ray = new Ray (point.transform.position, -Vector3.up);
			RaycastHit hit;
			//Physics.Raycast (transform.position, -transform.up, out hit);
			bool groundHit = Physics.Raycast (ray, out hit, hoverHeight);
			combinedNormals += hit.normal;
			combinedHitPoints += hit.point;

			if (groundHit) {
				Debug.DrawRay (ray.origin, ray.direction, Color.red, 0);
			}
		}
		visibleAvgNormal = combinedNormals / 4;

		RaycastHit frontLeftHit = getDownHit (frontLeft);
		RaycastHit backLeftHit = getDownHit (backLeft);
		RaycastHit frontRightHit = getDownHit (frontRight);
		RaycastHit backRightHit = getDownHit (backRight);
		Vector3 left = frontLeftHit.point - backLeftHit.point;
		Vector3 right = frontRightHit.point - frontRightHit.point;

		Vector3 avg = left + right / 2;
		Debug.DrawRay (transform.position, avg, Color.green);
		if (avg == new Vector3 (0, 0, 0)) {
			print ("ok");
		} else {
			transform.forward = avg;
		}

	}

	private RaycastHit getDownHit(GameObject obj){
		Ray ray = new Ray (obj.transform.position, -Vector3.up);
		RaycastHit hit;
		Physics.Raycast(ray,out hit);
		return hit;
	}
}
