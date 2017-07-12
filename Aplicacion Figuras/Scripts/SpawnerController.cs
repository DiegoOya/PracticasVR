using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnerController : NetworkBehaviour {

	GameObject figura;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Instanciamos una esfera cuando se clicka el botón esfera y la hacemos hija del ratón
	public void EsferaClicked(GameObject controller)
	{
		figura = (GameObject)Instantiate(Resources.Load("Esfera"));
		NetworkServer.Spawn(figura);
		figura.transform.SetParent(controller.transform);
	}

	//Instanciamos un cubo cuando se clicka el botón cubo y lo hacemos hijo del ratón
	public void CuboClicked(GameObject controller)
	{
		figura = (GameObject)Instantiate(Resources.Load("Cubo"));
		NetworkServer.Spawn(figura);
		figura.transform.SetParent(controller.transform);
	}

	//Instanciamos una capsula cuando se clicka el botón capsula y la hacemos hija del ratón
	public void CapsulaClicked(GameObject controller)
	{
		figura = (GameObject)Instantiate(Resources.Load("Capsula"));
		NetworkServer.Spawn(figura);
		figura.transform.SetParent(controller.transform);
	}
}
