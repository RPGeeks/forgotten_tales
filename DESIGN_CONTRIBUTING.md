## Design-Team Style Guide ##


# Please follow the rules below:
- When adding assets to the project you need to keep a hierarchical structure.
- If the assets you add may be added to `GameObject`s you need to place the root folder of your structure (if not already existent) into `Resources` folder. 
_Why? Because some of our programmers may not want to use drag-n-drop to assign assets to `GameObject`s and instead use `Resources.Load<SomeType>(path);`_
*scripts are not assets so they will not be placed in `Resources` folder*.
- When naming you hierarchical structure of folders *do not add redundant information in names*, it is not necessary.
- The file *naming convention* is UpperCamelCase.
e.g.    
```markdown
- Resources
  - Objects
    - Houses
      - Basic.obj
      - Inn.obj
      - TownHall.obj
    - Items
      - Weapons
        - Sword.obj
        - Bow.obj
      - Armours
        - Basic.obj
        - ~~BasicArmour.obj~~ // Bad
        - Enchanted.obj
        - Demonic.obj
  - Sprites
    - UI
      - Menus
        - Button.jpg
        - Board.jpg
    - Skybox
      - Sunny.jpg
  - Materials
  - Prefabs
```