### EzConfig - Minimal Version
------------

###### How To Use?

###### How to Inıt

```csharp
EzConfig.Others.SelectConfig(Application.StartupPath + @"/Example", true);
```
###### or

```csharp
EzConfig.Others.SelectConfig(Application.StartupPath + @"/Example");
EzConfig.Others.CreateConfig();
```

------------

###### How To Read & Write Value

###### How to write value?

```csharp
EzConfig.WriteValue("Section", "KeyName", "Value");
```

###### How to read value?

```csharp
EzConfig.ReadValue("Section", "KeyName");
```

###### returned value is "Value"
