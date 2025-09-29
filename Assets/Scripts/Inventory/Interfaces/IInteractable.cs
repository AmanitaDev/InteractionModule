using UnityEngine;

namespace Project.Inventory.Interfaces
{
    public interface IInteractable
    {
        string GetInteractPrompt();
        void OnInteraction();
    }
}