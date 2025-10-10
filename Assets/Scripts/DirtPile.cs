using System;
using DG.Tweening;
using Project.Equip;
using Project.Interaction;
using Project.Inventory.Interfaces;
using Project.Inventory.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project
{
    public class DirtPile : MonoBehaviour, IAttackable
    {
        public ItemData itemData;
        // this prefab will  be ready after 3 dig action
        // first to spawn, other two for raising it
        public GameObject hitParticle;
        public IEquipable.EQUIPTYPE equipTypeToAttack;
        public Transform childToRaise;

        private int countForRaise = 0;

        private void Start()
        {
            childToRaise.transform.DOLocalMoveY(-.1f, .2f);
        }

        public string GetInteractPrompt()
        {
            if (countForRaise < 2)
            {
                return $"Dig to raise";
            }
            else
            {
                return $"Plant sapling";
            }
        }

        public PlayerInteraction.ACTIONTYPE GetActionType()
        {
            return itemData.actionType;
        }

        public void OnAttack(IEquipable.EQUIPTYPE equipType, Vector3 hitPoint, Vector3 hitNormal)
        {
            if (equipTypeToAttack != equipType) return;

            if (countForRaise < 2)
            {
                // raise
                countForRaise++;
                childToRaise.transform.DOMoveY(childToRaise.transform.localPosition.y + .05f, .2f);
                Destroy(Instantiate(hitParticle, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up)), 1.0f);
            }
        }
    }
}