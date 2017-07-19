using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

/*Script que controla los controles del jugador "Ratón"*/
public class PlayerControllerRaton : NetworkBehaviour
{
    public float range = 50f;                                     // Distance in Unity units over which the player can fire

    public delegate void puntero();
    public static puntero pointer;
    //public event PointerEventHandler PointerIn;
    //public event PointerEventHandler PointerOut;
    public static float xRotate, yRotate;

    private Camera fpsCam;                                        // Holds a reference to the first person camera
    Quaternion targetRotation;
    GameObject figura;
    RaycastHit hit;
    private bool tieneHijo;
    Transform previousContact = null;
    private GameObject seleccionada;
    private GameObject canvasFigura;

    void Start()
    {
        // Get and store a reference to our Camera by searching this GameObject and its parents
        fpsCam = GetComponentInChildren<Camera>();
        canvasFigura = GameObject.FindGameObjectWithTag("canvasOpciones");
        Cursor.lockState = CursorLockMode.Locked;
        if (!isLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            return;
        }
    }

    void Update()
    {
        if (!isLocalPlayer) //Esto asegura que el código sólo se ejecute en la máquina local que ejecuta la aplicación
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            return;
        }
        /// Comprobar si es local player en Networking
        bool bHit; 
        MoveCamera();

        //GetComponent<Transform>().LookAt(Input.mousePosition);

        // Create a vector at the center of our camera's viewport
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        /*Cancelar todas las suscripciones al evento*/		
        PlayerControllerRaton.pointer -= onPointerIn;
        PlayerControllerRaton.pointer -= onPointerOut;
        PlayerControllerRaton.pointer -= onSubmit;

        foreach (Transform child in transform) //Si el ratón ya tiene un hijo, no podrá coger otra figura
        {
            if (child.gameObject.tag == "Figura") tieneHijo = true;
            else tieneHijo = false;
        }
        bHit = Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range); // Check if our raycast has hit anything
        if (!tieneHijo)
        {
            if (bHit)
            {
                pointer += onPointerIn;
                previousContact = hit.transform;
                pointer();
                if (Input.GetMouseButtonDown(0))
                {
                    pointer += onSubmit;
                    pointer();
                }
                if (Input.GetMouseButton(0))
                {
                    //Comprobamos que el collider con el que choca el raycast es una figura, si es así se procesa
                    if (hit.collider.gameObject.tag == "Figura") 
                    {
                        figura = hit.collider.gameObject;
                        if (figura.transform.parent == null)
                        {
                            figura.GetComponent<Rigidbody>().isKinematic = true;
                            figura.transform.SetParent(this.transform);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) //si dejamos de pulsar el botón izquierdo del ratón, se deja caer la figura
        {
			if(transform.childCount > 1) figura = GetComponentInChildren<CompFigura>().gameObject;
            if (figura != null)
            {
                if (figura.transform.parent != null)
                {
                    if (figura.transform.parent.gameObject == this.gameObject)
                    {
						//figura.GetComponent<Rigidbody>().isKinematic = false;
						CmdNotParent(figura);
                    }
                }
            }
        }

        if (bHit)
        {
            if (Input.GetMouseButtonDown(1)) //Si pulsamos botón derecho sobre una figura aparece su menú para poder mover o cambiar color
            {
                if (hit.collider.gameObject.tag == "Figura")
                {
                    if (!canvasFigura.activeInHierarchy) canvasFigura.SetActive(true);
                    seleccionada = hit.collider.gameObject;
                    PosicionCanvas(seleccionada);
                }
            }
        }

        if (previousContact && previousContact != hit.transform)
        {
            pointer += onPointerOut;
            previousContact = null;
            pointer();
        }
    }

	//Si es un botón con el que choca el raycast lo seleccionamos
	void onPointerIn()
    {
        Button btn = hit.collider.gameObject.GetComponent<Button>();
        if (btn != null)
        {
            btn.Select();
        }
    }

    //Si el raycast sale del botón lo se desselecciona
    void onPointerOut()
    {
        if (hit.collider != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    //Ejecutamos la acción asignada al botón
    void onSubmit()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

    //Movemos la cámara modo FPS
    void MoveCamera()
    {
        xRotate += Input.GetAxis("Mouse X");
        yRotate += Input.GetAxis("Mouse Y");

        transform.rotation = Quaternion.Euler(-yRotate, xRotate, 0);
    }

    //Si se pulsa el botón mover, volvemos a hacer hija la figura del ratón
	[Command]
    public void CmdMoverClickado()
    {
		seleccionada.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
		RpcMoverClickado();
	}

	//Si se pulsa el botón mover, volvemos a hacer hija la figura del ratón
	[ClientRpc]
	public void RpcMoverClickado()
	{
		seleccionada.transform.SetParent(GameObject.FindGameObjectWithTag("Raton").transform);
	}

	//Dotamos a la figura de un color aleatorio
	public void ColorClickado()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value);
        seleccionada.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
    }

    //Lleva el canvas de menú de figuras a la posición donde está la figura en la que pulsamos
    void PosicionCanvas(GameObject tocada)
    {
        if (fpsCam.transform.position == new Vector3(0, 1.2f, 15)) //alzado
        {
            canvasFigura.transform.LookAt(fpsCam.transform);
            canvasFigura.transform.position = new Vector3(tocada.transform.position.x + 2, tocada.transform.position.y + 2, tocada.transform.position.z + 2);
        }
        if (fpsCam.transform.position == new Vector3(15, 1.2f, 0)) //perfil
        {
            canvasFigura.transform.LookAt(fpsCam.transform);
            canvasFigura.transform.position = new Vector3(tocada.transform.position.x + 2, tocada.transform.position.y + 2, tocada.transform.position.z + 2);
        }
        if (fpsCam.transform.position == new Vector3(0, 15, 0)) //planta
        {
            canvasFigura.transform.LookAt(fpsCam.transform);
            canvasFigura.transform.position = new Vector3(tocada.transform.position.x + 2, tocada.transform.position.y + 2, tocada.transform.position.z + 2);
        }
    }

    // Se encarga de spawnear a las figuras en el entorno para interactuar con ellas
    [Command]
    public void CmdSpawn(string fig, string padre)
    {
        figura = (GameObject)Instantiate(Resources.Load(fig));
        NetworkServer.Spawn(figura);
        figura.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        RpcParent(figura, padre);
        //figura.GetComponent<NetworkIdentity>().RemoveClientAuthority(connectionToClient);
    }

    // Se encarga de hacer al objeto hijo del mando que corresponda
    [ClientRpc]
    public void RpcParent(GameObject figura, string padre)
    {
        figura.transform.SetParent(GameObject.FindGameObjectWithTag(padre).transform);
        figura.GetComponent<Rigidbody>().isKinematic = true;
    }

	[Command]
	void CmdNotParent(GameObject figura)
	{
		RpcNotParent(figura);
		figura.GetComponent<NetworkIdentity>().RemoveClientAuthority(connectionToClient);
	}

	[ClientRpc]
	void RpcNotParent(GameObject figura)
	{
		figura.transform.SetParent(null);
	}
}