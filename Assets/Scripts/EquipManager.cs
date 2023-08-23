using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableDictionary.Scripts;
using DG.Tweening;

public class EquipManager : MonoBehaviour
{
    [SerializeField] List<Slot> partSlot;
    [System.Serializable]
    class Slot
    {
        public string name; 
        public bool isUsed;
        [SerializeField] Transform slotTrans;
        private Equipment equipmentHolder;
       

        public void SetUp(Equipment equipment)
        {
            equipmentHolder = equipment;
            isUsed = true;
            equipment.transform.parent = slotTrans;
            equipment.IsEquip = true;
            
        }
        public void RemoveEquip()
        {
            isUsed = false;

            equipmentHolder.IsEquip = false;
            
           
        }
    }
  
    Slot GetSlotByName(string slotName)
    {
        return partSlot.Find(x => x.name == slotName);
    }
    public void Equip(Equipment equipment, string partName)
    {
        var slot = GetSlotByName(partName);
        if (slot == null) return;

        slot.SetUp(equipment);


    }
    public void RemoveEquipOnSlot(string slotName)
    {
        var slot = GetSlotByName(slotName);
        if (slot != null) slot.RemoveEquip();
    }
}
