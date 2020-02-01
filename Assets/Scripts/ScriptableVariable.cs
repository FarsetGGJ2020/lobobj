using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableVariable<T> : ScriptableObject
{
	public T value;

	public static implicit operator T(ScriptableVariable<T> obj)
	{
		return obj.value;
	}
}
