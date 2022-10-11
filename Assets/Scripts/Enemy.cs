using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEntity
{

    // Start is called before the first frame update
    void Start()
    {
        max_hp = 200;
        hp = max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position.normalized;
        dir.y -= 5;
        if (transform.localPosition.y > 5) { transform.Translate(dir.normalized *3f*Time.deltaTime); }
    }


}
