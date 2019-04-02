using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {

    public float speed;                     //bullet speed
    private float _birthTime;
    public float lifeTime = 3;
    

    private void Start()
    {
        _birthTime = Time.time;
    }

    // Update is called once per frame
    void Update ()
    {
        if (_birthTime + lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(GameObject.Find("Boss1").GetComponent<Boss1Handler>().bulletRange, -speed, 0);
    }

	

}
