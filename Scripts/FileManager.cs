using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileManager : MonoBehaviour {

    ControlOption co = new ControlOption();

    private void Start()
    {
        ControlOption.current = co; 
    }
    public void HTC()
    {
        ControlOption.current.option = 0;
        SaveLoad.Save();
        SteamVR.enabled = true;
        UnityEngine.VR.VRSettings.enabled = true;
        SceneManager.LoadScene("vistas");
    }
	
	public void Raton()
    {
        ControlOption.current.option = 1;
        SaveLoad.Save();
        SteamVR.SafeDispose();
        UnityEngine.VR.VRSettings.enabled = false;
        SceneManager.LoadScene("vistas");
    }
}
