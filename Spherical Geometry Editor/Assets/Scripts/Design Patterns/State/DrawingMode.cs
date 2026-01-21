using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrawingMode : ScriptableObject
{
	private int controllPoints;
	protected GameObject prefab;

	public int ControllPoints
	{
		get { return controllPoints; }
		protected set { controllPoints = value; }
	}

	public void SetUp(GameObject preafab, int controllPoints) 
	{ 
		this.prefab = preafab;
		this.controllPoints = controllPoints;
	}

	public abstract ICommand Draw(GameObject[] controllPoints);

}
