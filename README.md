# UnitySettingUI
Setting UI framework for Unity

You can install this package by:
1. Opening your Unity project
2. Opening the Package Manager (Window > Package Manager)
3. Click the "+" button in the top-left
4. Select "Add package from git URL..."
5. Enter the repository URL: `https://github.com/unipotent/KokoroSharpUnity.git`
6. Click "Add"

## Dependencies
- [com.mackysoft.serializereference-extensions](https://github.com/mackysoft/Unity-SerializeReferenceExtensions) for a class selector in the inspector.
- [ayellowpaper.serialized-dictionary](https://github.com/ayellowpaper/SerializedDictionary) for a editing dictionaries in the inspector, for UI objects.

## Details
The idea is to have a [SettingConfig<T>](https://github.com/dot182/UnitySettingUI/blob/main/Runtime/Base/SettingConfig.cs) class for each of your settings, and have one [SettingUIMenu](https://github.com/dot182/UnitySettingUI/blob/main/Runtime/Menu/SettingUIMenu.cs) scriptable object with a list of your settings. <br>
You can either inherit from SettingUIMenu using the [IApplyAllSettings](https://github.com/dot182/UnitySettingUI/blob/main/Runtime/Menu/IApplyAllSettings.cs) interface to have a single callback to apply all your settings, or have your individual setting objects inherit from the SettingConfig<T> class and the [IOnSettingApply<T>](https://github.com/dot182/UnitySettingUI/blob/main/Runtime/Base/IOnSettingApply.cs) interface for individual callbacks to apply each setting. You can mix and match if you want. <br>

The `SettingUIMenu` has the setting list field, and also an `ISaveMethod` field. There is a `PlayerPrefsSaveMethod` included in the package, or you can create your own save methods. <br>
Each `SettingConfig<T>` object has a setting name, a save key, and a value field for whatever type it is. Thanks to the SerializeReferenceExtensions package, you can create the setting objects directly in the inspector, and you can edit the name and save key in the inspector. The basic types for the `SettingConfig<T>`like string, float, etc are included in the package for you.

The `UIPrefabDataSheet` scriptable object is to hold all your prefabs for UI. Right click for the, and press "Add default UI keys" to fill in the the default prefabs you need.
The `SettingUIConfig` class defines logic for ui. Included in the package are: Dropdown, slider, toggle, button. For some of these you might need to define logic:
- Dropdown: You should create a class that inherits from `SettingConfig<string>` or `StringConfig`, and also the `IDropdownOptions` interface to define what options will populate it.
- Button: You should create a class that inherits from SettingConfig and the `IOnButtonSettingClick` interface to define what happens on click.

Finally to instantiate your settings objects, you can use `SettingUIMenu.LoadUI(UIPrefabDataSheet data, Transform parent)`. 
Example:
```c#
public class SettingExample : MonoBehaviour
{
    public UIPrefabDataSheet PrefabDataSheet;
    public SettingUIMenu Menu;
    void Start()
    {
        Menu.LoadUI(PrefabDataSheet, this.transform);
    }
    void OnDisable()
    {
        if (Menu is IApplyAllSettings applyAllSettingCallback)
        {
            applyAllSettingCallback.ApplyAllSettings();
        }
        foreach(Transform child in this.transform) // you have to destroy the instantiated objects yourself.
        {
            Destroy(child.gameObject);
        }
    }
}
```