using Project.Inventory;
using Project.Inventory.Interfaces;
using UnityEngine;

namespace Project.Equip
{
    [RequireComponent(typeof(Animator))]
    public class EquipableTool : MonoBehaviour, IEquipable
    {
        public IEquipable.EQUIPTYPE equipType;
        
        public float attactRate = .15f;
        private bool attacking;
        public float attackDistance = 3;

        [Header("Resource Gathering")] public bool doesGatherResources;

        [Header("Combat")] public bool doesDealDamage;
        public int damage = 10;

        //components
        private Animator _animator;
        private Camera cam;

        private void Awake()
        {
            //get our components
            _animator = GetComponent<Animator>();
            cam = Camera.main;
        }

        // called when we press the attack input
        public IEquipable.EQUIPTYPE GetEquipType()
        {
            return equipType;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void OnAttackInput()
        {
            if (!attacking)
            {
                attacking = true;
                _animator.SetTrigger("Attack");
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
                //did we hit a attackable?
                if (hit.collider.TryGetComponent(out IAttackable attackable))
                {
                    attackable.OnAttack(GetEquipType(), hit.point, hit.normal);
                }
            }
        }
    }
}