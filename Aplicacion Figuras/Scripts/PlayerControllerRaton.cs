using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerRaton : MonoBehaviour
{
	public float range = 50f;                                     // Distance in Unity units over which the player can fire
   
    public delegate void puntero();
    public static puntero pointer;
    //public event PointerEventHandler PointerIn;
    //public event PointerEventHandler PointerOut;

    private Camera fpsCam;                                        // Holds a reference to the first person camera
	Quaternion targetRotation;
    GameObject figura;
    GameObject[] allObjects;
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
	}

    void Update()
	{
        allObjects = GameObject.FindObjectsOfType<GameObject>();
        /// Comprobar si es local player en Networking
        bool bHit;
        MoveCamera();

		//GetComponent<Transform>().LookAt(Input.mousePosition);

		// Create a vector at the center of our camera's viewport
		Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

		// Declare a raycast hit to store information about what our raycast has hit		
        PlayerControllerRaton.pointer -= onPointerIn;
		PlayerControllerRaton.pointer -= onPointerOut;
		PlayerControllerRaton.pointer -= onSubmit;
        // Check if our raycast has hit anything
        foreach(Transform child in transform)
        {
            if (child.gameObject.tag == "Figura") tieneHijo = true;
            else tieneHijo = false;
        }
        bHit = Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range);
        if (!tieneHijo)
        {
            if (bHit)
            {
				pointer += onPointerIn;
				previousContact = hit.transform;
				pointer();
				if (Input.GetMouseButton(0))
                {
                    pointer += onSubmit;
                    pointer();
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

        if (!Input.GetMouseButton(0))
        {
            if (figura != null)
            {
                if (figura.transform.parent != null)
                {
                    if (figura.transform.parent.gameObject == this.gameObject)
                    {
                        figura.GetComponent<Rigidbody>().isKinematic = false;
                        figura.transform.SetParent(null);
                    }
                }
            }
        }

        if (bHit)
        {
            if (Input.GetMouseButton(1))
            {
                if (hit.collider.gameObject.tag == "Figura")
                {
                    Debug.Log("Entra");
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

    void onPointerIn()
    {
        Button btn = hit.collider.gameObject.GetComponent<Button>();
        if (btn != null)
        {
            btn.Select();
        }
    }

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

	void onSubmit()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

	void MoveCamera()
	{
        // Dependiendo de si esta en una vista u otra hay que usar el LookAt() de forma distinta
        float sensitivity = 0.02f;
        Vector3 vp = fpsCam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, fpsCam.nearClipPlane));
        vp.x -= 0.5f;
        vp.y -= 0.5f;
        vp.x *= sensitivity;
        vp.y *= sensitivity;
        vp.x += 0.5f;
        vp.y += 0.5f;
        Vector3 sp = fpsCam.ViewportToScreenPoint(vp);

        Vector3 v = fpsCam.ScreenToWorldPoint(sp);

		if (this.transform.position == new Vector3(0, 1.2f, 15)) //Alzado
		{
			transform.LookAt(v, Vector3.up);
		}
		if (this.transform.position == new Vector3(15, 1.2f, 0)) //perfil
		{
			transform.LookAt(v, Vector3.up);
		}
		if (this.transform.position == new Vector3(0, 15, 0)) //Planta
		{
			transform.LookAt(v, Vector3.forward);
		}
    }

    public void EsferaClicked()
    {
        figura = (GameObject)Instantiate(Resources.Load("Esfera"));
        figura.transform.SetParent(this.transform);
    }

    public void CuboClicked()
    {
        figura = (GameObject)Instantiate(Resources.Load("Cubo"));
        figura.transform.SetParent(this.transform);
    }

    public void CapsulaClicked()
    {
        figura = (GameObject)Instantiate(Resources.Load("Capsula"));
        figura.transform.SetParent(this.transform);
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
}