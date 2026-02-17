---
name: domainprimitive-builder
description: Helps create and implement DomainPrimitive types correctly using the AltaSoft DomainPrimitives framework. Guides the agent to generate generator-compatible primitives, add proper validation logic, apply normalization when needed, and follow all structural and domain invariant rules required for safe DomainPrimitive design.
---

# domainprimitive-builder — Skill Specification

## Language & Platform Scope

This skill applies only to C# projects using the AltaSoft DomainPrimitives framework and its source generator.

Agent must:

- Generate C# code only
- Use modern C# syntax compatible with the project target framework and if applicable use C# Copilot instructions
- Follow C# type system rules for structs, classes, attributes, and static methods
- Assume .NET and Roslyn source generator environment
- prefer var over explicit types when the type is obvious from the right-hand side

Agent must NOT:

- Generate implementations in other languages
- Suggest non-C# patterns (Java/Kotlin/TypeScript/etc.)
- Translate DomainPrimitive patterns into other ecosystems

All templates, rules, and examples in this skill are C#-specific.

---

## Purpose

This skill guides the agent to correctly create and maintain DomainPrimitive types using the AltaSoft DomainPrimitives framework. It ensures primitives are generator-compatible, domain-safe, and follow strict structural and validation rules.

Use this skill when:

- Creating a new DomainPrimitive type
- Adding or modifying validation logic
- Adding normalization (Transform)
- Applying DomainPrimitive attributes
- Refactoring primitive invariants
- Creating chained primitives

---

## Clarification-First Generation Policy

When the user asks to create a DomainPrimitive and the domain rules are not fully specified, the agent must **ask clarification questions before generating code**.

Agent must not generate a primitive implementation unless it is confident about the domain invariants and validation rules.

If uncertainty exists, the agent must pause generation and ask targeted questions.

---

### When Clarification Is Required

Agent must ask questions when any of the following are unclear:

* allowed value range
* precision or scale (for decimal/amount types)
* rounding rules
* normalization expectations
* maximum or minimum limits
* whether normalization vs rejection is required
* format strictness
* overflow handling
* operator support expectations
* serialization format requirements

---

### Example — TransactionAmount Primitive

If the user asks to create `TransactionAmount`, the agent should ask questions such as:

* What is the maximum allowed amount?
* Is zero allowed or forbidden?
* Are negative values allowed?
* What decimal precision is required?
* Should values be rounded or rejected if precision is exceeded?
* If rounding is allowed — what rounding mode should be used?
* Should currency minor-unit scale be enforced (e.g., 2 digits)?
* Is normalization allowed or must invalid precision fail validation?
* Should arithmetic operators be restricted?

Agent must wait for answers before generating the primitive.

---

### Generation Confidence Rule

Agent may generate immediately only if:

* domain constraints are explicitly provided, OR
* primitive semantics are universally obvious (e.g., NonEmptyString, PositiveInt)

Otherwise → clarification required.

---

### Safe Suggestion Mode

Before clarification, agent may suggest:

* possible validation rules
* recommended constraints
* attribute options
* transform options

But must label them as **proposals**, not final implementation.

---


# Global Type Rules (Project-Specific — MUST FOLLOW)

These rules are project-specific and override any generic recommendations.

## Shape Rule — Class vs Struct

- If the underlying type is **string** → primitive MUST be a `sealed partial class`
- If the underlying type is **not string** → primitive MUST be a `readonly partial struct`
- Agent must not choose class vs struct heuristically

Correct:

public sealed partial class Email : IDomainValue<string>  
public readonly partial struct OrderId : IDomainValue<Guid>

Forbidden:

public readonly partial struct Email : IDomainValue<string>  
public sealed partial class OrderId : IDomainValue<Guid>

---

## Allowed Underlying Types Only

DomainPrimitives may wrap only these C# types:

- string
- Guid
- byte
- sbyte
- short
- ushort
- int
- uint
- long
- ulong
- decimal
- double
- float
- bool
- char
- TimeSpan
- DateTime
- DateTimeOffset
- DateOnly
- TimeOnly

Rules:

