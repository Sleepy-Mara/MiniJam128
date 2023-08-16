using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private void Start()
    {
        // esto define la cantidad sobre la que se divide la tasa de refresco de la pantalla/dispositivo
        QualitySettings.vSyncCount = 1;
        //esto define la cantidad de fps objetivo a ejecutar el juego
        Application.targetFrameRate = 60;
    }
}
