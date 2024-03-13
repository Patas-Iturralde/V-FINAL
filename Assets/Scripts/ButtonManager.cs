using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void LoadLevel(string index)
    {
        SceneManager.LoadScene("Modular DungeonV3");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
