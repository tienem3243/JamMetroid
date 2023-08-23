using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    [SerializeField] Animator animator;
    [ContextMenu("swap")]
    public void SwapLayer(string name)
    {
       int index= animator.GetLayerIndex(name);
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, i == index ? 0 : 1);
        }
    }
}
