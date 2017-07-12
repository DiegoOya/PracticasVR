using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncController : NetworkBehaviour {
	
	[SyncVar(hook = "OnPosChange")]
	Vector3 pos;

	[SyncVar(hook = "OnColorChange")]
	Color color;

	// Use this for initialization
	void Awake () {
		pos = transform.position;
		color = GetComponent<MeshRenderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		color = GetComponent<MeshRenderer>().material.color;
	}

	void OnPosChange(Vector3 pos)
	{
		transform.position = pos;
	}

	void OnColorChange(Color color)
	{
		GetComponent<MeshRenderer>().material.color = color;
	}
}
