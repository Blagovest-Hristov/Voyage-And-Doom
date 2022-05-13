using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SaveSeed : MonoBehaviourPunCallbacks
{
    public GameObject generation;
    public GameObject structure;
    // Start is called before the first frame update
    void Start()
    {
        
    } 

    // Update is called once per frame
    void Update()
    {
        if (generation.GetComponent<Level_Genration>().enabled == false)
        {
            var a = new ExitGames.Client.Photon.Hashtable();

            a.Add("map", structure);
            PhotonNetwork.LocalPlayer.SetCustomProperties(a);
            GetComponent<SaveSeed>().enabled = false;
          
        }
    }
}
