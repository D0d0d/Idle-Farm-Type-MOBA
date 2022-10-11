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
    public GameObject hpBar;
    public CircleCollider2D AtackRadius;
    public GameObject bullet;

    private HashSet<Enemy> _enemies = new HashSet<Enemy>();
    protected Enemy Target;


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
        Vector3 newBar = hpBar.transform.localScale;
        newBar.x = hp / max_hp;
        hpBar.transform.localScale = newBar;
        if (hp < 0) { Destroy(this.gameObject); isDead = true; }
    }
    public bool FindTarget()
    {
        if (Target.isDead||Target==null)
        {
            Debug.Log("Finding target");
            Debug.Log("Its " + this._enemies.Count.ToString());
            float sqrLen = AtackRadius.radius*10f;

            foreach (Enemy t in _enemies)
            {
                float new_sqrLen = ( transform.position-t.transform.position ).sqrMagnitude;
                Debug.Log(new_sqrLen.ToString());
                if (new_sqrLen < sqrLen)
                {
                    sqrLen = new_sqrLen;
                    this.Target = t;
                    Debug.Log("Found new target!");
                }
            }
        }
        return !(Target.isDead || Target == null);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            _enemies.Add(enemy);
            if (Target == null) { Target = enemy; }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            _enemies.Remove(collision.gameObject.GetComponent<Enemy>());
            if (Target == collision.gameObject) { Target = null; }

        }
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
            yield return new WaitForSeconds(1.5f);

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
