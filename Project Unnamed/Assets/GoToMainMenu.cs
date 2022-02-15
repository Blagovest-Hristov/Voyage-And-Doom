using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public Button start;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

}
