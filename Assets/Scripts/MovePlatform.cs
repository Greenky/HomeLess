using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x > -23)
        {
            speed = -speed;
        }

        if (transform.position.x < -34)
        {
            speed = 2f;
        }
        Debug.Log(transform.position.x);
    }
}
