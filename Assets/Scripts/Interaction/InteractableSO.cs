using UnityEngine;

namespace Interaction
{
    [CreateAssetMenu(fileName = "Interactable", menuName = "Scriptables/Create Interactable", order = 0)]
    public class InteractableSO : ScriptableObject
    {
        public string description;
    }
}