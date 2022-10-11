using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class Tower : BaseEntity
{
    public Transform shootPos;
    public GameObject bullet;
    void Start()
    {
        autoAtack = true;

        hp = 1800;
        armor = 12;

        dmg_base = new Tuple<float, float>(88,92);
        isAtacking = true;
        StartCoroutine("AutoAtack");


    }


    public override void Atack()
    {
        Vector3 dir = (shootPos.position - this.Target.transform.position).normalized;

        dmg = Random.Range(dmg_base.Item1, dmg_base.Item2);
        if (isRanged) { Instantiate(bullet, shootPos.position, Quaternion.Euler(dir), transform).GetComponent<BaseBullet>().DmgTarget(dmg, Target); };
    }
}
