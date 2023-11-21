using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float damage = 10f;
//    public Collider prepareCollider;
//    public Collider attackCollider;
//    public Animator anim;

//    void Start()
//    {
//        Collider[] colliders = GetComponents<colliders>();

//        if (colliders.Length >= 2)
//        {
//            prepareCollider = colliders[0];
//            attackCollider = colliders[1];
//        }
//    }
//    private void OnCollisionEnter2D(Collider2D collision)
//    {   
//        anim = GetComponent<Animator>();
//        anim.SetBool("isPreparing", false);
        
//        if (collision.GetComponent<Collider>() == prepareCollider)
//        {

//        }
//        else if(collision.GetComponent<Collider>() == attackCollider)
//        {

//        }
//    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    public float damage = 10f;
//    public Collider prepareCollider;
//    public Collider attackCollider;
//    public Animator anim;

//    void Start()
//    {
//        Collider[] colliders = GetComponents<colliders>();

//        if (colliders.Length >= 2)
//        {
//            prepareCollider = colliders[0];
//            attackCollider = colliders[1];
//        }
//    }
//    private void OnCollisionEnter2D(Collider2D collision)
//    {   
//        anim = GetComponent<Animator>();
//        anim.SetBool("isPreparing", false);
        
//        if (collision.collider == prepareCollider)
//        {

//        }
//        else if(collision.collider == attackCollider)
//        {

//        }
//    }
//}