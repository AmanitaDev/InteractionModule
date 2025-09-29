using UnityEngine;

namespace Project.Equip
{
    public class EquipFood : Equip
    {
        // called when we press the attack input
        public override void OnAttackInput()
        {
            // TODO check inventory count
            // consume food
            // remove one from inventory
            // add energy
            Debug.LogError("Add energy");
        }
    }
}