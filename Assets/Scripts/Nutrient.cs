using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple nutrient, that randomizes itself on Awake
/// </summary>
public class Nutrient : MonoBehaviour
{
	public static string NutrientTag = "Nutrient";

	// This should be private, but I'm keeping it public in this demo for easier overview
	public int type;

	public int Type {
		get { return type; }
		set { type = value; }
	}


	// This should be private, but I'm keeping it public in this demo for easier overview
	public int value;

	public int Value {
		get { return value; }
		set { this.value = value; }
	}

	private Transform parent;

	public void Awake ()
	{
		// Select a random type and assign a value
		Type = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
		// Just a simple formula to create some unequality in values
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
