using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInal : MonoBehaviour
{
    public LayerMask characterLayer;
    public GameObject popUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircleAll(transform.position, 10f, characterLayer).Length>0)
        {
            Debug.Log("scanned");
            popUp.SetActive(true);
        }
    }
}