- No other underlying types are allowed
- Unsupported types → agent must refuse and suggest closest supported type
- Custom types are only allowed if they are already DomainPrimitives

---

# Structural Generator Safety Rules

These rules are mandatory and must never be violated.

## Required Interface

Every DomainPrimitive must implement:

IDomainValue<T> (AltaSoft.DomainPrimitives)

---

## Partial Declaration

All DomainPrimitive types must be declared `partial`.

---

## Constructors Are Forbidden

Do not generate:

- parameterless constructors
- value constructors
- private constructors

Construction is generator-controlled.

---

## No Public Fields or Properties

Do not declare:

- public fields
- public properties
- manual backing storage

Generator provides storage and access APIs.

---

## Reserved Member Names (Forbidden)

Do not declare:

- _value
- _valueOrDefault
- _isInitialized

---

## Validate Method Signature

Must be exactly:

public static PrimitiveValidationResult Validate(T value)

Rules:

- public
- static
- correct parameter type
- return PrimitiveValidationResult

---

## No Side Effects in Validate or Transform

Forbidden:

- database access
- HTTP calls
- file I/O
- logging
- DI usage
- configuration reads

Validation must be pure.

---

# Validation Logic Rules

Validation defines domain invariants and must be explicit and domain-focused. Validatation Method must not call Transform.

## Preferred Error Return Style

Use implicit string → PrimitiveValidationResult conversion:

if (value < 0)
    return "number is negative";

Avoid PrimitiveValidationResult.Error(...) unless explicitly required.

---

## Success Return Rule

Always return:

PrimitiveValidationResult.Ok

when validation passes.

---

## Early Return Pattern

Use ordered early-return checks:

if (value.Length == 0)
    return "must not be empty";

if (value.Length > 50)
    return "too long";

return PrimitiveValidationResult.Ok;

Avoid nested validation trees.

---

## Validation Logic Generation Rules

Agent must generate concrete domain validation — not placeholders.

Forbidden outputs:

- TODO validate
- add validation here
- always return Ok without reason

Validation must match primitive semantics inferred from the name.

Examples:

- PositiveAmount → value > 0
- Percentage → 0–100
- Id → not empty / not zero / not Guid.Empty
- Code → character set + length

Validation must stay local to the value and not depend on external systems.

---

## Error Message Style

Validation error messages must be:

- short
- domain-focused
- human-readable
- action-oriented

Good:

- "must be positive"
- "length must be between 3 and 20"
- "contains non-ASCII characters"

Bad:

- "invalid input"
- "validation failed"
- "system error"

---

## Primitive Creation Templates

Agent must select template strictly by underlying type.

### String Template

sealed partial class + IDomainValue<string>

### Non-String Template

readonly partial struct + IDomainValue<T>

Templates must follow Structural Generator Safety Rules.

---

## Transform (Normalization) Rules

Optional normalization hook executed before Validate. 

Signature:

static T Transform(T value)

Allowed:

- trim
- casing
- canonical formatting

Forbidden:

- hiding invalid data
- defaults substitution
- external lookups

Must be idempotent.

---

## Attribute Usage Rules

Attributes express structural or mechanical constraints.  
Validate expresses domain semantics.

### StringLength

Use only for string primitives with fixed length limits.

### SupportedOperations

Use for numeric primitives to restrict arithmetic operators:
Addition, Subtraction, Multiplication, Division, Modulus.

### SerializationFormat

Use for Date/Time/Duration types when canonical external format is required.

Do not use attributes for dynamic or contextual rules.

---

## Custom Static ToString Formatting Hook

DomainPrimitives may define:

public static string ToString(T value)

This is a generator formatting hook — not an override.

Rules:

- static
- pure
- deterministic
- invariant culture only

Example:

public static string ToString(DateOnly value)
    => value.ToString("dd", CultureInfo.InvariantCulture);

Do not add when default formatting is sufficient or when SerializationFormat covers the need.

---

# Agent Refusal Rules

Agent must refuse to generate primitives that:

- violate struct/class shape rule
- use unsupported underlying types
- declare constructors
- add public fields or properties
- perform external validation
- throw in Validate
- use non-C# implementations
