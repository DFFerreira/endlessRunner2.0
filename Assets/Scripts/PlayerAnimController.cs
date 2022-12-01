using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimController : MonoBehaviour
{
    PlayerController player;
    [SerializeField] Animator animator;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    void Update()
    {
        animator.SetBool("isJumping", player.IsJumping);
    }

}
