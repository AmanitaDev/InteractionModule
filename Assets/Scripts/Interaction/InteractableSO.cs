using System;
using UnityEngine;

namespace Interaction
{
    [CreateAssetMenu(fileName = "Interactable", menuName = "Scriptables/Create Interactable", order = 0)]
    public class InteractableSO : ScriptableObject
    {
        public ACTIONTYPE actionType;
        public string description;
    }

    // This enum is coming from the input system actions
    [Serializable]
    public enum ACTIONTYPE
    {
        Attack,
        Interact
    }
}