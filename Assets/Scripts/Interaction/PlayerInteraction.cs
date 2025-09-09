using System;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        public Camera mainCam;
        public float interactionDistance = 2f;

        public GameObject interactionUI;
        public TextMeshProUGUI interactionText;
        public Image interactionKey;

        private StarterAssetsInputs _input;

        private void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Update()
        {
            InteractionRay();
        }

        void InteractionRay()
        {
            Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
            RaycastHit hit;

            bool hitSomething = false;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    hitSomething = true;
                    interactionText.text = interactable.GetDescription();
                    //interactionKey.sprite = 

                    if (_input.interact)
                    {
                        _input.interact = false;
                        interactable.Interact();
                    }
                }
            }

            interactionUI.SetActive(hitSomething);
        }
    }
}