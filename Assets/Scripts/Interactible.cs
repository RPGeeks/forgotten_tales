using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public abstract void Visit(InteractionHandler handler);
}
