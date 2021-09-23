using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{

    public GameObject Particle;
    public GameObject Particle2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            Instantiate(Particle, Vector3.zero, Quaternion.identity);
            Instantiate(Particle2, Vector3.zero, Quaternion.identity);
        }
    }
}
