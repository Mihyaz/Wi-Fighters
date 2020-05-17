using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isReloading", false);
    }
}
