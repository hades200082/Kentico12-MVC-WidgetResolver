# Kentico 12 MVC Widget Resolver for Portal Engine widgets

This package can be used to parse out Portal Engine widgets from Rich Text fields.

## Install

**Nuget Package Manager** 

Note: This package has not been released yet. Once it is released I'll include nuget details here.


## Usage

### Dependency Injection

#### Autofac

If you're using AutoFac then the following will suffice...

```csharp
var builder = new ContainerBuilder();

// ...

builder.RegisterWidgetResolver();

// ...
```

#### Other DI

Within your dependency injection, ensure that the following interfaces are registered to their corresponding classes. They should all be registered as singletons.

- `IRichTextResolver` = `RichTextResolver`
- `IWidgetResolver` = `WidgetResolver`
- `IWidgetRegistry` = `WidgetRegistry`

### Configuration

Create your widgets in Kentico as normal. The properties you create do not need to be represented on the webpart that the widget is based upon.

 *Remember that this package will only give you access to the widgets property values, not any WebForms UserControl functionality.*

In your MVC application create a model class that has the same properties as you created for the widget in Kentico. Make this model class implement the interface `IWidgetmodel` from this package.

`IWidgetRegistry.RegisterWidget<YourWidgetModel>(string YourWidgetCodeName)` needs to be called to register each widget that you want to use. There are two optional parameters for this method that allow you to specify action delegates that can be used to perform additional logic before or after widget resolution.

The `beforeAction` returns a `JObject` and is passed a `JObject` of the parsed widget properties.

The afterAction returns, and is passed, an object of your widget model.

**For example**

```csharp
_registry.RegisterWidget<PortalWidgetYouTubeVideo>("MvcYouTubeVideo", 
   beforeAction: o =>
    {
        var strList = o.Property("MyNodeIDUniSelectorProperty").Value.ToString();
        var list = strList.Split(';').Select(val => int.Parse(val));
        var docs = DocumentHelper.GetDocuments().WhereIn("NodeID", list);

        o.Add("MyDocumentList", Newtonsoft.Json.JsonConvert.SerializeObject(docs));
        return o;
    });
```

#### Autofac

If you're using Autofac you can register your widgets by creating a new class that implements `IStartable` from the Autofac package and inject into its constructor the `IWidgetRegistry`.

Register your widgets with the WidgetRegistry as follows within the `Start()` method.

```csharp
public class WidgetRegistrationComponent : IStartable
{
    private readonly IWidgetRegistry _registry;

    public WidgetRegistrationComponent(IWidgetRegistry registry)
    {
        _registry = registry;
    }

    /// <inheritdoc />
    public void Start()
    {
        _registry.RegisterWidget<PortalWidgetYouTubeVideo>("MvcYouTubeVideo");
        _registry.RegisterWidget<PortalWidgetQuotedText>("MvcQuotedText");
    }
}
```

Then in your dependency injection config register the `IStartable`:

```csharp
var builder = new ContainerBuilder();

// ...

builder.RegisterWidgetResolver();

builder.RegisterType<WidgetRegistrationComponent>()
       .As<IStartable>()
       .SingleInstance();

// ...
```

## Resolving Richtext

In your ViewModel set the rich-text property to type `IRichTextData`.

```csharp
public class MyBlogViewModel
{
    // ...
    
    public IRichTextData BodyContent { get; set; }
    
    // ...
}
```

Inject `IRichTextResolver` into your controller.

When you retrieve the `string` property from the Rich Text property of the Kentico object/document, call `RichTextresolver.Resolve(string text)` on it.

```csharp
var vm = new MyBlogViewmodel{
    BodyContent = RichTextresolver.Resolve(MyKenticoTreeNode.GetValue("BodyContent"))
};
```

In your views create a view at `~/Views/Shared/DisplayTemplates/YourWidgetModelName.cshtml` that accepts your widget model as its ViewModel and renders the widget data as you desire.

---

**Important:** If you wish to control how the HTML portions of the RichText are rendered (such as calling ResolveUrls on it) then you must also create a DisplayTemplate view called `RichTextHtml.cshtml`

```csharp
@model LeeConlin.Kentico12.MVC.WidgetResolver.RichTextHtml
@Html.Kentico().ResolveUrls(Model)
```

---

In the view that your controller returns, to render the `IRichTextData` property of the ViewModel using `DisplayFor`:

```csharp
@Html.DisplayFor(m => m.YourRichTextField)
```

This will automatically render the RichText using the display templates.