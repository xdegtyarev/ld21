﻿/*************************************************************************
 *           Registered Component (c) by ClockStone 2011                    *
 * 
 * A component derived from this class will get registered in a global 
 * component reference list. This way one can receive a list of 
 * registered components much faster than by using GameObject.FindObjectsOfType
 * 
 * Usage:
 * 
 * Derive your component class from RegisteredComponent instead of
 * MonoBehaviour.
 * 
 * Use RegisteredComponentController.GetAllOfType<MyType>() to retrieve a
 * list of all present components of type MyType. Note that this will also 
 * return all references of classes derived from MyType.
 * 
 * Works with PoolableObject if Awake and OnDestroy messages are sent.
 * 
 * ***********************************************************************
*/


using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 1591 // undocumented XML code warning


/// <summary>
/// Derive your MonoBehaviour class from RegisteredComponent and all references to instances of this
/// component will be saved in an internal array. Use <see cref="RegisteredComponentController.GetAllOfType()"/>
/// to retrieve this array, which is much faster than using Unity's GameObject.FindObjectsOfType() function.
/// </summary>
public abstract class RegisteredComponent : MonoBehaviour
{
    bool isRegistered = false;
    bool isUnregistered = false;

    protected virtual void Awake()
    {
        if ( !isRegistered )
        {
            RegisteredComponentController._Register( this );
            isRegistered = true;
            isUnregistered = false;
        } else
            Debug.LogWarning( "RegisteredComponent: Awake() / OnDestroy() not correctly called. Object: " + name);
    }

    protected virtual void OnDestroy()
    {
        if ( isRegistered && !isUnregistered )
        {
            RegisteredComponentController._Unregister( this );
            isRegistered = false;
            isUnregistered = true;
        }
        else
        {
            bool alreadyUnregisteredProperly = !isRegistered && isUnregistered;

            if ( !alreadyUnregisteredProperly ) // for poolable objects OnDestroy() can get called multiple times
            {
                Debug.LogWarning( "RegisteredComponent: Awake() / OnDestroy() not correctly called. Object: " + name + " isRegistered:" + isRegistered + " isUnregistered:" + isUnregistered );
            }
        }
    }
}

/// <summary>
/// This controller provides fast access to all currently existing RegisteredComponent instances. 
/// </summary>
/// <remarks>
/// The function <see cref="RegisteredComponentController.GetAllOfType()"/> is understood as a replacement for Unity's 
/// slow GameObject.FindObjectsOfType() function.
/// </remarks>
static public class RegisteredComponentController
{
    public class InstanceContainer : HashSet<RegisteredComponent>
    {

    }

    static Dictionary<Type, InstanceContainer> _instanceContainers = new Dictionary<Type, InstanceContainer>();

    /// <summary>
    /// Retrieves an array of all currently existing instances of the class <c>T</c>, 
    /// where <c>T</c> must be a <see cref="RegisteredComponent"/>
    /// </summary>
    /// <typeparam name="T">a class derived from <see cref="RegisteredComponent"/></typeparam>
    /// <returns>
    /// The array of instances.
    /// </returns>
    static public T[] GetAllOfType<T>() where T : RegisteredComponent
    {
        InstanceContainer container;

        if ( !_instanceContainers.TryGetValue( typeof( T ), out container ) )
        {
            return new T[0];
        }

        var array = new T[ container.Count ];
        int count = 0;

        foreach ( RegisteredComponent cpnt in container )
        {
            array[ count++ ] = (T)cpnt;
        }

        return array;
    }

    static private InstanceContainer _GetInstanceContainer( Type type )
    {
        InstanceContainer container;

        if ( _instanceContainers.TryGetValue( type, out container ) )
        {
            return container;
        }
        else
        {
            container = new InstanceContainer();
            _instanceContainers.Add( type, container );
        }

        return container;
    }

    static private void _RegisterType( RegisteredComponent component, Type type )
    {
        InstanceContainer container = _GetInstanceContainer( type );
        if ( !container.Add( component ) )
        {
            Debug.LogError( "RegisteredComponentController error: Tried to register same instance twice" );
        }

        //Debug.Log( "Registered " + type.Name + ": " + component.gameObject.name );
    }

    static internal void _Register( RegisteredComponent component )
    {
        Type type = component.GetType();

        do
        {
            _RegisterType( component, type );
            type = type.BaseType;

        } while ( type != typeof( RegisteredComponent ) );

        //Debug.Log( "Registered " + component.GetType().Name + ": " + component.gameObject.name );
    }

    static internal void _UnregisterType( RegisteredComponent component, Type type )
    {
        InstanceContainer container = _GetInstanceContainer( type );
        if ( !container.Remove( component ) )
        {
            Debug.LogError( "RegisteredComponentController error: Tried to unregister unknown instance" );
        }

        //Debug.Log( "Unregistered " + type.Name + ": " + component.gameObject.name );
    }

    static internal void _Unregister( RegisteredComponent component )
    {
        Type type = component.GetType();

        do
        {
            _UnregisterType( component, type );
            type = type.BaseType;

        } while ( type != typeof( RegisteredComponent ) );

        //Debug.Log( "Unregistered " + component.GetType().Name + ": " + component.gameObject.name );
    }
}
