using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Spawner_SinglePlayer : MonoBehaviour
{


    public  GameObject character;
    
    public GameObject generationGrid;
    void Start()
    {
        Instantiate(character, new Vector3(), Quaternion.identity);
        generationGrid.SetActive(true);
    }

    // Update is called once per frame
   
}
