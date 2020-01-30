using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    public static bool _raceIsActive;
    GoalManager _goalManager;

    private void Awake()
    {
        // Turns off the VR XR settings to false
        UnityEngine.XR.XRSettings.enabled = false;
    }


    // Loads The Menu Scene
    public void StartMainMenu()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        RaceIsNOTActive();
        SceneManager.LoadScene(0);
    }

    // Loads The SinglePlayer Scene
    public void StartNormalMod()
    {
        SceneManager.LoadScene(3);
    }

    // Loads The Two Player Scene
    public void StartTwoPLayerMod()
    {
        SceneManager.LoadScene(2);
    }
    
    // Loads The VR Scene
    public void StartVrMod()
    {
        SceneManager.LoadScene(3);
    }


    // Turns ON the Static bool which represents if the Race is Active or not
    public void RaceIsActive()
    {
        _raceIsActive = true;
    }
    
    // Turns OFF the Static bool which represents if the Race is Active or not
    public void RaceIsNOTActive()
    {
        _raceIsActive = false;
    }
    
    // Quits The Game
    public void QuitGame()
    {
        Application.Quit();
    }
    
    // Restarts the current Scene
    public void RestartLevel()
    {
        RaceIsNOTActive();
        TimeScore._scoreTime = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
