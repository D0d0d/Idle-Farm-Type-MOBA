using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float dmg;
    public float speed = 3f;
    protected Enemy Target;

    public void DmgTarget(float Dmg, Enemy target)
    {
        Target = target;
        dmg = Dmg;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() == Target)
        {
            Target.takeDamage(dmg);
            Destroy(this.gameObject);
        }
    }
    public void Update()
    {
         Vector3 vectorToTarget = Target.transform.position - transform.position;
         float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
         Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

         transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime*speed*10f);
         transform.Translate(vectorToTarget * speed * Time.deltaTime);
    }
}
