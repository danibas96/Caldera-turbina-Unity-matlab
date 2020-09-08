using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person2 : MonoBehaviour
{
    public float speedMove1 = 5.0f;
    public float speedRot1 = 200.0f;
    private Animator anim;
    public float x, y;
    public Rigidbody rb;
    //private bool m_Jump;
   // public float force_jump = 8f;


    /// //
   // public GameObject explosion;
    public ContactPoint contact;
    public float TiempodeVida;


    void Start()
    {


        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, x * Time.deltaTime * speedRot1, 0);
        transform.Translate(0, 0, y * Time.deltaTime * speedMove1);


    }
    void Update()
    {
        x = Input.GetAxis("Horizontal2");
        y = Input.GetAxis("Vertical2");
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);



    }
}
