//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
//using UnityEngine.Networking;

public struct PointerEventArgs
{
    public uint controllerIndex;
    public uint flags;
    public float distance;
    public Transform target;
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);


public class SteamVR_LaserPointer : MonoBehaviour
{
    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;
    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;

    GameObject canvasFigura;
    Transform previousContact = null;
    GameObject seleccionada, esfera, cubo, capsula;
    SteamVR_TrackedController controller;
    RaycastHit hit;
    int cont = 0;
    public bool update = false;

    /*Se crea el aspecto físico del láser*/
    void Start ()
    {
        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
		holder.transform.localRotation = Quaternion.identity;

		pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
		pointer.transform.localRotation = Quaternion.identity;
		BoxCollider collider = pointer.GetComponent<BoxCollider>();
        pointer.name = "Laser";
        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if (collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
        canvasFigura = GameObject.FindGameObjectWithTag("canvasOpciones");
        update = true;
	}
    
    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    // Update is called once per frame
    public void Actualizar()
    {
        if (!isActive)
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        float dist = 100f;

        controller = GetComponent<SteamVR_TrackedController>();

        //Lanzamos un rayo para detectar colliders
        Ray raycast = new Ray(transform.position, transform.forward);
        bool bHit = Physics.Raycast(raycast, out hit, dist);
        //Si la nueva posicion donde apunta el laser no es un objeto con un collider se llaman a los métodos 
        //de "salida" asignados al evento pointerOut
        if (previousContact && previousContact != hit.transform)
        {
            PointerEventArgs args = new PointerEventArgs();
            if (controller != null)
            {
                args.controllerIndex = controller.controllerIndex;
            }
            args.distance = 0f;
            args.flags = 0;
            args.target = previousContact;
            OnPointerOut(args);
            previousContact = null;
        }
        //Si el láser choca contra un collider se llaman a los métodos de "entrada" asignados
        //al evento pointerIn
        if (bHit && previousContact != hit.transform)
        {
            PointerEventArgs argsIn = new PointerEventArgs();
            if (controller != null)
            {
                argsIn.controllerIndex = controller.controllerIndex;
            }
            argsIn.distance = hit.distance;
            argsIn.flags = 0;
            argsIn.target = hit.transform;
            OnPointerIn(argsIn);
            previousContact = hit.transform;
        }
        if (!bHit)
        {
            previousContact = null;
        }
        //if (bHit && hit.distance < 100f)
        //{
        //    dist = hit.distance;
        //}

        if (controller != null && controller.triggerPressed)
        {
            pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
        }
        else
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
        }
        pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);

        //comprobar que pulsamos una figura para asi mostrar el menú de figura
        if (bHit)
        {
            if (hit.collider.gameObject.CompareTag("Figura"))
            {
                if (controller.padPressed)
                {
                    if (!canvasFigura.activeInHierarchy) canvasFigura.SetActive(true);
                    seleccionada = hit.collider.gameObject;
                    PosicionCanvas(seleccionada);
                }
            }
        }
    }

    //Si se pulsa el botón figura se instancia una esfera hija del mando
    public void Esfera_pulsado()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Figura")
            {
                cont++;
            }
        }
        if (cont == 0)
        {
            esfera = (GameObject)Instantiate(Resources.Load("Esfera"));
            NetworkServer.Spawn(esfera);
            //esfera = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Esfera.prefab", typeof(GameObject)));
            esfera.GetComponent<CompFigura>().enabled = true;
            esfera.GetComponent<Rigidbody>().isKinematic = true;
            esfera.transform.SetParent(gameObject.transform);
        }
        cont = 0;
    }

    //Si se pulsa el botón cubo se instancia un cubo hija del mando
    public void Cubo_pulsado()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Figura")
            {
                cont++;
            }
        }
        if (cont == 0)
        {
            cubo = (GameObject)Instantiate(Resources.Load("Cubo"));
            NetworkServer.Spawn(cubo);
            //cubo = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Cubo.prefab", typeof(GameObject)));
            cubo.GetComponent<CompFigura>().enabled = true;
            cubo.GetComponent<Rigidbody>().isKinematic = true;
            cubo.transform.SetParent(gameObject.transform);
        }
        cont = 0;
    }

    //Si se pulsa el botón capsula se instancia una capsula hija del mando
    public void Capsula_pulsado()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Figura")
            {
                cont++;
            }
        }
        if (cont == 0)
        {
            capsula = (GameObject)Instantiate(Resources.Load("Capsula"));
            NetworkServer.Spawn(capsula);
            //capsula = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Capsula.prefab", typeof(GameObject)));
            capsula.GetComponent<CompFigura>().enabled = true;
            capsula.GetComponent<Rigidbody>().isKinematic = true;
            capsula.transform.SetParent(gameObject.transform);
        }
        cont = 0;
    }


public void MoverClickado()
    {
        seleccionada.transform.SetParent(this.gameObject.transform);
    }

    public void ColorClickado()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value);
        seleccionada.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
    }

    //public void RotarDerecha()
    //{
    //    if (hit.collider.gameObject.GetComponent<Slider>() != null)
    //    {
    //        seleccionada.transform.Rotate(0, hit.collider.gameObject.GetComponent<Slider>().value * 360,0);
    //    }
    //}

    //public void RotarIzquierda()
    //{
    //    if (hit.collider.gameObject.GetComponent<Slider>() != null)
    //    {
    //        seleccionada.transform.Rotate(0, -hit.collider.gameObject.GetComponent<Slider>().value * 360, 0);
    //    }
    //}

     //movemos el canvas del menú de figuras a la posición en la que se encuentre la figura seleccionada
    void PosicionCanvas(GameObject tocada)
    {
        if (transform.parent.gameObject.transform.position == new Vector3(0, 0, 15)) //alzado
        {
            canvasFigura.transform.LookAt(transform.parent.gameObject.transform);
            canvasFigura.transform.position = new Vector3(tocada.transform.position.x + 2, tocada.transform.position.y + 2, tocada.transform.position.z + 2);
        }
        if (transform.parent.gameObject.transform.position == new Vector3(15, 0, 0)) //perfil
        {
            canvasFigura.transform.LookAt(transform.parent.gameObject.transform);
            canvasFigura.transform.position = new Vector3(tocada.transform.position.x + 2, tocada.transform.position.y + 2, tocada.transform.position.z + 2);
        }
        if (transform.parent.gameObject.transform.position == new Vector3(0, 15, 0)) //planta
        {
            canvasFigura.transform.LookAt(transform.parent.gameObject.transform);
            canvasFigura.transform.position = new Vector3(tocada.transform.position.x + 2, tocada.transform.position.y + 2, tocada.transform.position.z + 2);
        }
    }
}
