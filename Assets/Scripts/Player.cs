using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    public float Speed = 10f;
    public float JumpForce = 10f;
    public float FireSpeed = 20f;

    public GameObject BulletPrefab;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            Vector2 vector = new Vector2(Input.GetAxis("Horizontal") * Speed, rb.velocity.y);
            rb.velocity = vector;

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                CmdFire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    [Command]
    void CmdFire(Vector3 mousePosition)
    {
        Vector3 center = gameObject.GetComponent<Renderer>().bounds.center;
        GameObject bullet = Instantiate(BulletPrefab, center, Quaternion.identity);

        bullet.GetComponent<Bullet>().PlayerFrom = gameObject;

        Vector2 heading = mousePosition - center;
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = heading.normalized * FireSpeed;

        NetworkServer.Spawn(bullet);
    }
}
