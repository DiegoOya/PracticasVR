using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Esta clase sirve para ejecutar la función de cada función en el dispositivo el cual haya pulsado el botón
que puede ser tanto el mando derecho de las vive, el izquierdo o el ratón*/
public class ButtonManager : MonoBehaviour
{
    //public GameObject mandoIzq, mandoDer;

    GameObject mandoRaton, mandoHTC, mandoIzq, mandoDer;
	public GameObject spawner;


	private void Update()
    {
        //Busca cada frame los PlayerPrefabs del control de teclado y raton y de las gafas HTC
        mandoRaton = GameObject.FindGameObjectWithTag("Raton");
        mandoHTC = GameObject.FindGameObjectWithTag("HTC");
        if (mandoHTC != null)
        {
            mandoIzq = mandoHTC.transform.GetChild(0).gameObject;
            mandoDer = mandoHTC.transform.GetChild(1).gameObject;
        }
    }

    //Si se pulsa el boton esfera se llama a la funcion que instancia una esfera en el mando correspondiente
    public void Esfera_clicked()
    {
        if (mandoHTC.GetComponent<PlayerHTC>().isLocalPlayer)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                spawner.GetComponent<SpawnerController>().EsferaClicked(mandoIzq);
            }
            else
            {
				spawner.GetComponent<SpawnerController>().EsferaClicked(mandoDer);
            }
        }else if(mandoRaton.GetComponent<PlayerControllerRaton>().isLocalPlayer)
        {
            spawner.GetComponent<SpawnerController>().EsferaClicked(mandoRaton);
        }
    }

    //Si se pulsa el boton cubo se llama a la funcion que instancia un cubo en el mando correspondiente
    public void Cubo_clicked()
    {
        if (mandoHTC.GetComponent<PlayerHTC>().isLocalPlayer)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
				spawner.GetComponent<SpawnerController>().CuboClicked(mandoIzq);
            }
            else
            {
				spawner.GetComponent<SpawnerController>().CuboClicked(mandoDer);
            }
        }else if(mandoRaton.GetComponent<PlayerControllerRaton>().isLocalPlayer)
        {
            spawner.GetComponent<SpawnerController>().CuboClicked(mandoRaton);
        }
    }

    //Si se pulsa el boton capsula se llama a la funcion que instancia una capsula en el mando correspondiente
    public void Capsula_clicked()
    {
        if (mandoHTC.GetComponent<PlayerHTC>().isLocalPlayer)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
				spawner.GetComponent<SpawnerController>().CapsulaClicked(mandoIzq);
            }
            else
            {
				spawner.GetComponent<SpawnerController>().CapsulaClicked(mandoDer);
            }
        }else if(mandoRaton.GetComponent<PlayerControllerRaton>().isLocalPlayer)
        {
            spawner.GetComponent<SpawnerController>().CapsulaClicked(mandoRaton);
        }
    }

    // Si se pulsa el boton mover se asocia el objeto al mando correspondiente
    public void MoverClickado()
    {
        if (mandoHTC.GetComponent<PlayerHTC>().isLocalPlayer)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                mandoIzq.GetComponent<SteamVR_LaserPointer>().MoverClickado();
            }
            else
            {
                mandoDer.GetComponent<SteamVR_LaserPointer>().MoverClickado();
            }
        }else if(mandoRaton.GetComponent<PlayerControllerRaton>().isLocalPlayer)
        {
            mandoRaton.GetComponent<PlayerControllerRaton>().MoverClickado();
        }
    }

    //Si se pulsa el boton color se llama a la funcion que cambia el color de las figuras en el mando correspondiente
    public void ColorClickado()
    {
        if (mandoHTC.GetComponent<PlayerHTC>().isLocalPlayer)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                mandoIzq.GetComponent<SteamVR_LaserPointer>().ColorClickado();
            }
            else
            {
                mandoDer.GetComponent<SteamVR_LaserPointer>().ColorClickado();
            }
        }else if(mandoRaton.GetComponent<PlayerControllerRaton>().isLocalPlayer)
        {
            mandoRaton.GetComponent<PlayerControllerRaton>().ColorClickado();
        }
    }
}
