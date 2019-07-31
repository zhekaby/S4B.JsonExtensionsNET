[![Build Status](https://travis-ci.org/zhekaby/S4B.JsonExtensionsNET.svg?branch=master)](https://travis-ci.org/zhekaby/S4B.JsonExtensionsNET)
# S4B.JsonExtensionsNET (.NET Standatd 2.0)
* `Flatten` - flattens json to key-value sequence
* `Unflatten` - unflattens key-value sequence to json
## JSON convertation example
```
{
    "Prop1" : {
        "Data" = 1
    },
    "Prop2" : [
        "el1",
        "el2",
        { "Name" : "John"}
    ]
}
```
Flattened JSON
```
Prop1.Data = 1
Prop2[0] = el1
Prop2[1] = el2
Prop2[2].Name = John
```
## Installation
```
Install-Package S4b.JsonExtensionsNET
```
## Examples of usage
```
(string key, string value)[] list = JsonExtensions.Flatten(data).ToArray();
(string key, string value)[] list = JsonExtensions.Flatten(data, ':').ToArray();

Model m = JsonExtensions.Unflatten<Model>(data);
```

