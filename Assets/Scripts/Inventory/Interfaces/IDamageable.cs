using UnityEngine;

namespace Project.Inventory.Interfaces
{
    public interface IDamagable
    {
        void TakePhysicalDamage(int damageAmount);
    }
}