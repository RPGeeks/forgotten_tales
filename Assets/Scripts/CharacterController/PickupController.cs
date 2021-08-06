using RPGeeks.Inventories;
using RPGeeks.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGeeks.CharacterController
{
    [RequireComponent(typeof(SphereCollider))]
    class PickupController : MonoBehaviour
    {
        [Header("Range")]
        [SerializeField] [Min(0.5f)] private float pickupRange = 5.0f;
        private SphereCollider _sphereCollider;
        private List<Pickup> _pickupsInRange;

        // TODO remove this and move to CharacterInputFeed
        [SerializeField] private KeyCode pickupKey = KeyCode.E;

        [Header("Inventory")]
        [SerializeField] private Inventory inventory;

        private void Awake()
        {
            _pickupsInRange = new List<Pickup>();
        }

        private void Start()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = pickupRange;
            _sphereCollider.isTrigger = true;

            inventory = inventory != null
                ? inventory
                : Resources.Load<Inventory>("Prefabs/Data/Inventory");
        }

        private void Update()
        {
            if (Input.GetKeyDown(pickupKey))
            {
                PickupItems();
            }
        }

        private void PickupItems()
        {
            foreach (Pickup pickup in _pickupsInRange)
            {
                // TODO no visual feedback 
                // Data exists but UI is not updated properly
                ItemSlot remaining = inventory.ItemContainer.AddItem(pickup.Item);

                if (remaining != null && (remaining.Quantity == 0 || remaining.IsEmpty))
                {
                    Destroy(pickup.gameObject);
                    // TODO maybe add specific sound
                }
            }
            _pickupsInRange.Clear();
        }

        private void OnTriggerStay(Collider other)
        {
            // TODO maybe storing GameObjects in pickupsInRange might be more efficient
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup != null)
            {
                if (!_pickupsInRange.Contains(pickup))
                {
                    _pickupsInRange.Add(pickup);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup != null)
            {
                if (_pickupsInRange.Contains(pickup))
                {
                    _pickupsInRange.Remove(pickup);
                }
            }
        }
    }
}
