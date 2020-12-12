using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class Boundary{
    public float xMin, xMax, zMin, zMax;
}
public class ArkShip : MonoBehaviour
{

    [Header("Movimiento")]
    public float speed;
    public float tilt;
    public Rigidbody rigid;
    public Boundary boundary;

    [Header("Disparo")]
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

  

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void FixedUpdate(){
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movH, 0f, movV);
        rigid.velocity = movement * speed;
        rigid.position = new Vector3(Mathf.Clamp(rigid.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rigid.position.z, boundary.zMin, boundary.zMax));
        rigid.rotation = Quaternion.Euler( 0f, 0f, rigid.velocity.x * -tilt);
    }
}
