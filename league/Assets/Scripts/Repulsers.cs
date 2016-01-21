using UnityEngine;
using System.Collections;

public class Repulsers : MonoBehaviour {

	public GameObject[] repulseLocations;
	public float hoverForce = 100f;
	public float hoverHeight = 2.5f;

	public bool groundHit = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		}

	void FixedUpdate(){
		foreach (GameObject repulser in repulseLocations) {
			
			Ray ray = new Ray (repulser.transform.position, -repulser.transform.up);
			Debug.DrawRay (ray.origin, ray.direction, Color.blue, 0);
			RaycastHit hit;
			groundHit = Physics.Raycast (ray, out hit, hoverHeight);
			if (groundHit) {
				Debug.DrawLine (ray.origin, hit.point, Color.green);
				Rigidbody carRigidbody = GetComponent <Rigidbody> ();
				float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
				Vector3 appliedHoverForce = hit.normal * hoverForce * proportionalHeight;
				carRigidbody.AddForceAtPosition (appliedHoverForce, repulser.transform.position, ForceMode.Acceleration);
				Debug.Log ("applied hover force:" + appliedHoverForce);
			}
		}
	}
}
