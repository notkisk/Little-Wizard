using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    public bool hasWon {  get; private set;}
    // Start is called before the first frame update
    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        capsuleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var enemies = FindObjectsOfType<Life>();
        Debug.Log(enemies.Length);
        if (!hasWon)
        {
            if (enemies.Length<1)
            {
                FindObjectOfType<AudioManager>().Play("Active");
                hasWon = true;
                capsuleCollider.enabled = true;
                anim.SetTrigger("Active");
            }
        }
    }
}
