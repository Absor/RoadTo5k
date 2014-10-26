using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightCharacterAnimatorScript : MonoBehaviour {

    public readonly int UP = 0, RIGHT = 1, DOWN = 2, LEFT = 3;

    private Animator animator;
    private Text text;

    void Awake()
    {
        animator = GetComponent<Animator>();
        text = GetComponentInChildren<Text>();
    }

    public void Reset()
    {
        Revive();
        Face(DOWN);
        Idle();
    }

    public int GetFacing()
    {
        return animator.GetInteger("direction");
    }

    public void Face(int direction)
    {
        animator.SetInteger("direction", direction);
    }

    public void Walk()
    {
        animator.SetBool("walking", true);
    }

    public void Idle()
    {
        animator.SetBool("walking", false);
    }

    public void Die()
    {
        animator.SetBool("dead", true);
    }

    public void Revive()
    {
        animator.SetBool("dead", false);
    }

    internal void Resurrect()
    {
        animator.SetBool("dead", false);
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }
}
