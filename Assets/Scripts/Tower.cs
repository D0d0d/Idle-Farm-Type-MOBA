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

        hp = 1800;
        armor = 12;

        dmg_base = new Tuple<float, float>(88,92);
        isAtacking = true;
        StartCoroutine("AutoAtack");


    }



}
