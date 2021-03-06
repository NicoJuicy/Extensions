# Extensions
Useful static or extension methods for working in .NET (especially LINQPad)

## DataTable extensions
In some situations, it can be useful to view an XML file as a table, for example when a webservice returns an array of objects.  While there is a built in function in .NET to read an XML file into a `DataSet`, it consists of multiple tables with relations, which isn't so easily viewable.
For example, the following XML:
```xml
<root>
  <User id="1">
    <FirstName>Fred</FirstName>
    <LastName>Bloggs</LastName>
    <UserName>bloggsf</UserName>
    <Address>
      <BuildingNumber>12</BuildingNumber>
      <Street>The Street</Street>
      <Town>Exampletown</Town>
    </Address>
  </User>
  <User id="3">
    <UserName>bloggsj</UserName>
    <FirstName>Joe</FirstName>
    <LastName>Bloggs</LastName>
    <Address>
      <BuildingNumber>9</BuildingNumber>
      <Street>Somewhere Else</Street>
      <Town>Exampletown</Town>
      <PostCode>TY12 6UA</PostCode>
    </Address>
  </User>
</root>
```
can be displayed like:

@id | FirstName | LastName | UserName | Address.BuildingNumber | Address.Street | Address.Town | Address.PostCode
--- | --- | --- | --- | --- | --- | --- | ---
1 | Fred | Bloggs | bloggsf | 12 | The Street | Exampletown | null
3 | Joe | Bloggs | bloggsj | 9 | Somewhere Else | Exampletown | TY12 6UA

using the following code:
```cs
XML.ToDataTable(xml: XElement.Load(@"\\path\to\file.xml"), hierarchySeparator: "." /* don't shorten column names */, includeAttributes: true, reverseHierarchy: true);
```

## String Extensions
Have you ever noticed that most of the time when doing a `String.IndexOf`, you almost always want to do some text manipulation.  Maybe you want the text that occurs before the string you are searching for, maybe the text before it, or maybe even the text between another `IndexOf`.  This is what `StringExtensions` helps you achieve with `TextBefore`, `TextAfter` and `TextBetween`.  Also, it can be useful to know the number of times a substring appears in a string, for this you can use `CountOccurrences`.  In addition, you can get all indexes of a substring in a string with `AllIndexesOf` and you can get all occurrences of text between two strings with `AllTextBetween`.  If you need all the indexes of multiple substrings, you can use `AllSortedIndexesOf`.

I find that I often want to use a string if it's value is not null or empty, otherwise use a different string. To make it easy with the null coalescing operator, there is an extension method for `NullIfEmpty`, so one can easily say `string1.NullIfEmpty() ?? string2`, for example, rather than needing lots more code. Also, it accepts an argument of whether or not to treat it as empty when it only contains whitespace, to make it easier to change at runtime unlike the built-in separate method `string.IsNullOrWhiteSpace` approach.

[![MyGet Build Status](https://www.myget.org/BuildSource/Badge/progamer-me?identifier=4653f437-eca9-4422-9a81-662bceb36acc)](https://www.myget.org/)
