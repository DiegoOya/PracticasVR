using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Esta clase controla en qué posición se coloca la cámara dependiendo de que vista seleccionemos*/
public class CameraManager : MonoBehaviour
{
    public GameObject canvasPTodo, canvasVTodo, canvasFTodo, canvasOpcionesFigura, canvasMira;
    GameObject camHTC, camRaton;
    [HideInInspector]
    public int nmandos;
    bool comp = true;
    int cont = 0;

    // Use this for initialization
    public void Awake()
    {
        //Busca los PlayerPrefabs del control de teclado y raton y de las gafas HTC
        camRaton = GameObject.FindGameObjectWithTag("Raton");
        camHTC = GameObject.FindGameObjectWithTag("HTC");
        //Dependiendo de la opcion seleccionada inicializa los paneles y coloca al PlayerPrefab en su sitio
        switch (ControlOption.current.option)
        {
            case 0: //HTC
                canvasFTodo.SetActive(false);
                canvasVTodo.SetActive(false);
                //canvasOpcionesFigura.SetActive(false);
                canvasMira.SetActive(false);
                //if (camRaton != null) camRaton.SetActive(false);
                //if (camHTC != null) camHTC.SetActive(true);
                alzado_pulsado();
                break;
            case 1: //Raton
                canvasFTodo.SetActive(false);
                canvasVTodo.SetActive(false);
                //canvasOpcionesFigura.SetActive(false);
                //if (camHTC != null) camHTC.SetActive(false);
                //if (camRaton != null) camRaton.SetActive(true);
                canvasMira.SetActive(true);
                alzado_pulsado();
                break;
        }
    }

    public void Update()
    {
        //Busca cada frame los PlayerPrefabs del control de teclado y raton y de las gafas HTC
        camRaton = GameObject.FindGameObjectWithTag("Raton");
        camHTC = GameObject.FindGameObjectWithTag("HTC");
        if (camRaton != null) camRaton.SetActive(true);
        if (camHTC != null) camHTC.SetActive(true);
        if (comp && (camRaton != null || camHTC != null))
        {
            comp = false;
            alzado_pulsado();
        }
        if (ControlOption.current.option == 0 && camHTC!=null) //HTC
        {
            if (cont <= 10)
            {
                if (camHTC.transform.GetChild(0).gameObject.activeInHierarchy && camHTC.transform.GetChild(1).gameObject.activeInHierarchy) nmandos = 2;
                else if (camHTC.transform.GetChild(0).gameObject.activeInHierarchy) nmandos = 0;
                else if (camHTC.transform.GetChild(1).gameObject.activeInHierarchy) nmandos = 1;
                cont++;
            }
        }
    }
    
    // Si se pulsa el boton alzado coloca al PlayerPrefab en la posicion predeterminada de la vista alzado
    public void alzado_pulsado()
    {
        switch (ControlOption.current.option) {
            case 0: //HTC
                if (camHTC != null)
                {
                    //if (camRaton != null) camRaton.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    camHTC.GetComponent<Transform>().position = new Vector3(0, 0, 15);
                    camHTC.GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
                    canvasPTodo.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                    canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                    canvasPTodo.transform.LookAt(camHTC.transform);
                    canvasPTodo.SetActive(true);
                }
                break;
            case 1:
                if (camRaton != null)
                {
                    //if (camHTC != null) camHTC.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    camRaton.GetComponent<Transform>().position = new Vector3(0, 1.2f, 15);
                    camRaton.GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
                    PlayerControllerRaton.xRotate = 180;
                    PlayerControllerRaton.yRotate = 0;
                    canvasPTodo.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                    canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                    canvasPTodo.transform.LookAt(camRaton.transform);
                    canvasPTodo.SetActive(true);
                }
                break;
        }        
        
    }

