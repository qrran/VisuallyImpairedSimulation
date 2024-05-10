using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : Object
{
    
    //float hp = 100;
    public float damage = 0;
    public float Damage { get { return damage; } }
    public float speed = 1;
    public Vector2 direction = new Vector2(1, 1);
    //only apply values of 1 or -1 to direction since any other value will affect the speed.

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        myBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 current_pos = gameObject.transform.position;
        float move_pos_x = Time.deltaTime * speed * direction.x;
        float move_pos_y = Time.deltaTime * speed * direction.y;
        current_pos += new Vector2(move_pos_x,move_pos_y);
        gameObject.transform.position = current_pos;

    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        hp -= damage;
        Debug.Log("HP: " + hp);
    }*/
}
