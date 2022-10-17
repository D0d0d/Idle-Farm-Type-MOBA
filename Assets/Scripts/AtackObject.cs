using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackObject : MonoBehaviour
{
    public CircleCollider2D AtakRadius;
    public HashSet<BaseEntity> _enemies = new HashSet<BaseEntity>();
    public BaseEntity Target;
    private string side;
    void Start()
    {
        side = this.tag;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!side.Contains(collision.gameObject.tag))
        {
            BaseEntity enemy = collision.gameObject.GetComponent<BaseEntity>();
            _enemies.Add(enemy);
            if (Target == null) { Target = enemy; }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (!side.Contains(collision.gameObject.tag))
        {
            _enemies.Remove(collision.gameObject.GetComponent<BaseEntity>());
            if (Target == collision.gameObject) { Target = null; }

        }
    }
}
