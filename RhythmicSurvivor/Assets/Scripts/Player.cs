using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 dir = new Vector3(-1f, 0f, 0f);
            Quaternion look = Quaternion.AngleAxis(270, transform.up);
            transform.rotation = look;
            transform.position += dir;
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 dir = new Vector3(1f, 0f, 0f);
            Quaternion look = Quaternion.AngleAxis(90, transform.up);
            transform.rotation = look;
            transform.position += dir;
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 dir = new Vector3(0f, 0f, 1f);
            Quaternion look = Quaternion.AngleAxis(0, transform.up);
            transform.rotation = look;
            transform.position += dir;
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 dir = new Vector3(0f, 0f, -1f);
            Quaternion look = Quaternion.AngleAxis(180, transform.up);
            transform.rotation = look;
            transform.position += dir;
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("attack", true);
        }
    }
}
