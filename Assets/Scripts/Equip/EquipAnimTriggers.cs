using UnityEngine;

namespace Project.Equip
{
    public class EquipAnimTriggers : MonoBehaviour
    {
        public EquipManager equipManager;
        
        public void OnHit()
        {
            equipManager.curEquipable.OnHit();
        }
    }
}