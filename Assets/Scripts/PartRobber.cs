using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartRobber : MonoBehaviour
{
    [SerializeField] EquipManager equipmentManager;
    [SerializeField] float sizeDetect;
    [SerializeField] CoroutineOD coroutineOD;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sizeDetect);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            List<GameObject> ignoreList=new List<GameObject>();
            if(equipmentManager.GetCurrentEquipmentList()!=null)
            {
                ignoreList = equipmentManager.GetCurrentEquipmentList().Select(x => x.gameObject).ToList<GameObject>();
            }
            var target = Util.GetTransformFromTagName("Equipment", false, true, transform, sizeDetect, ignoreList);
            
            if (target == null) return;

            Equipment equipment = target.GetComponent<Equipment>();
          
            if (equipment.IsEquip) return;
         
            if (!equipmentManager.ImplantAble(equipment.SlotRequire)) return;
            coroutineOD.StartTimer(0.5f, 
                (x) => CollectAniamtion(target, equipmentManager.GetTransformSlot(equipment.SlotRequire)),
                () => Implant(equipment));
           
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            equipmentManager.RemoveEquipOnSlot("Head");
        }
    }

    private void Implant(Equipment equipment)
    {
        equipmentManager.Equip(equipment, equipment.SlotRequire);
    }

    void CollectAniamtion(Transform equipment, Transform target)
    {
       
            equipment.transform.position = Vector3.MoveTowards(equipment.position, target.position, 0.1f);   
    }

}
