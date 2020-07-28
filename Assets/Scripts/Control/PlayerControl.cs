using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Mover mover;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    private void Update()
    {
        //if (!isAlive) { return; }
        MoveInput();
        JumpInput();
        //Climb();
        //Die();
    }

    private void MoveInput()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        mover.Move(controlThrow);
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            mover.Jump();
        }
    }
}
