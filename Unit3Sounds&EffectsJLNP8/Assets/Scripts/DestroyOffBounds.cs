using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision) 
    {
        Destroy(collision.gameObject);
    }
}
