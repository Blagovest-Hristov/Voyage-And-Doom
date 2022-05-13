using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class SeedWriter : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] startingPos;
    public GameObject[] RoomTypes;
    public GameObject[] RoomInteriors;
    public GameObject spawn;
    public GameObject character;
    public GameObject pathfinder;
    public GameObject enemy;
    public GameObject final;

    private GameObject parentOfStructure;

    public string seed = " "; 

    private int direction;
    public float moveAmountX;
    public float moveAmountY = 15;

    public float minX;
    public float maxX;
    public float minY;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 2f;
    public int next;

    private Transform a;

    private List<Vector2> enemySpawn = new List<Vector2>();
    void Start()
    {
        parentOfStructure = GameObject.Find("Structure");
        character = GameObject.Find("Character (1) Variant(Clone)");
        minX = startingPos[0].position.x;
        maxX = startingPos[3].position.x;
        minY = startingPos[0].position.y - 45;
        transform.position = new Vector3(0, 0, 33);
        int randStartingPos = Random.Range(0, startingPos.Length);
        //Instantiate(spawn, startingPos[randStartingPos].position, Quaternion.Euler(0, 180, 0));
        seed += randStartingPos;
      

        a = startingPos[randStartingPos];

        enemy.SetActive(true);

        direction = 1;
        transform.position = startingPos[randStartingPos].position;
        next = Random.Range(1, 4);
        seed += next;
    }

    private void Update()
    {

        if (timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    internal int maxRnd = 4;
    internal int minRnd = 1;
    internal int prev = 1;
    internal int rotate = 0;
    void Move()
    {

        if (direction == 1 || direction == 2)
        {
            if (transform.position.x <= maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
                transform.position = newPos;
                direction = 1;
                rotate = 0;
                maxRnd = 4;
            }
            else
            {
                direction = maxRnd - 1;
            }
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x >= minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmountX, transform.position.y);
                transform.position = newPos;
                minRnd = 3;
                direction = 3;
                rotate = 180;

            }
            else
            {
                direction = maxRnd - 1;
            }
        }
        if (direction == maxRnd - 1)
        {
            if (transform.position.y > minY)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountY);
                transform.position = newPos;
                maxRnd = 6;
                minRnd = 1;
                direction = 5;
            }
            else
            {
                AstarPath.active.Scan();

                foreach (var item in GetComponents<EnemyAI>())
                {
                    item.Target = character.transform;
                }

                AstarPath.active.Scan();


                GetComponent<Level_Genration>().enabled = false;
            }
        }

        if (next == 1 || next == 2)
        {
            if (transform.position.x <= maxX)
            {


                next = 1;
                maxRnd = 4;

            }
            else
            {
                next = maxRnd - 1;
            }
        }
        else if (next == 3 || next == 4)
        {
            if (transform.position.x >= minX)
            {


                minRnd = 3;
                next = 3;

            }
            else
            {
                next = maxRnd - 1;
            }
        }
        if (next == maxRnd - 1)
        {
            if (transform.position.y > minY)
            {

                maxRnd = 6;
                minRnd = 1;
                next = 5;
            }
            else
            {

                Debug.Log("lenght:" + FindObjectsOfType<EnemyAI>().Length);
                foreach (var item in FindObjectsOfType<EnemyAI>())
                {
                    item.Target = character.transform;
                }

                foreach (var item in enemySpawn)
                {
                    Instantiate(enemy, item, Quaternion.identity);
                }
                AstarPath.active.Scan();

                while (a.position != character.transform.position)
                {
                    character.GetComponent<Transform>().position = Vector3.MoveTowards(character.transform.position, a.position, Time.deltaTime * 1);
                }
                character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, 33);
                Instantiate(final, transform.position, Quaternion.identity, parentOfStructure.transform);
                Instantiate(RoomTypes[4], transform.position, Quaternion.identity, parentOfStructure.transform);
                AstarPath.active.Scan();

                GetComponent<Level_Genration>().enabled = false;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////   


        if (maxRnd - 1 == direction && maxRnd - 1 > next)
        {
            Instantiate(RoomTypes[0], transform.position, Quaternion.identity, parentOfStructure.transform);
            enemySpawn.Add(transform.position);
            AstarPath.active.Scan();
        }
        else if (maxRnd - 1 > direction && maxRnd - 1 == next)
        {
            Instantiate(RoomTypes[3], transform.position, Quaternion.Euler(0, rotate, 0), parentOfStructure.transform);
        }
        else if (maxRnd - 1 == direction && maxRnd - 1 == next)
        {

            Instantiate(RoomTypes[1], transform.position, Quaternion.identity, parentOfStructure.transform);
        }
        else if (maxRnd - 1 > direction && maxRnd - 1 > next)
        {
            Instantiate(RoomTypes[2], transform.position, Quaternion.Euler(0, rotate, 0), parentOfStructure.transform);
        }
        else
        {
            Instantiate(RoomTypes[0], transform.position, Quaternion.Euler(0, 180, 0), parentOfStructure.transform);
        }

        Instantiate(RoomInteriors[Random.Range(0, RoomInteriors.Length - 1)], transform.position, Quaternion.identity, parentOfStructure.transform);
        AstarPath.active.Scan();


        prev = direction;
        direction = next;
        next = Random.Range(minRnd, maxRnd);

    }
}
