using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class Const{
public const string _str="str", _agi="agi", _int="int";
}
public class BaseEntity : MonoBehaviour
{
    public string side;
    public GameObject hpBar;
    public Transform Img;
    public GameObject bullet;

    public HashSet<BaseEntity> _enemies;
    private BaseEntity Target;

    public AtackObject AtackRadius;

    protected bool isAtacking;
    protected bool autoAtack;
    public bool isRanged = false;

    public float max_hp, max_mana, hp,mana;               // 646 null
    public float atack_spd;
    public float atack_range;
    public float move_speed;
    public float magic_boost;
    public float mana_regen;

    public float armor;
    public float phys_resist;
    public float mag_resist;
    public float eff_resist;
    public float evade;
    public float hp_regen;

    public Tuple<float, float> dmg_base;// 27-31
    public float dmg;                    // полный урон, пока что dmg_base+основной стат

    public bool isDead = false;
    public void takeDamage(float dmg_)
    {
        hp -= dmg_;
        if (hpBar!=null)
        {
            Vector3 newBar = hpBar.transform.localScale;
            newBar.x = hp / max_hp;
            hpBar.transform.localScale = newBar;
        }
        if (hp < 0) { Destroy(this.gameObject); isDead = true; }
    }
    public bool FindTarget()
    {
        this.Target = AtackRadius.Target;
        _enemies = AtackRadius._enemies;
        if (Target == null||Target.isDead)
        {
            float sqrLen = AtackRadius.AtakRadius.radius*10f;

            foreach (BaseEntity t in _enemies)
            {
                float new_sqrLen = ( transform.position-t.transform.position ).sqrMagnitude;
                if (new_sqrLen < sqrLen)
                {
                    sqrLen = new_sqrLen;
                    this.Target = t;
                }
            }
        }
        return !(Target == null || Target.isDead);
    }

    public virtual float AddDamage()
    {
        return 0f;
    }
    public virtual float DoDamage()
    {
        return Random.Range(dmg_base.Item1, dmg_base.Item2) + AddDamage();
    }
    public virtual void Atack()
    {
        if (FindTarget())
        {
            Vector3 dir = (transform.position - this.Target.transform.position).normalized;

            if (isRanged) {
                Debug.Log("Im ranged");
                Instantiate(bullet, transform.position, Quaternion.Euler(dir), transform).GetComponent<BaseBullet>().DmgTarget(DoDamage(), Target); 
            }else{
                Target.takeDamage(DoDamage());
            };
        }
    }

    private IEnumerator AutoAtack()
    {
        while (isAtacking)
        {
            yield return new WaitForSeconds(atack_spd);

            if (isAtacking&&FindTarget())
            {
                Atack();
                /*
                if (Target == null)
                {
                    Debug.Log("Finding  new target");
                    if (FindTarget()==true)
                    {
                        Atack();
                    }
                }
                else
                {

                    Atack();
                }*/
            }
        }
    }
    public void DoAtack()
    {

        if (!isAtacking)
        {
            Atack();
            if (autoAtack)
            {
                isAtacking = true;
                StartCoroutine("AutoAtack");
            }
        }
        else
        {
            isAtacking=false;
        }


    }

}

public class EntityWithStats : BaseEntity
{

    public string main_stat;
    public Dictionary<string, float> stats = new Dictionary<string, float>()
                                    {
                                        { Const._str, 0},
                                        { Const._agi, 0},
                                        { Const._int, 0}
                                    }; // статы: сила, ловкость, интеллект
     public Dictionary<string, float> stats_d = new Dictionary<string, float>()
                                    {
                                        { Const._str, 0},
                                        { Const._agi, 0},
                                        { Const._int, 0}
                                    }; // прирост статов (стат_дельта)

    public override float AddDamage()
    {
        return stats[main_stat];
    }
}
