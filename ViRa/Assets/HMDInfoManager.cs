using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (XRSettings.isDeviceActive == false)
        {
            Debug.Log("No headset found.");
        }
        else if (XRSettings.loadedDeviceName.ToLower().Contains("mock"))
        {
            Debug.Log("Using the Mock Display.");
        }
        else
        {
            Debug.Log($"Found headset: {XRSettings.loadedDeviceName}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