    // Si se pulsa el boton planta coloca al PlayerPrefab en la posicion predeterminada de la vista planta
    public void planta_pulsado()
    {
        switch (ControlOption.current.option) {
            case 0: //HTC
                if(camHTC != null){
                    //if (camRaton != null) camRaton.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasOpcionesFigura.SetActive(false);
                    camHTC.GetComponent<Transform>().position = new Vector3(0, 15, 0);
                    camHTC.GetComponent<Transform>().eulerAngles = new Vector3(90, 0, 0);
                    canvasPTodo.transform.position = new Vector3(camHTC.transform.position.x - 8, 1, camHTC.transform.position.z + 4);
                    canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 8, 0, camHTC.transform.position.z + 4);
                    canvasPTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                    canvasPTodo.SetActive(true);
                    switch (nmandos)
                    {
                        case 0: camHTC.transform.GetChild(0).gameObject.SetActive(true); break;
                        case 1: camHTC.transform.GetChild(1).gameObject.SetActive(true); break;
                        case 2: camHTC.transform.GetChild(0).gameObject.SetActive(true); camHTC.transform.GetChild(1).gameObject.SetActive(true); break;
                    }
                }
                break;
            case 1: //Raton
                if (camRaton != null)
                {
                    //if (camHTC != null) camHTC.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasOpcionesFigura.SetActive(false);
                    camRaton.GetComponent<Transform>().transform.position = new Vector3(0, 15, 0);
                    camRaton.GetComponent<Transform>().eulerAngles = new Vector3(90, 0, 0);
					PlayerControllerRaton.xRotate = 0;
					PlayerControllerRaton.yRotate = -90;
					canvasPTodo.transform.position = new Vector3(camRaton.transform.position.x - 8, 1, camRaton.transform.position.z + 4);
                    canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 8, 0, camRaton.transform.position.z + 4);
                    canvasPTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                    canvasPTodo.SetActive(true);
                }
                break;
        }
    }

    // Si se pulsa el boton perfil coloca al PlayerPrefab en la posicion predeterminada de la vista perfil
    public void perfil_pulsado()
    {
        switch (ControlOption.current.option)
        {
            case 0: //HTC
                if (camHTC != null)
                {
                    //if (camRaton != null) camRaton.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasOpcionesFigura.SetActive(false);
                    camHTC.GetComponent<Transform>().position = new Vector3(15, 0, 0);
                    camHTC.GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);
                    canvasPTodo.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                    canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                    canvasPTodo.transform.LookAt(camHTC.transform);
                    canvasPTodo.SetActive(true);
                    switch (nmandos)
                    {
                        case 0: camHTC.transform.GetChild(0).gameObject.SetActive(true); break;
                        case 1: camHTC.transform.GetChild(1).gameObject.SetActive(true); break;
                        case 2: camHTC.transform.GetChild(0).gameObject.SetActive(true); camHTC.transform.GetChild(1).gameObject.SetActive(true); break;
                    }
                }
                break;
            case 1: //Raton
                if (camRaton != null)
                {
                    //if (camHTC != null) camHTC.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasOpcionesFigura.SetActive(false);
                    camRaton.GetComponent<Transform>().position = new Vector3(15, 1.2f, 0);
                    camRaton.GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);
					PlayerControllerRaton.xRotate = -90;
					PlayerControllerRaton.yRotate = 0;
					canvasPTodo.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                    canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                    canvasPTodo.transform.LookAt(camRaton.transform);
                    canvasPTodo.SetActive(true);
                }
                break;
        }
        
    }

    // Si se pulsa el boton vistas desactiva el panel principal y activa el de las vistas
    public void vistas_pulsado()
    {
        switch (ControlOption.current.option) {
            case 0: //HTC
                //if (camRaton != null) camRaton.SetActive(false);
                if (camHTC.transform.position == new Vector3(0, 0, 15)) //Alzado
                {
                    canvasPTodo.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                    canvasVTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                    canvasVTodo.transform.LookAt(camHTC.transform);
                    canvasVTodo.SetActive(true);
                }
                if (camHTC.transform.position == new Vector3(15, 0, 0)) //Perfil
                {
                    canvasPTodo.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                    canvasVTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                    canvasVTodo.transform.LookAt(camHTC.transform);
                    canvasVTodo.SetActive(true);
                }
                if (camHTC.transform.position == new Vector3(0, 15, 0)) //Planta
                {
                    canvasPTodo.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.transform.position = new Vector3(camHTC.transform.position.x - 8, 3, camHTC.transform.position.z + 4);
                    canvasVTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 8, 0, camHTC.transform.position.z + 4);
                    canvasVTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                    canvasVTodo.SetActive(true);
                }
                break;

            case 1: //Raton
                //if (camHTC != null) camHTC.SetActive(false);
                if (camRaton.transform.position == new Vector3(0, 1.2f, 15)) //Alzado
                {
                    canvasPTodo.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                    canvasVTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                    canvasVTodo.transform.LookAt(camRaton.transform);
                    canvasVTodo.SetActive(true);
                }
                if (camRaton.transform.position == new Vector3(15, 1.2f, 0)) //Perfil
                {
                    canvasPTodo.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                    canvasVTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                    canvasVTodo.transform.LookAt(camRaton.transform);
                    canvasVTodo.SetActive(true);
                }
                if (camRaton.transform.position == new Vector3(0, 15, 0)) //Planta
                {
                    canvasPTodo.SetActive(false);
                    canvasFTodo.SetActive(false);
                    canvasVTodo.transform.position = new Vector3(camRaton.transform.position.x - 8, 3, camRaton.transform.position.z + 4);
                    canvasVTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 8, 0, camRaton.transform.position.z + 4);
                    canvasVTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                    canvasVTodo.SetActive(true);
                }
                break;
        }
        
    }

    // Si se pulsa el boton figuras desactiva el panel principal y activa el de las figuras
    public void figuras_pulsado()
    {
        switch (ControlOption.current.option)
        {
            case 0: //HTC
                //if (camRaton != null) camRaton.SetActive(false);
                if (camHTC.transform.position == new Vector3(0, 0, 15)) //alzado
                {
                    canvasPTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasFTodo.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                    canvasFTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                    canvasFTodo.transform.LookAt(camHTC.transform);
                    canvasFTodo.SetActive(true);
                }
                if (camHTC.transform.position == new Vector3(15, 0, 0)) //perfil
                {
                    canvasPTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasFTodo.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                    canvasFTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                    canvasFTodo.transform.LookAt(camHTC.transform);
                    canvasFTodo.SetActive(true);
                }
                if (camHTC.transform.position == new Vector3(0, 15, 0)) //planta
                {
                    canvasPTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasFTodo.transform.position = new Vector3(camHTC.transform.position.x - 8, 3, camHTC.transform.position.z + 4);
                    canvasFTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 8, 0, camHTC.transform.position.z + 4);
                    canvasFTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                    canvasFTodo.SetActive(true);
                }
                break;

            case 1: //Raton
                //if (camHTC != null) camHTC.SetActive(false);
                if (camRaton.transform.position == new Vector3(0, 1.2f, 15)) //alzado
                {
                    canvasPTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasFTodo.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                    canvasFTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                    canvasFTodo.transform.LookAt(camRaton.transform);
                    canvasFTodo.SetActive(true);
                }
                if (camRaton.transform.position == new Vector3(15, 1.2f, 0)) //perfil
                {
                    canvasPTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasFTodo.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                    canvasFTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                    canvasFTodo.transform.LookAt(camRaton.transform);
                    canvasFTodo.SetActive(true);
                }
                if (camRaton.transform.position == new Vector3(0, 15, 0)) //planta
                {
                    canvasPTodo.SetActive(false);
                    canvasVTodo.SetActive(false);
                    canvasFTodo.transform.position = new Vector3(camRaton.transform.position.x - 8, 3, camRaton.transform.position.z + 4);
                    canvasFTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 8, 0, camRaton.transform.position.z + 4);
                    canvasFTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                    canvasFTodo.SetActive(true);
                }
                break;
        }
        
    }


    // Si se pulsa el boton volver desactiva el panel que esté activo y activa el principal
    public void Volver()
    {
        switch (ControlOption.current.option)
        {
            case 0: //HTC
                //if (camRaton != null) camRaton.SetActive(false);
                if (camHTC.transform.position == new Vector3(0, 0, 15)) //alzado
                {
                    if (canvasFTodo.activeInHierarchy || canvasVTodo.activeInHierarchy)
                    {
                        canvasVTodo.SetActive(false);
                        canvasFTodo.SetActive(false);
                        canvasPTodo.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                        canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x + 8, 5, 0);
                        canvasPTodo.transform.LookAt(camHTC.transform);
                        canvasPTodo.SetActive(true);
                    }
                }
                if (camHTC.transform.position == new Vector3(0, 15, 0))  //planta
                {
                    if (canvasFTodo.activeInHierarchy || canvasVTodo.activeInHierarchy)
                    {
                        canvasVTodo.SetActive(false);
                        canvasFTodo.SetActive(false);
                        canvasPTodo.transform.position = new Vector3(camHTC.transform.position.x - 8, 1, camHTC.transform.position.z + 4);
                        canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 8, 1, camHTC.transform.position.z + 4);
                        canvasPTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                        canvasPTodo.SetActive(true);
                    }
                }
                if (camHTC.transform.position == new Vector3(15, 0, 0)) //perfil
                {
                    if (canvasFTodo.activeInHierarchy || canvasVTodo.activeInHierarchy)
                    {
                        canvasVTodo.SetActive(false);
                        canvasFTodo.SetActive(false);
                        canvasPTodo.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                        canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camHTC.transform.position.x - 17, camHTC.transform.position.y + 5, camHTC.transform.position.z - 9);
                        canvasPTodo.transform.LookAt(camHTC.transform);
                        canvasPTodo.SetActive(true);
                    }
                }
                break;

            case 1: //Raton
                //if (camHTC != null) camHTC.SetActive(false);
                if (camRaton.transform.position == new Vector3(0, 1.2f, 15)) //alzado
                {
                    if (canvasFTodo.activeInHierarchy || canvasVTodo.activeInHierarchy)
                    {
                        canvasVTodo.SetActive(false);
                        canvasFTodo.SetActive(false);
                        canvasPTodo.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                        canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x + 8, 5, 0);
                        canvasPTodo.transform.LookAt(camRaton.transform);
                        canvasPTodo.SetActive(true);
                    }
                }
                if (camRaton.transform.position == new Vector3(0, 15, 0))  //planta
                {
                    if (canvasFTodo.activeInHierarchy || canvasVTodo.activeInHierarchy)
                    {
                        canvasVTodo.SetActive(false);
                        canvasFTodo.SetActive(false);
                        canvasPTodo.transform.position = new Vector3(camRaton.transform.position.x - 8, 1, camRaton.transform.position.z + 4);
                        canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 8, 1, camRaton.transform.position.z + 4);
                        canvasPTodo.transform.eulerAngles = new Vector3(270, 180, 0);
                        canvasPTodo.SetActive(true);
                    }
                }
                if (camRaton.transform.position == new Vector3(15, 1.2f, 0)) //perfil
                {
                    if (canvasFTodo.activeInHierarchy || canvasVTodo.activeInHierarchy)
                    {
                        canvasVTodo.SetActive(false);
                        canvasFTodo.SetActive(false);
                        canvasPTodo.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                        canvasPTodo.transform.GetChild(0).gameObject.transform.position = new Vector3(camRaton.transform.position.x - 17, camRaton.transform.position.y + 5, camRaton.transform.position.z - 9);
                        canvasPTodo.transform.LookAt(camRaton.transform);
                        canvasPTodo.SetActive(true);
                    }
                }
                break;
        }
        
    }
}
