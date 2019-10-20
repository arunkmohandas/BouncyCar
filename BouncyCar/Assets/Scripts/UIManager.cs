using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject stopButton;
    public BouncyCarController carController;
    private void Awake()
    {
        stopButton.SetActive(false);
        startButton.SetActive(true);
    }
    public void StartGame()
    {
        carController.InitCar();
        stopButton.SetActive(true);
        startButton.SetActive(false);
    }
    public void StopGame()
    {
        carController.StopCar();
        SceneManager.LoadScene(0);
    }
}