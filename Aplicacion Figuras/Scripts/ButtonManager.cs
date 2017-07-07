﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //public GameObject mandoIzq, mandoDer;

    GameObject mandoRaton, mandoHTC, mandoIzq, mandoDer;

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
                mandoIzq.GetComponent<SteamVR_LaserPointer>().Esfera_pulsado();
            }
            else
            {
                mandoDer.GetComponent<SteamVR_LaserPointer>().Esfera_pulsado();
            }
        }else if(mandoRaton != null)
        {
            mandoRaton.GetComponent<PlayerControllerRaton>().EsferaClicked();
        }
    }

    public void Cubo_clicked()
    {
        if (mandoHTC != null)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                mandoIzq.GetComponent<SteamVR_LaserPointer>().Cubo_pulsado();
            }
            else
            {
                mandoDer.GetComponent<SteamVR_LaserPointer>().Cubo_pulsado();
            }
        }else if(mandoRaton != null)
        {
            mandoRaton.GetComponent<PlayerControllerRaton>().CuboClicked();
        }
    }

    public void Capsula_clicked()
    {
        if (mandoHTC != null)
        {
            if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                mandoIzq.GetComponent<SteamVR_LaserPointer>().Capsula_pulsado();
            }
            else
            {
                mandoDer.GetComponent<SteamVR_LaserPointer>().Capsula_pulsado();
            }
        }else if(mandoRaton != null)
        {
            mandoRaton.GetComponent<PlayerControllerRaton>().CapsulaClicked();
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

    public void DerechaSlider()
    {
        if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
        {
            mandoIzq.GetComponent<SteamVR_LaserPointer>().RotarDerecha();
        }
        else
        {
            mandoDer.GetComponent<SteamVR_LaserPointer>().RotarDerecha();
        }
    }

    public void IzquierdaSlider()
    {
        if (mandoIzq.GetComponent<SteamVR_TrackedController>().triggerPressed)
        {
            mandoIzq.GetComponent<SteamVR_LaserPointer>().RotarIzquierda();
        }
        else
        {
            mandoDer.GetComponent<SteamVR_LaserPointer>().RotarIzquierda();
        }
    }
}
