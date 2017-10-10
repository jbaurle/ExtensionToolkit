# ExtensionToolkit

**This project is not maintained anymore.**

The Extension Toolkit project provides a collection of useful extension methods for all kind of types (e.g. String class). 

The Microsoft documentation is describing extension methods as follows: Extension methods enable you to add methods to existing types without creating a new derived type, recompiling, or otherwise modifying the original type. Extension methods are a special kind of static method, but they are called as if they were instance methods on the extended type. For client code written in C# and Visual Basic, there is no apparent difference between calling an extension method and the methods that are actually defined in a type.

**Samples**

The String.Replace extension method:

```cs
string mailTemplate = "Dear ${Name}, how are you? .... Creation date: ${CreatedOn}";

//...

string mail = mailTemplate.Replace(new { Name = "Billy", CreatedOn = DateTime.Now });
```

The string variable mail will contain the following text:

```cs
//Dear Billy, how are you? .... Creation date: 01/25/2007
```

The String.ToNameValueCollection and NameValueCollection.Join extension method:

```cs
string prefFromDb = "ShowList=1|Type=Premium|DefaultTheme=Green";

//...

NameValueCollection options = prefFromDb.ToNameValueCollection();

//...

if(options["Type"](_Type_) == "Premium){

  //...

}

//...

prefFromDb = options.Join();
```

The List<string>.ToDataTableStructure extension method:

```cs
List<string> columns = new List<string>();
columns.Add("ID");
columns.Add("Name");

DataTable dt = columns.ToDataTableStructure();
```

The NameValueCollection.ToXml extension method:

```cs
NameValueCollection c = new NameValueCollection();
c.Add("Host", "codeplex.com");
c.Add("Port", "80");

xml = c.ToXml("Server", new { Active = "true" });
```

The created XML snippet will look like this:

```xml
<Server Active="true">
  <Host>codeplex.com</Host>
  <Port>80</Port>
</Server>
```

**Help wanted**

If you want to contribute to this project or have ideas for new extension methods, please feel free to contact me.
