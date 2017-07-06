# C# Form Handler
[![Build status](https://ci.appveyor.com/api/projects/status/axrcfcwwvy7oakkc/branch/master?svg=true)](https://ci.appveyor.com/project/mlynam/csharp-form-handler/branch/master)


This package is responsible for creating valid, email friendly html given a set of key-value pairs representing form data. The html will be minified and returned as a string. A `Title` and `Description` are options to override in the email's template.

## Getting Started
To install `iTEAMConsulting.FormHandler`, run the following command in the [Package Manager Console](https://docs.nuget.org/docs/start-here/using-the-package-manager-console)
```
PM> Install-Package iTEAMConsulting.FormHandler
```

## Usage
### Form Data
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
### Options
Look at the available options you may override [here](https://raw.githubusercontent.com/iteam-consulting/csharp-form-handler/master/iTEAMConsulting.FormHandler/MakeEmailsOptions.cs).

You may create the options accessor using the static method:
```c#
using Microsoft.Extensions.Options;

var optionsAccessor = new Options.Create<MakeEmailsOptions>(
  new MakeEmailsOptions
  {
    // Place overrides here
    Title = "Override Title", // Default: "Form Data Submission"
    FontColor = "#4d67a9", // Default: "#222222"
  }
);
```
### Getting HTML
To get the email friendly HTML, just build it by passing the data through:
```c#
var emails = new MakeEmails(optionsAccessor);
var html = emails.Build(data);
```


## Authors
iTEAM Consulting: *Software Division*

## Questions
Contact software@iteamnm.com for questions
