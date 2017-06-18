using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient : MonoBehaviour
{
	public static string NutrientTag = "Nutrient";

	public int type;

	public int Type {
		get { return type; }
		set { type = value; }
	}

	public int value;

	public int Value {
		get { return value; }
		set { this.value = value; }
	}

	private Transform parent;

	public void Awake ()
	{
		Type = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
		Value = Type * 2 + 3;
	}

	/// <summary>
	/// Saves the transform parent. Useful before you change that parent
	/// </summary>
	public void SaveParent ()
	{
		this.parent = transform.parent;
	}

	/// <summary>
	/// Loads the previously saved transform parent.
	/// </summary>
	public void LoadParent ()
	{
		transform.parent = parent;
	}
}
