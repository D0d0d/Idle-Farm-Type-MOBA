using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float dmg;
    public float speed = 5f;
    protected BaseEntity Target;

    public void DmgTarget(float Dmg, BaseEntity target)
    {
        Target = target;
        dmg = Dmg;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BaseEntity>() == Target)
        {
            Target.takeDamage(dmg);
            Destroy(this.gameObject);
        }
    }
    public void Update()
    {
        if (!Target.isDead)
        {
            var lookPos = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.LookAt(Target.transform, Vector3.forward);
            //transform.Translate(vectorToTarget * speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
