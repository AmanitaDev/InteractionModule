using UnityEngine;

namespace Inventory.Interfaces
{
    public interface IInteractable
    {
        string GetInteractPrompt();
        void OnInteraction();
    }
}