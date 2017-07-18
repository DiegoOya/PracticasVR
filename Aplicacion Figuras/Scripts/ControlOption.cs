using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla la opcion que se selecciona y permite la interaccion con ella
/// </summary>
[System.Serializable]
public class ControlOption {

	public static ControlOption current;
	public int option;

	public ControlOption()
	{
		option = -1;
	}
}
