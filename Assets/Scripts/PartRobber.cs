using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartRobber : MonoBehaviour
{
   [SerializeField] EquipManager equipmanager;
    [SerializeField] float sizeDetect;
    [SerializeField] WeaponSwap swap;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sizeDetect);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var target = Util.GetTransformFromTagName("Equipment", false, true, transform,sizeDetect);
            Debug.Log(target);
            if (target == null) return;
            Equipment equipment = target.GetComponent<Equipment>();
            equipmanager.Equip(equipment, equipment.SlotRequire);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            equipmanager.RemoveEquipOnSlot("Hand");
        }
    }
  
}
