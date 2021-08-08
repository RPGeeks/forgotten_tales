using RPGeeks.ItemHandlers;
using RPGeeks.Items;
using UnityEngine;

public abstract class InteractionHandler : MonoBehaviour
{
    public virtual void Accept(Interactible interactible)
    {
        interactible.Visit(this);
    }

    public virtual void Accept(Pickup pickup)
    {
        if (this as IItemsHandler != null)
        {
            pickup.Visit(this);
        }
    }
}
