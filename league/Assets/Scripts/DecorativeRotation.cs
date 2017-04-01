using UnityEngine;
using System.Collections;

public class DecorativeRotation : MonoBehaviour
{
	public Rigidbody rigidBody;
	private float turnInput;
	public float tiltSpeed = 30.0f;

	private float initialThreshold = 0.1f;
	private float maxThreshold = 0.2f;

	void Update ()
	{
		if (Input.GetAxisRaw ("Horizontal") == 0) {
			RotateTowardLocalZero ();
			return;
		}

		float tiltAngle = tiltSpeed * Input.GetAxisRaw ("Horizontal");
		float localRotationZ = transform.localRotation.z;
		float absoluteLocalRotationZ = Mathf.Abs (localRotationZ);

		if (absoluteLocalRotationZ >= 0 && absoluteLocalRotationZ < initialThreshold) {
			transform.Rotate (0, 0, -1 * tiltAngle * Time.deltaTime);
		} else if (absoluteLocalRotationZ < maxThreshold) {
			//smooths out end of rotation
			float rotationPercentage = (maxThreshold - absoluteLocalRotationZ) / initialThreshold;
			Debug.Log ("rotation Percentage: "+ rotationPercentage);
			transform.Rotate (0, 0, -1 * tiltAngle * Time.deltaTime * rotationPercentage);
		}
	}

	private void RotateTowardLocalZero ()
	{
		float smoothTiltSpeed = 2.0f;
		transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.Euler (0, 0, 0), Time.deltaTime * smoothTiltSpeed);
	}
}
