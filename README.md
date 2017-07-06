# C# Form Handler
This package is responsible for creating valid, email friendly html given a set of key-value pairs representing form data. The html will be minified and returned as a string. A `Title` and `Description` are options to override in the email's template.

## Getting Started
To install `iTEAMConsulting.FormHandler`, run the following command in the [Package Manager Console](https://docs.nuget.org/docs/start-here/using-the-package-manager-console)
```
PM> Install-Package iTEAMConsulting.FormHandler
```

## Usage
Form data should be inside a model object like:
```c#
class FormData
{
  public string Name { get; set; }
  public int Year { get; set; }
}

var data = new FormData
{
  Name = "iTEAM Consulting",
  Year = 2017
};
```
You may create the options accessor using the static method:
```c#
using Microsoft.Extensions.Options;

var optionsAccessor = new Options.Create<MakeEmailsOptions>(
  new MakeEmailsOptions
  {
    Title = "Title",
    Description = "Description"
  }
);
```
Getting the email friendly HTML:
```c#
var emails = new MakeEmails(optionsAccessor);
var html = emails.Build(data);
```


## Authors
iTEAM Consulting: *Software Division*

## Questions
Contact software@iteamnm.com for questions
