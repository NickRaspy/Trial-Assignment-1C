using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void GameStart() => SceneManager.LoadScene("Game");
    public void Exit() => Application.Quit();
}
