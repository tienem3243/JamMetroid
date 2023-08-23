using UnityEngine;

[CreateAssetMenu(menuName = ("GameName/WeaponConfig"))]
         public class WeaponConfig : ScriptableObject
{
    [SerializeField] AnimationClip attackAnimation;
    [SerializeField] AnimationClip reloadAnimation;
    [SerializeField] float timeBetweenAnimationCycles = .1f;
    [SerializeField] float maxAttackRange = 2f;
    [SerializeField] float damage = 10f;

    // add public getters i.g. 
    public AnimationClip GetAttackAnimation()
    {
        return attackAnimation;
    }
}