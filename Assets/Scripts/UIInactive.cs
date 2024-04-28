using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInactive : MonoBehaviour
{

    public GameObject[] panelHide;
    public MasksSelector masksSelector;

    public bool isActive = true;

    private void Start()
    {
        masksSelector = GetComponent<MasksSelector>();
    }

    public void HidePanelUI()
    {
        if (!isActive)
        {
            foreach (GameObject panel in panelHide)
            {
                panel.gameObject.SetActive(true);
            }
            isActive = true;
        }
        else if (isActive) 
        {
            foreach (GameObject panel in panelHide)
            {
                panel.gameObject.SetActive(false);
            }
            isActive = false;
        }
    }

}
