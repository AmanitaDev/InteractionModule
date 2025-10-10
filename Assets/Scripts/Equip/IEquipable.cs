using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Equip
{
    public interface IEquipable
    {
        public enum EQUIPTYPE
        {
            NULL,
            AXE,
            SHOVEL,
            BANANA,
            WOOD,
        }

        EQUIPTYPE GetEquipType();
        GameObject GetGameObject();
        void OnAttackInput();
        void OnHit();
    }
}