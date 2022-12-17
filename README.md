# Personal Data Encryption Console Application

An application that implements PDE (personal data encryption) that appeared in Windows11 22H2.

## Environment

- Windows 11 22H2
- Visual Studio 2019
- C# console application (.NET Framework 4.7.2)

## Nuget Package

Add the following package.

```
Microsoft.Windows.SDK.Contracts
```

Aboce package is neccesary to use WinRT API in ".NET Framework" application.


## How to use this application

Specify target file path and encryption level.

-0: Not encrypted
-1: Encrypted
-2: Encrypted

You want to know more details about PDE, please read Microsoft official document.

[Microsoft PDE](https://learn.microsoft.com/en-us/windows/security/information-protection/personal-data-encryption/overview-pde)


```
pde.exe "D:\test.txt" 0
```




