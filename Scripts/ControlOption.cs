using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlOption {

	public static ControlOption current;
	public int option;

	public ControlOption()
	{
		option = -1;
	}
}
