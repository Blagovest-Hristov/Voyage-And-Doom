using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character_Spawner_MultiPlayer : MonoBehaviour
{

    public GameObject generationGrid;
    public GameObject Reader;
    void Start()
    {

        if (PhotonNetwork.CountOfPlayers == 1)
        {
            PhotonNetwork.Instantiate("Character (1) Variant", new Vector3(), Quaternion.identity);
            generationGrid.SetActive(true);
            
            
        }

        else
        {
            PhotonNetwork.Instantiate("Character (1) Variant", new Vector3(), Quaternion.identity);
            Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["seed"].ToString());
          
            Reader.GetComponent<SeedReader>().ReadSeed(PhotonNetwork.CurrentRoom.CustomProperties["seed"].ToString());
        }



    }
    void Update()
    {
        if (generationGrid.GetComponent<Level_Genration>().enabled == false)
        {
            var a = new ExitGames.Client.Photon.Hashtable();

            a.Add("seed", generationGrid.GetComponent<Level_Genration>().seed);


            PhotonNetwork.CurrentRoom.SetCustomProperties(a);

            GetComponent<Character_Spawner_MultiPlayer>().enabled = false;
        }
    }



}
