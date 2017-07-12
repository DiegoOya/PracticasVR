﻿using System.Collections;
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
        mandoRaton = GameObject.FindGameObjectWithTag("Raton");
        mandoHTC = GameObject.FindGameObjectWithTag("HTC");
        if (mandoHTC != null)
        {
            mandoIzq = mandoHTC.transform.GetChild(0).gameObject;
            mandoDer = mandoHTC.transform.GetChild(1).gameObject;
        }
    }

    public void Esfera_clicked()
    {
        if (mandoHTC != null)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                spawner.GetComponent<SpawnerController>().EsferaClicked(mandoIzq);
            }
            else
            {
				spawner.GetComponent<SpawnerController>().EsferaClicked(mandoDer);
            }
        }else if(mandoRaton != null)
        {
            spawner.GetComponent<SpawnerController>().EsferaClicked(mandoRaton);
        }
    }

    public void Cubo_clicked()
    {
        if (mandoHTC != null)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
				spawner.GetComponent<SpawnerController>().CuboClicked(mandoIzq);
            }
            else
            {
				spawner.GetComponent<SpawnerController>().CuboClicked(mandoDer);
            }
        }else if(mandoRaton != null)
        {
            spawner.GetComponent<SpawnerController>().CuboClicked(mandoRaton);
        }
    }

    public void Capsula_clicked()
    {
        if (mandoHTC != null)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
				spawner.GetComponent<SpawnerController>().CapsulaClicked(mandoIzq);
            }
            else
            {
				spawner.GetComponent<SpawnerController>().CapsulaClicked(mandoDer);
            }
        }else if(mandoRaton != null)
        {
            spawner.GetComponent<SpawnerController>().CapsulaClicked(mandoRaton);
        }
    }

    public void MoverClickado()
    {
        if (mandoHTC != null)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                mandoIzq.GetComponent<SteamVR_LaserPointer>().MoverClickado();
            }
            else
            {
                mandoDer.GetComponent<SteamVR_LaserPointer>().MoverClickado();
            }
        }else if(mandoRaton != null)
        {
            mandoRaton.GetComponent<PlayerControllerRaton>().MoverClickado();
        }
    }

    public void ColorClickado()
    {
        if (mandoHTC != null)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                mandoIzq.GetComponent<SteamVR_LaserPointer>().ColorClickado();
            }
            else
            {
                mandoDer.GetComponent<SteamVR_LaserPointer>().ColorClickado();
            }
        }else if(mandoRaton != null)
        {
            mandoRaton.GetComponent<PlayerControllerRaton>().ColorClickado();
        }
    }

    //public void DerechaSlider()
    //{
    //    if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
    //    {
    //        mandoIzq.GetComponent<SteamVR_LaserPointer>().RotarDerecha();
    //    }
    //    else
    //    {
    //        mandoDer.GetComponent<SteamVR_LaserPointer>().RotarDerecha();
    //    }
    //}

    //public void IzquierdaSlider()
    //{
    //    if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
    //    {
    //        mandoIzq.GetComponent<SteamVR_LaserPointer>().RotarIzquierda();
    //    }
    //    else
    //    {
    //        mandoDer.GetComponent<SteamVR_LaserPointer>().RotarIzquierda();
    //    }
    //}
}
