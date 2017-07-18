using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Esta clase se encarga de manejar instanciacion de los objetos en el entorno
/// </summary>
public class SpawnerController : NetworkBehaviour {

	GameObject figura;

	//Instanciamos una esfera cuando se clicka el botón esfera y la hacemos hija del ratón
	public void EsferaClicked(GameObject controller)
	{
        if (controller.CompareTag("Raton"))
        {
            controller.GetComponent<PlayerControllerRaton>().CmdSpawn("Esfera", controller.tag);
        }
        else
        {
            if (controller == null) return;
            controller.transform.parent.gameObject.GetComponent<PlayerHTC>().CmdSpawn("Esfera", controller.tag);
        }      
	}



    //Instanciamos un cubo cuando se clicka el botón cubo y lo hacemos hijo del ratón
    public void CuboClicked(GameObject controller)
	{
        if (controller.CompareTag("Raton"))
        {
            controller.GetComponent<PlayerControllerRaton>().CmdSpawn("Cubo",controller.tag);
        }
        else
        {
            if (controller == null) return;
            controller.transform.parent.gameObject.GetComponent<PlayerHTC>().CmdSpawn("Cubo", controller.tag);
        }
	}

	//Instanciamos una capsula cuando se clicka el botón capsula y la hacemos hija del ratón
	public void CapsulaClicked(GameObject controller)
	{
        if (controller.CompareTag("Raton"))
        {
            controller.GetComponent<PlayerControllerRaton>().CmdSpawn("Capsula", controller.tag);
        }
        else
        {
            if (controller == null) return;
            controller.transform.parent.gameObject.GetComponent<PlayerHTC>().CmdSpawn("Capsula", controller.tag);
        }
	}
}
