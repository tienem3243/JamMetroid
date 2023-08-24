using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : CustomBehaviour
{
    [SerializeField]string slotRequire;
    [SerializeField] bool synWithBodyAnim;
    [SerializeField] string layerName;
    bool isEquip;
    public string SlotRequire { get => slotRequire; }
    public bool SynWithBodyAnim { get => synWithBodyAnim; }
    public string LayerName { get => layerName;  }
    public bool IsEquip
    {
        get => isEquip;
        set
        {
            isEquip = value;
            if (isEquip == true)
            {
                GetComponent<Collider>().enabled = false;
                transform.localPosition = Vector3.zero;
                transform.rotation = new Quaternion(0, 0, 0, 0);
                rigidbody.isKinematic = true;
            }
            else
            {
                GetComponent<Collider>().enabled = true;
                transform.parent = null;
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
            }
        }
    }

    public abstract void Use();
   
}
