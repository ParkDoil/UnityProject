using UnityEngine;

public static class Extension
{
    public static void SetIKTransgormAndWeight(this Animator animator, AvatarIKGoal goal, Transform goalTransform, float weight = 1f)
    {
        animator.SetIKPositionWeight(goal, weight);
        animator.SetIKPosition(goal, goalTransform.position);

        animator.SetIKRotationWeight(goal, weight);
        animator.SetIKRotation(goal, goalTransform.rotation);
    }
}
