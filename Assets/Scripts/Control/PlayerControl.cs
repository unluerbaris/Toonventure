using UnityEngine;
using Toon.Movement;
using Toon.Core;

namespace Toon.Control
{
    public class PlayerControl : MonoBehaviour
    {
        Mover mover;
        BoxCollider2D myFeetCollider;
        AudioManager audioManager;

        float climbThrow;
        float controlThrow;

        private void Start()
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            mover = GetComponent<Mover>();
            myFeetCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (DodgeInput()) return; // don't do other actions while dodging

            MoveInput();
            JumpInput();
            ClimbInput();    
        }

        private void MoveInput()
        {
            controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
            mover.Move(controlThrow);
        }

        private void JumpInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                //Don't play audio if player is already on the air
                if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
                {
                    audioManager.PlaySound("jump");
                }
                mover.Jump();
            }
        }

        private void ClimbInput()
        {
            climbThrow = Input.GetAxis("Vertical"); //-1 to +1
            mover.Climb(climbThrow);
        }

        private bool DodgeInput()
        {
            if (Input.GetButton("Down") && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                mover.Dodge(true);
                return true;
            }

            mover.Dodge(false);
            return false;
        }
    }
}
