using Project.Inventory;
using Project.Inventory.Interfaces;
using UnityEngine;

namespace Project.Equip
{
    public class EquipTool : Equip
    {
        public float attactRate = .15f;
        private bool attacking;
        public float attackDistance = 3;

        [Header("Resource Gathering")] public bool doesGatherResources;
        public TOOL ToolType;

        [Header("Combat")] public bool doesDealDamage;
        public int damage = 10;

        public enum TOOL
        {
            NULL,
            AXE,
            SHOVEL,
        }

        //components
        private Animator anim;
        private Camera cam;

        private void Awake()
        {
            //get our components
            anim = GetComponent<Animator>();
            cam = Camera.main;
        }

        // called when we press the attack input
        public override void OnAttackInput()
        {
            if (!attacking)
            {
                attacking = true;
                anim.SetTrigger("Attack");
                Invoke(nameof(OnCanAttack), attactRate);
            }
        }

        // called when we're able to attack again
        void OnCanAttack()
        {
            attacking = false;
        }

        // called when the animation impacts
        public void OnHit()
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, attackDistance))
            {
                //did we hit a resource?
                if (doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
                {
                    resource.Gather(ToolType, hit.point, hit.normal);
                }
                // did we hit a damagable?
                if(doesDealDamage && hit.collider.GetComponent<IDamagable>() != null)
                {
                    hit.collider.GetComponent<IDamagable>().TakePhysicalDamage(damage);
                }
            }
        }
    }
}