using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        if(horizontal != 0 || vertical != 0)
        {

            var v = vertical * speed;
            var transform = GetComponent<Transform>();
            transform.Rotate(new Vector3(0, horizontal * 3, 0));

            var rb = GetComponent<Rigidbody>();
            if (v != 0)
            {
                
                rb.velocity = transform.forward * v;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

    }

 
}
