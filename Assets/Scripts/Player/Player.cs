using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : EntityWithStats
{
    public Transform shootPos;
    // Start is called before the first frame update

    void Start()
    {
        side = this.tag;
        autoAtack = true;
        main_stat = Const._agi;
        stats[Const._str]=16; stats[Const._agi]=20; stats[Const._int]=15;
        stats_d[Const._str]=1.4f; stats_d[Const._agi]=2.9f; stats_d[Const._int]=1.4f;

        hp=200+stats[Const._str]*20; mana=75+stats[Const._int]*12;

        dmg_base = new Tuple<float, float>(31, 38);
        atack_spd = 1.5f;

    }

    // Update is called once per frame
    void Update()
    {   
    }



}
