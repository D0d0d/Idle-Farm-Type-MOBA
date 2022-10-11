using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawn : MonoBehaviour
{
    public GameObject Sword1;
    public GameObject Sword2;
    public GameObject Sword3;
    public GameObject Sword4;
    public GameObject Ranged;

    public void Spawn(GameObject sword, GameObject ranged, Transform _transform)
    {
        Instantiate(sword, Sword1.transform.position, transform.rotation, _transform);
        Instantiate(sword, Sword2.transform.position, transform.rotation, _transform);
        Instantiate(sword, Sword3.transform.position, transform.rotation, _transform);
        Instantiate(sword, Sword4.transform.position, transform.rotation, _transform);

        Instantiate(ranged,Ranged.transform.position, transform.rotation, _transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
