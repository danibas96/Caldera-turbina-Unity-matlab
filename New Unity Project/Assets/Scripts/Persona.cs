using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persona : MonoBehaviour
{
    public float speedMove = 5.0f;
    public float speedRot = 200.0f;
    private Animator anim;
    public float x, y;
    public Rigidbody rb;
    private bool m_Jump;
    public float force_jump = 8f;


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
        transform.Rotate(0, x * Time.deltaTime * speedRot, 0);
        transform.Translate(0, 0, y * Time.deltaTime * speedMove);
       

    }
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

       

    }
   
}
