using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure the sphere is a trigger!
[RequireComponent (typeof(SphereCollider))]
public class Eating : MonoBehaviour , Evolveable
{
	[System.Serializable]
	public class Config
	{
		[HideInInspector]
		[Tooltip ("The maximum range at which nutrients will be consumed. Is drawn from the sphere collider")]
		public float range;
		[Tooltip ("The trigger collider which ")]
		public SphereCollider collider;
		[Tooltip ("The organism script.")]
		public Organism organism;
		[Tooltip ("The nutrient type ingested.")]
		public int ingestType;
		[Tooltip ("The nutrient type egested.")]
		public int egestType;
		[Range (0.0f, 1.0f)]
		[Tooltip ("The chance that an offspring will evolve - change some characteristic compared to parent")]
		public float evolutionChance;

		public bool SetCollider (SphereCollider collider)
		{
			if (collider == null || !collider.isTrigger) {
				return false;
			}
			this.collider = collider;
			this.range = collider.radius;
			return true;
		}
	}

	[System.Serializable]
	public class State
	{
		/// <summary>
		/// The nutrients currently inside this unit.
		/// </summary>
		public Queue<Nutrient> inStomach = new Queue<Nutrient> ();
		/// <summary>
		/// The value of the top nutrient in the queue, which decreases as it's being consumed.
		/// </summary>
		public float currentNutValue = 0;
		/// <summary>
		/// The value of the other nutrients in the queue
		/// </summary>
		public float otherNutValue = 0;
	}

	public Config config;
	public State state;

	void Awake ()
	{
		state = new State ();
		// If there is a problem with the collider, there is no point in running this script
		if (!config.SetCollider (GetComponent <SphereCollider> ())) {
			Debug.Log ("Error setting collider on gameobject " + gameObject.name + " . Disabling");
			this.enabled = false;
		}
		config.organism = GetComponent <Organism> ();
		config.organism.config.eating = this;
		gameObject.name = "org " + config.ingestType + " " + config.egestType;

		if (Random.value < config.evolutionChance)
			Evolve ();
	}

	void Update ()
	{
		state.currentNutValue -= config.organism.config.deathRate * Time.deltaTime;
		UpdateCurrentHP ();
		if (state.currentNutValue <= 0) {
			Egest ();
			if (state.inStomach.Count > 0) {
				state.currentNutValue = state.inStomach.Peek ().Value - state.currentNutValue;
			}
		}
	}

	void UpdateCurrentHP ()
	{
		config.organism.state.currentHP = state.otherNutValue + state.currentNutValue;
	}

	protected void OnTriggerEnter (Collider other)
	{
		// We want to add nutrients to stomach
		if (other.CompareTag (Nutrient.NutrientTag)) {
			Nutrient nutrient = other.GetComponent <Nutrient> ();
			if (nutrient != null && nutrient.Type == config.ingestType)
				Ingest (nutrient);
		}
		// We should also be detecting hunters etc
	}

	#region In/Egests

	/// <summary>
	/// Helper method to add the given nutrient to stomach.
	/// </summary>
	/// <param name="nutrient">A wild Nutrient.</param>
	public void Ingest (Nutrient nutrient)
	{
		if (nutrient == null)
			return;
		state.inStomach.Enqueue (nutrient);
		nutrient.SaveParent ();
		state.otherNutValue += nutrient.Value;
		UpdateCurrentHP ();
		nutrient.transform.parent = transform;
		nutrient.gameObject.SetActive (false);
	}

	/// <summary>
	/// Removes the nutrient from stomach.
	/// </summary>
	/// <param name="nutrient">Nutrient.</param>
	void Egest ()
	{
		if (state.inStomach.Count == 0)
			return;
		
		Nutrient nutrient = state.inStomach.Peek ();
		state.inStomach.Dequeue ();
		nutrient.transform.position = transform.position;
		nutrient.LoadParent ();
		state.otherNutValue -= nutrient.Value;
		UpdateCurrentHP ();
		nutrient.Type = config.egestType;
		nutrient.gameObject.SetActive (true);
	}

	public List<Nutrient> EgestMoreThan (float requiredValue)
	{
		List<Nutrient> egested = new List<Nutrient> ();
		while (requiredValue > 0 && state.inStomach.Count > 0) {
			Nutrient next = state.inStomach.Peek ();
			egested.Add (next);
			requiredValue -= next.Value;
			Egest ();
		}	
		return egested;
	}

	#endregion

	#region Evolveable implementation

	public void Evolve ()
	{
		switch (Random.Range (0, 3)) {
		case 0:
			config.range += config.range * 0.2f;
			config.collider.radius = config.range;
			break;
		case 1:
			config.ingestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			do {
				config.egestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			} while (config.ingestType == config.egestType);
			break;
		case 2:
			config.egestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			do {
				config.ingestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			} while (config.ingestType == config.egestType);
			break;
		}
	}

	public void Devolve ()
	{
		switch (Random.Range (0, 3)) {
		case 0:
			config.range -= (int)config.range * 0.2f;
			config.collider.radius = config.range;
			break;
		case 1:
			config.ingestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			do {
				config.egestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			} while (config.ingestType == config.egestType);
			break;
		case 2:
			config.egestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			do {
				config.ingestType = Random.Range (0, Singleton<Environment>.instance.config.nutVariants);
			} while (config.ingestType == config.egestType);
			break;
		}
	}

	#endregion

}
