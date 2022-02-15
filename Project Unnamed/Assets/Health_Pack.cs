using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pack : MonoBehaviour
{
    public float heal = 25;
    public LayerMask a;
    public Transform trans;
    public GameObject destroy;
    void Start()
    {
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0.01f));
        if (Physics2D.OverlapCircleAll(trans.position, 0.5f, a).Length>0)
        {
            Physics2D.OverlapCircleAll(trans.position, 0.5f, a)[0].GetComponent<Movement>().Heal(heal);
            Destroy(destroy);
        }
    }
}
