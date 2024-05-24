using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MovingObject : Object
{

    //float hp = 100;
    public float damage = 0;
    public float speed = 0.25F;
    public UnityEngine.Vector2 direction = new UnityEngine.Vector2(1, 1);
    public bool roam = false;
    public float x_min = -5;
    public float x_max = 5;
    public float y_min = -4;
    public float y_max = 4;
    public int min_duration = 1;
    public int max_duration = 3;
    public float offset = 0.5F;
    //offset min and max boundaries for detection

    public UnityEngine.Vector2 Direction { get { return direction; } }
    public float Damage { get { return damage; } }
    public float X_min { get { return x_min; } }
    public float X_max { get { return x_max; } }
    public float Y_min { get { return y_min; } }
    public float Y_max { get { return y_max; } }

    private float timer;
    private float duration;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        myBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        timer = 0;
        duration = 2;
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 current_pos = gameObject.transform.position;

        if (roam)
        {
            if (timer > duration || current_pos.x > (x_max - offset) || current_pos.x < (x_min + offset) || current_pos.y > (y_max - offset) || current_pos.y < (y_min + offset))
            {
                changeDirection();
                timer = 0;
                duration = UnityEngine.Random.Range(min_duration, max_duration);
            }
            timer += Time.deltaTime;
        }

        float move_pos_x = Time.deltaTime * speed * direction.x;
        float move_pos_y = Time.deltaTime * speed * direction.y;
        current_pos += new UnityEngine.Vector2(move_pos_x, move_pos_y);
        gameObject.transform.position = current_pos;
    }

    void changeDirection()
    {
        UnityEngine.Vector2 current_pos = gameObject.transform.position;

        float angle;

        if (current_pos.x > (x_max - offset))
        {
            angle = UnityEngine.Random.Range(90, 270);
        }
        else if (current_pos.x < (x_min + offset))
        {
            angle = UnityEngine.Random.Range(270, 450);
        }
        else angle = UnityEngine.Random.Range(0, 360);
        direction.x = Mathf.Cos(angle * (Mathf.PI / 180));

        if (current_pos.y > (y_max - offset))
        {
            angle = UnityEngine.Random.Range(90, 270);
        }
        else if (current_pos.y < (y_min + offset))
        {
            angle = UnityEngine.Random.Range(270, 450);
        }
        else angle = UnityEngine.Random.Range(0, 360);

        direction.y = Mathf.Sin(angle * (Mathf.PI / 180));
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        hp -= damage;
        Debug.Log("HP: " + hp);
    }*/
}

