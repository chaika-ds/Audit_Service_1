﻿using System.Reflection;

namespace AuditService.Tests.Extensions;

/// <summary>
/// InvokeExtensions class
/// </summary>
internal static class InvokeExtensions
{
    /// <summary>
    /// Invokes a private/public method on an object. Useful for unit testing.
    /// </summary>
    /// <typeparam name="T">Specifies the method invocation result type.</typeparam>
    /// <param name="obj">The object containing the method.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">Parameters to pass to the method.</param>
    /// <returns>The result of the method invocation.</returns>
    /// <exception cref="ArgumentException">When no such method exists on the object.</exception>
    /// <exception cref="ArgumentException">When the method invocation resulted in an object of different type, as the type param T.</exception>
    public static T Invoke<T>(this object obj, string methodName, params object[] parameters)
    {

        var methodInvokeResult = obj.GetType().InvokeMember(methodName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, Type.DefaultBinder, obj, parameters);
        
        if (methodInvokeResult is T resultInvoke)
        {
            return resultInvoke;
        }

        throw new ArgumentException(
            $"Bad type parameter. Type parameter is of type \"{typeof(T).Name}\", whereas method invocation result is of type \"{methodInvokeResult?.GetType().Name}\"");
    }
}