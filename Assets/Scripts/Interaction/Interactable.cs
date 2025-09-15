using UnityEngine;

namespace Interaction
{
    public class Interactable: MonoBehaviour
    {
        public InteractableSO interactableSo;
        
        public string GetDescription()
        {
            return interactableSo.description;
        }
        
        public virtual void Interact()
        {
            
        }

        public string GetActionType()
        {
            return interactableSo.actionType.ToString();
        }
    }
}