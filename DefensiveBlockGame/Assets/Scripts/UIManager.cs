using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("Block Count")]

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject inventoryPanel;

    // Update is called once per frame
    void Update()
    {

        //if (Gamepad.all[0].startButton.isPressed)
        //{
        //    if (GlobalScript.gameIsPaused) PauseGame(false);
        //    if (!GlobalScript.gameIsPaused) PauseGame(true);
        //}

        //HideInventory();
    }


    public void PauseGame(bool isPaused)
    {
        pausePanel.SetActive(isPaused);
        GlobalScript.gameIsPaused = isPaused;
    }


    private void HideInventory()
    {

        if (Gamepad.all[0].buttonWest.isPressed && !GlobalScript.inventoryIsHidden)
        {
            inventoryPanel.SetActive(false);
            GlobalScript.inventoryIsHidden = true;
            Debug.Log("Inventory is Hidden");
        }

        if (Gamepad.all[0].buttonWest.isPressed && GlobalScript.inventoryIsHidden)
        {
            inventoryPanel.SetActive(true);
            GlobalScript.inventoryIsHidden = false;
            Debug.Log("Inventory is not Hidden");
        }

    }

}
