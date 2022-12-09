using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);   
        if(clips.Length > 0)
        {
            AnimatorClipInfo jumpClipInfo = clips[0];

            PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
            float multiplier = jumpClipInfo.clip.length / player.JumpDuration;
            animator.SetFloat(PlayerAnimConstants.jumpMultiplier, multiplier);
        }
        
    }

}
