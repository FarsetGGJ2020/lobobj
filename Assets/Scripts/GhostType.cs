﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Ghost Type")]
public class GhostType : ScriptableObject
{
	public float speed;
	public float strength;
	public DamageType weakness;
	public float fireRate;
	public float[] speeds;
	public float hitWindow;
	public float capacitySize = 25F;
}