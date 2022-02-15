using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popUp;
    public Button controls;
    void Start()
    {
        controls.onClick.AddListener(popUpController);
    }

    // Update is called once per frame
    void popUpController()
    {

        popUp.SetActive(!popUp.active);

        
    }
}
