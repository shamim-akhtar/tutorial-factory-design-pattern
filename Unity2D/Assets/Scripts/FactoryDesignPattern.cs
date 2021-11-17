using System;
using System.Collections.Generic;
using System.Reflection;

public class CObjectFactory<T> where T: class
{
  // The dictionary to hold the key value pairs 
  // for the typename and type for T and all
  // types for subclasses derived from T
  private Dictionary<string, Type> mObjectTypes = 
    new Dictionary<string, Type>();

  // The singleton instance.
  private static readonly 
    CObjectFactory<T> instance = new CObjectFactory<T>();

  // public property to get the Singleton instance.
  public static CObjectFactory<T> Instance
  {
    get
    {
      return instance;
    }
  }

  // the private constructor.
  // Here we create the dictionary.
  private CObjectFactory()
  {
    Assembly assem = Assembly.GetAssembly(typeof(T));
    Type[] types = assem.GetTypes();

    foreach (Type type in types)
    {
      if (
        type.IsClass &&       // only if class
        !type.IsAbstract &&   // only if not abstract
        (type == typeof(T) || // the base class.
        type.IsSubclassOf(typeof(T))) // all subclasses
        )
      {
        mObjectTypes.Add(type.Name, type);
      }
    }
  }

  // The public method to create 
  // instances of an object of type T or any
  // subclass of T.
  public T Create(string objectType)
  {
    if(!mObjectTypes.ContainsKey(objectType))
    {
      Console.WriteLine("Invalid type for CObjectFactory");
      return null;
    }

    Type type = mObjectTypes[objectType];
    T inst = (T)Activator.CreateInstance(type);
    return inst;
  }
}
