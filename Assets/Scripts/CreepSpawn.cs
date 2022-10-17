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
    public Transform Destination; 
    public void Spawn(GameObject sword, GameObject ranged, Transform _transform)
    {
        Instantiate(sword, Sword1.transform.position, transform.rotation, _transform).GetComponent<LaneCreep>().Set(Destination, this.tag);
        Instantiate(sword, Sword2.transform.position, transform.rotation, _transform).GetComponent<LaneCreep>().Set(Destination, this.tag);
        Instantiate(sword, Sword3.transform.position, transform.rotation, _transform).GetComponent<LaneCreep>().Set(Destination, this.tag);
        Instantiate(sword, Sword4.transform.position, transform.rotation, _transform).GetComponent<LaneCreep>().Set(Destination, this.tag);
        Instantiate(ranged,Ranged.transform.position, transform.rotation, _transform).GetComponent<LaneCreep>().Set(Destination, this.tag);
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
