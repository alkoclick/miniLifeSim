using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent (typeof(ThirdPersonCharacter))]
public class MovementChar : MonoBehaviour
{
	// the navmesh agent required for the path finding
	public NavMeshAgent agent { get; private set; }
	// the character we are controlling
	public ThirdPersonCharacter character { get; private set; }


	private void Start ()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<NavMeshAgent> ();
		character = GetComponent<ThirdPersonCharacter> ();

		agent.updateRotation = false;
		agent.updatePosition = true;
	}

	void FixedUpdate ()
	{
		// We want to keep them always moving - once in the end of the path they should get a new dest
		if (agent.remainingDistance <= agent.stoppingDistance && (agent.pathStatus == NavMeshPathStatus.PathComplete || agent.pathStatus == NavMeshPathStatus.PathInvalid))
			SelectRandomDestination ();
			
		character.Move (agent.desiredVelocity, false, false);
	}

	void SelectRandomDestination ()
	{
		Environment.Config conf = Singleton<Environment>.instance.config;
		// This also does path recalculation for us
		agent.destination = new Vector3 (UnityEngine.Random.value * conf.maxX, UnityEngine.Random.value * conf.maxY, UnityEngine.Random.value * conf.maxZ);
	}
}

