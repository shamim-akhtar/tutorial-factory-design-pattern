# Implement a Factory Design Pattern using C#

![Featured](https://faramira.com/wp-content/uploads/2021/11/Factory2-930x620.jpg)

In this tutorial, you will learn the factory design pattern and implement a factory design pattern using C# to create objects of similar types dynamically.

## Introduction

Sometimes, you may need to instantiate a derived object of a base class whose type is unknown at compile time. The factory design pattern comes in handy in such situations. 

Using this pattern, we can instantiate a derived class type at runtime based on a creation criterion. We achieve this instantiation by defining a method that allows this object creation via a Singleton object.

### Definition
A factory design pattern is a creational pattern. It uses a factory method to construct objects without knowing their class types. 

### Why named a Factory Design Pattern?
In software development, analogous to an actual factory, a factory represents a software object that creates new objects of similar behaviour on-demand. This on-demand creation happens by calling a factory method.

For example, you can have a factory class called GameObjectFactory with static methods to create other game objects like a Player, an Enemy, a Gun, a Bullet or Abilities.

The latter classes might have complex constructions that make obtaining an instance of that specific class difficult. The Factory, in this case, takes care of the object’s complex configuration (for example, adding to an object pool, adding to the physics engine, adding to separate render-bins, assigning different layers, and so on) and returns a reference to the created object.

## How to implement a Factory Design Pattern?
In a factory design pattern, you create an object, called the Factory, whose primary purpose is to make other objects of similar behaviour. By doing so, you avoid the problem of complex object instantiation by keeping them at a central place.

Note that the factory method doesn’t have to create new instances all the time. It can also return existing objects from a cache or from another source.

### Factory Design Pattern using C# Reflection
#### What is C# Reflection
Reflection is a mechanism to get type and metadata information in C#. It provides metadata on types at runtime. You can use Reflection to:

- dynamically create an instance of a given type,
- bind the type to an existing object, or 
- get the type from a current object and invoke its methods or access its fields and properties

The classes that give access to the metadata of a running program are in the System.Reflection namespace.

The System.Reflection namespace contains classes that allow obtaining information about an application and dynamically adding types, values, and objects. The classes in the System.Reflection namespace, together with System.Type enable you to obtain information about loaded assemblies and the types defined within them, such as classes, interfaces, and value types. You can also use Reflection to create type instances at run time and to invoke and access them.

We can use Reflection in the following cases:

- When you are required to view or use attribute information at runtime.
- When you need to examine various types in an assembly and instantiate these types.
- When you require late binding to methods and properties
- When you require creating new types at runtime and then performs some tasks using those types.

#### The Factory Code

```javascript
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

```
