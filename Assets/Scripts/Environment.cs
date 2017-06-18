﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Environment : MonoBehaviour
{
	[Serializable]
	public class Config
	{
		// Keep an eye out, the prefab probably has a lot of these values overriden
		[Tooltip ("The organism prefab to spawn")]
		public GameObject orgPrefab;
		[Tooltip ("The nutrient prefab to spawn")]
		public GameObject nutPrefab;
		[Tooltip ("Max length, height and width to spawn things - Ideally match these directly to the terrain values")]
		public int maxX, maxY, maxZ;
		[Tooltip ("How many variants of nutrients to spawn")]
		public int nutVariants;
		[Tooltip ("How many nutrients to spawn initially")]
		public int nutsToSpawn;
		[Tooltip ("How many nutrients to spawn per second")]
		public int nutSpawnRate = 2;
	}

	public Config config;

	void Awake ()
	{
		for (int i = 0; i < config.nutsToSpawn; i++)
			SpawnNutInRange (config.maxX, config.maxY, config.maxZ);
		SpawnFirstOrganism (config.maxX, config.maxY, config.maxZ);
	}

	void SpawnFirstOrganism (int maxX, int maxY, int maxZ)
	{
		GameObject adam = Instantiate (config.orgPrefab, new Vector3 (UnityEngine.Random.value * maxX, UnityEngine.Random.value * maxY, UnityEngine.Random.value * maxZ), Quaternion.identity, transform);
		Eating adamsMouth = adam.GetComponent <Eating> ();
		for (int i = 0; i < 3; i++) {
			GameObject newNut = Instantiate (config.nutPrefab);
			adamsMouth.Ingest (newNut.GetComponent <Nutrient> ());
		}
		adam.transform.parent = null;
	}

	void SpawnNutInRange (int maxX, int maxY, int maxZ)
	{
		Instantiate (config.nutPrefab, new Vector3 (UnityEngine.Random.value * maxX, UnityEngine.Random.value * maxY, UnityEngine.Random.value * maxZ), Quaternion.identity, transform);
	}

}
