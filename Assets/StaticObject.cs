using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : Object
{

    public float damage = 0;
    public float Damage { get { return damage; } }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        myBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
