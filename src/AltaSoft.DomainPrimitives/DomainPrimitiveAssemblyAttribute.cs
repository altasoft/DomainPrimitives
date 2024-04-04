using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an attribute that is applied to assemblies to indicate that they are part of a DomainPrimitive assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class DomainPrimitiveAssemblyAttribute : Attribute;
