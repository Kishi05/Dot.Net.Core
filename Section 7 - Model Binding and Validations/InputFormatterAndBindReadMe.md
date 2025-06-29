# Why `[BindNever]` Doesn't Work with `[FromBody]`

## 🔍 Explanation

`[BindNever]` is part of the **model binding system**, which binds data from:

- Route parameters (`[FromRoute]`)
- Query strings (`[FromQuery]`)
- Form data (`[FromForm]`)
- Headers
- [FromBody] uses formatters (like JSON or XML) to deserialize the entire request body into an object.

Formatters (e.g., System.Text.Json, Newtonsoft.Json, XmlSerializer) bypass the model binding pipeline, so attributes like [BindNever] are not respected.

However, `[FromBody]` uses **formatters** (like JSON or XML serializers) for deserialization, **not model binding**. Because of this, attributes like `[BindNever]` are **ignored** when binding from the body.


## ✅ Summary Table

| Attribute     | Works With                                 | Affects `[FromBody]`? |
| ------------- | ------------------------------------------ | --------------------- |
| `[BindNever]` | `[FromQuery]`, `[FromForm]`, `[FromRoute]` | ❌ No                  |
| `[Bind]`      | Same as above                              | ❌ No                  |
| `[FromBody]`  | Uses formatters (JSON/XML)                 | ✅ Yes                 |

Example:

```csharp
public class BindTest
{
    public int Id { get; set; }
    public string Name { get; set; }

    [BindNever] // This does NOT prevent binding when using [FromBody]
    public string Email { get; set; }

    public string Phone { get; set; }
}

[ApiController]
[Route("bind")]
public class BindController : ControllerBase
{
    [HttpPost("BindNever")]
    public IActionResult BindNever([FromBody] BindTest bindTest)
    {
        return Ok(bindTest);
    }
}
```

will still populate the **Email** property — even though it's marked with `[BindNever]`.