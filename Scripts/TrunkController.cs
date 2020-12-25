using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D body;

    private void Update()
    {

        body.velocity = new Vector3(0f, -9.8f, 0f);        
    }
}
