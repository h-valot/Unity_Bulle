using System;
using System.Collections;
using UnityEngine;

public static class CoroutineExt
{
	public static IEnumerator Delay(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action?.Invoke();
	}
}