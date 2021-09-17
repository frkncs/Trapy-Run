using System.Collections;
using System.Collections.Generic;
using States.Player;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController playerController) : base(playerController)
    {
        playerController.PlayIdleAnim();
    }

    public override void Update(PlayerController playerController)
    {
        
    }

    public override void FixedUpdate(PlayerController playerController)
    {
        
    }

    public override void OnCollisionEnter(PlayerController playerController)
    {
        
    }

    public override void OnCollisionExit(PlayerController playerController)
    {
        
    }

    public override void OnTriggerEnter(PlayerController playerController)
    {
        
    }

    public override void OnTriggerExit(PlayerController playerController)
    {
        
    }
}
