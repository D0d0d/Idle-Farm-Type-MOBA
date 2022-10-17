using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class Tower : BaseEntity
{
    public Transform shootPos;
    void Start()
    {
        autoAtack = true;

        hp = 18000;
        armor = 12;
        atack_spd = 1.5f;
        dmg_base = new Tuple<float, float>(88,92);
        isAtacking = true;
        StartCoroutine("AutoAtack");
        side = this.tag;


    }



}
