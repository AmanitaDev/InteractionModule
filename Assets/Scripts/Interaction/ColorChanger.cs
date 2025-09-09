using UnityEngine;

namespace Interaction
{
    public class ColorChanger : Interactable
    {
        Material mat;
 
        private void Start() {
            mat = GetComponent<MeshRenderer>().material;
        }
        
        public override void Interact()
        {
            mat.color = Random.ColorHSV();
        }
    }
}