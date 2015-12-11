using UnityEngine;
using System.Collections;

public class ComponentsHelper {

    private static ComponentsHelper instance;

    public static ComponentsHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ComponentsHelper();
            }
            return instance;
        }
    }

    public T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
}
