# Math Extension

A .NET library that contains a Rational number class. This allows you to work with rational numbers
without floating point errors.

It also contains some useful mathematical functions, such as GCD.

Available on [NuGet](https://www.nuget.org/packages/MathExtension/).

## Rational Numbers

The `Rational` class can be used like any other numeric class in .NET.
There are a number of ways to create a `Rational` number:

1. Using the constructor:
```
var r = new Rational(2, 3); // = 2/3
```
2. By casting:
```
var r = (Rational)2 / 3; // = 2/3
var r = (Rational)2.5; // = 5/2
```
3. Using a slightly nicer casting method:
```
using Q = MathExtension.Rational;
...
var r = (Q)2 / 3;
```
4. Converting from a double with tolerance:
```
var r = Rational.FromDouble(0.66667, 1e-4); // = 2/3
```

The `Rational` class works will all standard mathematical and comparison operators.

Supported framework versions: 4.0, 4.5