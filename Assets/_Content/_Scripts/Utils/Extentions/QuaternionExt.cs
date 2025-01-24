using UnityEngine;

public static class QuaternionExt
{
	public static Quaternion QuaternionSmoothDamp(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime)
	{
		// Assertions
		if (Time.deltaTime == 0) return current;
		if (smoothTime == 0) return target;

		Vector3 c = current.eulerAngles;
		Vector3 t = target.eulerAngles;

		return Quaternion.Euler(
			Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
			Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
			Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
		);
	}
}