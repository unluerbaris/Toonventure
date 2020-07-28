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
        //Jump();
        //Climb();
        //Die();
    }

    private void MoveInput()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        mover.Move(controlThrow);
    }
}
