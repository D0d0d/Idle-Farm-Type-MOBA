using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public class LaneCreep : BaseEntity
{
    public float speed = 2f;
    public Transform Destination;

    public void Set(Transform Dest, string Side)
    {
        Destination = Dest;
        side=Side;
    }
    public void MoveToTarget() 
    {
        var lookPos = Destination.transform.position - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90f;
        Img.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, Destination.transform.position, speed * Time.deltaTime);
        //hpBar.transform.position = Vector3.MoveTowards(hpBar.transform.position, transform.position, speed * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        max_hp = 200;
        hp = max_hp;
        dmg_base = new Tuple<float, float>(15, 25);
        autoAtack = true;
        atack_spd = 4f;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!isAtacking)
        {
            if (Destination)
            {
                MoveToTarget();
            }
            if (FindTarget()) { DoAtack(); }
        }

    }

}
