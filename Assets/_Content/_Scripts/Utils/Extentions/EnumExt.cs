using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnumExt
{
	public static T GetRandom<T>(this IEnumerable<T> elems)
	{
		if (elems.Count() == 0)
		{
			Debug.LogError("ENUM EXTENTION: Given IEnumerator is empty.");
		}
		return elems.ElementAt(new System.Random().Next(0, elems.Count()));
	}
}