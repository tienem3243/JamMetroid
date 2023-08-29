using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableDictionary.Scripts;
using DG.Tweening;

public class EquipManager : MonoBehaviour
{
    [SerializeField] List<Slot> partSlot;
    [SerializeField] WeaponSwap swapAdapter;

    private void Start()
    {
        swapAdapter.Reset();
    }
    private void Update()
    {

    }
    [System.Serializable]
    class Slot
    {
        public string name;
        public bool isUsed;
        public Transform slotTrans;
        public Equipment equipmentHolder;


        public void SetUp(Equipment equipment)
        {
            equipmentHolder = equipment;
            isUsed = true;
            equipment.transform.parent = slotTrans;
            equipment.IsEquip = true;

        }
        public void RemoveEquip()
        {
            if (equipmentHolder == null) return;
            isUsed = false;
           
            equipmentHolder.IsEquip = false;
            equipmentHolder = null; 

        }
    }
    public List<Equipment> GetCurrentEquipmentList()
    {
        List<Equipment> obj = new List<Equipment>();
        partSlot.ForEach(x =>
        {
            if (x.equipmentHolder != null)
            obj.Add(x.equipmentHolder);
        });
        if (obj.Count == 0) return null;
        return obj;
    }
    Slot GetSlotByName(string slotName)
    {
        return partSlot.Find(x => x.name == slotName);
    }
    public bool ImplantAble(string partName)
    {
        Slot slot = partSlot.Find(x => x.name == partName);
        if (slot == null) return false;

        return !slot.isUsed;
    }
    public Transform GetTransformSlot(string slotName)
    {
        return partSlot.Find(x => x.name == slotName).slotTrans;
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
        else return;
        
    }

}
