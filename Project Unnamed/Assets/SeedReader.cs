using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedReader : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] startingPos;
    public GameObject[] RoomTypes;
    public GameObject[] RoomInteriors;
    public GameObject Spawn;
    private int rotate;

    void Start()
    {
        
    }
    public void ReadSeed(string seed)
    {
        var character = GameObject.Find("Character (1) Variant(Clone)");

        Instantiate(Spawn, startingPos[seed[0] - 48].transform.position, Quaternion.Euler(0,180,0));
        transform.position = startingPos[seed[0] - 48].transform.position;
        for (int i = 1; i < seed.Length - 1; i++)
        {
            if (seed[i] - 48 == 1 || seed[i] - 48 == 2)
            {
             
                    Vector2 newPos = new Vector2(transform.position.x + 20, transform.position.y);
                    transform.position = newPos;

                    rotate = 0;
               
            }
            else if (seed[i] - 48 == 3 || seed[i] - 48 == 4)
            {
               
                    Vector2 newPos = new Vector2(transform.position.x - 20, transform.position.y);
                    transform.position = newPos;
                  
                    rotate = 180;         
                
            }
            else if (seed[i] - 48 == 5)
            {
                    Vector2 newPos = new Vector2(transform.position.x, transform.position.y - 15);
                    transform.position = newPos;
            }

            if (seed[i] - 48 == seed[i+1] - 48 && seed[i] - 48 < 5)
            {
                Instantiate(RoomTypes[0], transform.position, Quaternion.identity);
            }
            else if (seed[i] - 48 == 5 && seed[i+1] - 48 == 5)
            {
                Instantiate(RoomTypes[3], transform.position, Quaternion.identity);
            }
            else if (seed[i] - 48 == 5 && seed[i+1] - 48 < 5)
            {
                Instantiate(RoomTypes[2], transform.position, Quaternion.Euler(0,rotate,0));
            }
            else if (seed[i] - 48 < 5 && seed[i+1] - 48 == 5)
            {
                Instantiate(RoomTypes[1], transform.position, Quaternion.Euler(0,rotate,0));

            }
        }
            while (startingPos[seed[0] - 48].transform.position != character.transform.position)
            {
                character.GetComponent<Transform>().position = Vector3.MoveTowards(character.transform.position, startingPos[seed[0] - 48].transform.position, Time.deltaTime * 1);
            }
            character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, 33);

    }
    // Update is called once per frame
   
}
