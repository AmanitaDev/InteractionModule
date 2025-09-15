using System;
using IPD;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

namespace Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        public Camera mainCam;
        public float interactionDistance = 2f;

        public GameObject interactionUI;
        public TextMeshProUGUI interactionText;
        public InputPromptImage inputPromptImage;
        
        [SerializeField]
        private TextMeshProUGUI controlSchemeText;
        [SerializeField]
        private TextMeshProUGUI deviceNameText;
        [SerializeField]
        private TextMeshProUGUI devicePathText;

        private InputDevice currentDevice = null;

        private StarterAssetsInputs _input;
        private const string XELU_STYLE = "xelu";
        private const string JSTATZ_STYLE = "JStatz";

        private Interactable interactable;

        private async void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            
            // Load input prompt data
            await InputPromptUtility.Load ( );

            // Update the UI
            DebugControlSchemeChange ( InputUser.all [ 0 ], InputUser.all [ 0 ].pairedDevices [ 0 ] );
        }
        
        private void OnEnable ( )
        {
            // Add subscriptions
            InputUser.onChange += OnInputDeviceChange;
        }

        private void OnDisable ( )
        {
            // Remove subscriptions
            InputUser.onChange -= OnInputDeviceChange;
        }
        
        /// <summary>
        /// This input is coming from PlayerInput component
        /// </summary>
        /// <param name="value"></param>
        public void OnInteract(InputValue value)
        {
            Debug.LogError("interact");
            if (interactable == null) return;
            
            interactable.Interact();
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
                interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    hitSomething = true;
                    interactionText.text = interactable.GetDescription();
                    inputPromptImage.Initialize(new DisplaySettingsModel
                    {
                        Action = interactable.GetActionType(),
                        Style = XELU_STYLE
                    });
                }
            }

            interactionUI.SetActive(hitSomething);
        }
        
        private void OnInputDeviceChange ( InputUser user, InputUserChange change, InputDevice device )
        {
            // Check for device change
            if ( change == InputUserChange.DevicePaired )
            {
                // Store current device
                currentDevice = device;
            }
            else if ( change == InputUserChange.DeviceUnpaired )
            {
                // Clear current device
                currentDevice = null;
            }
            else if ( change == InputUserChange.ControlSchemeChanged )
            {
                // Update the UI
                DebugControlSchemeChange ( user, currentDevice );
            }
        }
        
        private void DebugControlSchemeChange ( InputUser user, InputDevice device )
        {
            // Set control scheme
            controlSchemeText.text = $"Control Scheme:<color=#FFC300> {user.controlScheme.Value.name}";

            // Set device name
            deviceNameText.text = $"Device Name:<color=#FFC300> {device.displayName}";

            // Set device path
            devicePathText.text = $"Device Path:<color=#FFC300> {device.path}";
        }
    }
}