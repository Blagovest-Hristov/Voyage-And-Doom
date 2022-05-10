using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadingScreen : MonoBehaviour
{
    public void StartMultiplayer()
    {
        SceneManager.LoadScene("LoadingScene");
    }

   
}
