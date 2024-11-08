using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate=true;
    private void Update()
    {
        if(isFirstUpdate)
        {
            isFirstUpdate=false;
            SceneLoader.LoaderCallback();
        }
    }
}
