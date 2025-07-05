## 📘 Section 22 – Error Handling

 ## Key Concepts

| Topic                                  | Description                                                               |
| -------------------------------------- | ------------------------------------------------------------------------- |
| UseExceptionHandler                    | Global middleware that reroutes unhandled exceptions to a safe endpoint   |
| IExceptionHandlerPathFeature           | Used to extract exception and failing path inside the error controller    |
| CustomException                        | User-defined domain exception to control status codes & behavior          |
| ExceptionMiddleware                    | Custom try-catch middleware for API error formatting                      |
| Razor View for Errors                  | Rendered string model or fallback HTML on unhandled exception             |
| Environment Checks                     | Show full stack in dev, safe messages in prod using `IWebHostEnvironment` |
