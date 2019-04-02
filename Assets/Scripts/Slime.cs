using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    // Start is called before the first frame update
    private float _birthTime;
    public float lifeTime = 2;

    private void Start()
    {
        _birthTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_birthTime + lifeTime < Time.time)
            Destroy(this.gameObject);
    }
 }
