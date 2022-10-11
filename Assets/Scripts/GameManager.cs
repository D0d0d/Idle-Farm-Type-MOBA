using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public CreepSpawn EnemySpawn;
    public GameObject Sword;
    void Start()
    {
        StartCoroutine("NewCreepsWave");
    }

    IEnumerator NewCreepsWave()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(8f);
            EnemySpawn.Spawn(Sword, Sword, canvas.transform);

        }
    }
}