using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    public AnimationClip weaponAnimationClip;
     
    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;

    protected int weaponIndex;

    public void Start()
    {
        animator = GetComponent<Animator>();
      

        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
           
            animatorOverrideController["Punch"] = weaponAnimationClip;
        }
    }
}