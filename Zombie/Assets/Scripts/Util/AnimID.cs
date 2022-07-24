using UnityEngine;
public static class PlayerAnimID
{
    public static readonly int DIE = Animator.StringToHash("Die");
    public static readonly int RELOAD = Animator.StringToHash("Reload");
    public static readonly int MOVE = Animator.StringToHash("Move");
}

public static class ZombieAnimID
{
    public static readonly int DIE = PlayerAnimID.DIE;
    public static readonly int HASTARGET = Animator.StringToHash("HasTarget");
}
