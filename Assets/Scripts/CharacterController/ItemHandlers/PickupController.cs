using RPGeeks.Inventories;
using RPGeeks.ItemHandlers;
using RPGeeks.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGeeks.CharacterController
{
    [RequireComponent(typeof(SphereCollider))]
    class PickupController : InteractionHandler, IItemsHandler
    {
        [Header("Range")]
        [SerializeField] [Min(0.5f)] private float pickupRange = 5.0f;
        [SerializeField] private List<Pickup> pickupsInRange;
        private SphereCollider _sphereCollider;

        // TODO remove this and move to CharacterInputFeed
        [SerializeField] private KeyCode pickupKey = KeyCode.E;

        [Header("Inventory")]
        [SerializeField] private Inventory inventory;

        public Inventory Inventory { get => inventory; private set => inventory = value; }

        private void Awake()
        {
            pickupsInRange = new List<Pickup>();
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
            foreach (Pickup pickup in pickupsInRange)
            {
                // TODO no visual feedback 
                // Data exists but UI is not updated properly

                // TODO fix bug -> 
                Accept(pickup);
            }
            pickupsInRange.Clear();
        }

        private void OnTriggerStay(Collider other)
        {
            // TODO maybe storing GameObjects in pickupsInRange might be more efficient
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup != null)
            {
                if (!pickupsInRange.Contains(pickup))
                {
                    pickupsInRange.Add(pickup);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup != null)
            {
                if (pickupsInRange.Contains(pickup))
                {
                    pickupsInRange.Remove(pickup);
                }
            }
        }
    }
}
