using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
	public class State
	{
		public NavMeshAgent nma;
	}

	private State state;

	void Awake ()
	{
		state = new State ();
		state.nma = GetComponent <NavMeshAgent> ();
		SelectRandomDestination ();
	}

	void FixedUpdate ()
	{
		// We want to keep them always moving - once in the end of the path they should get a new dest
		if (state.nma.remainingDistance <= state.nma.stoppingDistance && (state.nma.pathStatus == NavMeshPathStatus.PathComplete || state.nma.pathStatus == NavMeshPathStatus.PathInvalid))
			SelectRandomDestination ();
	}

	void SelectRandomDestination ()
	{
		Environment.Config conf = Singleton<Environment>.instance.config;
		// This also does path recalculation for us
		state.nma.destination = new Vector3 (UnityEngine.Random.value * conf.maxX, UnityEngine.Random.value * conf.maxY, UnityEngine.Random.value * conf.maxZ);
	}
}
