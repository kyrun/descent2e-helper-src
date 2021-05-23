using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Simple Messenger system for Depedency Injection.
/// </summary>
public static class Messenger
{
	/// <summary>
	/// Event Handler delegate.
	/// </summary>
	public delegate void MessengerEventHandler<T>(T m) where T : Message;

	private static Dictionary<System.Type, System.Delegate> _subscribersDictionary = new Dictionary<System.Type, System.Delegate>();

	/// <summary>
	/// Subscribe class method to a specific message via delegate
	/// </summary>
	/// <param name="subscriber">delegate method</param>
	/// <typeparam name="T">message type</typeparam>
	public static void Subscribe<T>(MessengerEventHandler<T> subscriber) where T : Message
	{
		if (subscriber == null) throw new System.ArgumentException("subscriber is invalid");
		var key = typeof(T);
		if (_subscribersDictionary.ContainsKey(key))
		{
			_subscribersDictionary[key] = System.Delegate.Combine(_subscribersDictionary[key], subscriber);
		}
		else
		{
			_subscribersDictionary.Add(key, subscriber);
		}
	}

	/// <summary>
	/// Unsubscribe class to a specific message via delegate
	/// </summary>
	/// <param name="subscriber">delegate method</param>
	/// <typeparam name="T">message type</typeparam>
	public static void UnSubscribe<T>(MessengerEventHandler<T> subscriber) where T : Message
	{
		if (subscriber == null) throw new System.ArgumentException("Subscriber is invalid");
		var key = typeof(T);
		if (_subscribersDictionary.ContainsKey(key))
		{
			var value = _subscribersDictionary[key];
			value = System.Delegate.Remove(value, subscriber);
			if (value == null) _subscribersDictionary.Remove(key);
			else _subscribersDictionary[key] = value;
		}
	}

	/// <summary>
	/// Messenger sends message
	/// </summary>
	/// <param name="m">The message object</param>
	/// <typeparam name="T">message type</typeparam>
	public static void Send<T>(T m) where T : Message
	{
		var key = typeof(T);
		if (_subscribersDictionary.ContainsKey(key))
			_subscribersDictionary[key].DynamicInvoke(m);
	}
}

public abstract class Message
{

}