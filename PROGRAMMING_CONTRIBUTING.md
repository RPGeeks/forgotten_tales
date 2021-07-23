## Design-Team Style Guide ##


# Please follow the rules below:
- When adding scripts to the project you need to keep a hierarchical structure.
e.g.
```markdown
- Scritps
  - Characters
    - Player
      - InputController.cs
      - DialogueController.cs
      - InventoryController.cs
    - Enemy
      - AIController.cs
    - MovementController.cs
    - StatusController.cs
    - EquipmentController.cs
  - Managers
    - GameManager.cs
    - SoundManager.cs
    - NetworkManager.cs
```
- Use accesors and mutators instead of getters and setters.
- Class members should be private.
- Private members can be modified from the editor using `[SerializeField]`.
- **Naming**
  - variables in lowerCamelCase
  - methods in UpperCamelCase
  - interfaces should start with *I* (e.g. `ICanMove`)
  - enums should start with *E*
- Considering using events when necessary (e.g. when you kill an enemy) **avoid them in `Update` functions**, they are fairly costly in Unity.  
e.g.
```C#
class Foo : MonoBehaviour 
{
	/**
	 * Firsty add the members
	*/
	// This can be modified from the Editor
	[SerializeField] private int editableField;

	// This can never be changed "by hand"
	private int _privateField;

	// Add accesor and mutator for _privateField;
	public int PrivateFieldButPublic
	{ 
		get => _privateField;
		set => _privateField = value;
	}

	/**
	 * Delegate has a GameObject parameter to distinguish
	 * between multiple instances of class Foo, as the
	 * event is static. (instance might as well be a string)  
	*/
	public delegate void SomeEvent(GameObject instance);
    /**
     * Only methods that have the same signature 
     * as SomeEvent can subscribe to onEvent
    */
	public static event SomeEvent onEvent;

	// Fires event when colliding with something
	private void OnCollisionEnter(Collision collision)
	{
		// ? means don't crash if there is no-one subscribed
		onEvent?.Invoke(this);
	}
}

class Bar : MonoBehaviour
{
	[SerializeField] private Foo foo;
	
	/**
	 * Can be directly use like this
	 * Can only be set in this class
	*/
	public int Accumulator { get; private set; }

	void Start()
	{
		Foo.onEvent += OnDeath;
	}

	// Bar considers foo dies when firing 
	private void OnDeath(GameObject instance)
	{
		Foo fooInstance = instance as Foo;
		if (fooInstance is null) {
			return;
		} 
		Accumulator += fooInstance.PrivateFieldButPublic;
	}
}
```
- Do not create monolithic classes, instead extract functionality that might be reusable into an `Interface` (even if there currently no case for its reusability in the project).