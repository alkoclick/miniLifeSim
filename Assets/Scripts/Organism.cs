using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent (typeof(Movement))]
public class Organism : MonoBehaviour
{

	[Serializable]
	public class Config
	{
		public float startingHealth = 10.0f;
		[Tooltip ("How much life the organism loses per sec")]
		public float deathRate = 0.5f;
		public Eating eating;
	}

	public Config config;

	[Serializable]
	public class State
	{
		public float currentHP = 0;
	}

	public State state = new State ();

	public void Update ()
	{
		// Reproduction
		if (state.currentHP > 2 * config.startingHealth) {
			Reproduce ();
		} else if (state.currentHP <= 0) {
			Die ();
		}
	}

	public void Reproduce ()
	{
		GameObject child = Instantiate (gameObject, transform.position, transform.rotation);
		Eating childEating = child.GetComponent <Eating> ();
		foreach (Nutrient nut in config.eating.EgestMoreThan (config.startingHealth)) {
			childEating.Ingest (nut);
		}
	}

	public void Die ()
	{
		Destroy (gameObject);
	}

	// Evolution happens on childbirth
}
