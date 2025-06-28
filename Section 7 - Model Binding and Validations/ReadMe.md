## ✅ Validation Annotations

| Annotation           | Purpose                                        | Example                                      |
|----------------------|------------------------------------------------|----------------------------------------------|
| `[Required]`         | Field must not be null or empty                | `[Required]`                                 |
| `[StringLength]`     | Set max/min length for strings                 | `[StringLength(50, MinimumLength = 5)]`      |
| `[MaxLength]`        | Max string/array length (affects DB too)       | `[MaxLength(100)]`                           |
| `[MinLength]`        | Minimum string/array length                    | `[MinLength(3)]`                             |
| `[Range]`            | Restrict numeric range                         | `[Range(1, 100)]`                            |
| `[RegularExpression]`| Match specific format (e.g., digits, emails)   | `[RegularExpression("^[0-9]+$")]`            |
| `[Compare]`          | Compare two properties (e.g., password match)  | `[Compare("Password")]`                      |

---

## 🧩 Data Formatting Annotations

| Annotation         | Purpose                                    | Example                                              |
|--------------------|--------------------------------------------|------------------------------------------------------|
| `[DataType]`       | UI rendering hints (not validation)        | `[DataType(DataType.EmailAddress)]`                 |
| `[DisplayFormat]`  | Custom format for dates, numbers, etc.     | `[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]` |
| `[Display]`        | UI label or field order                    | `[Display(Name = "Full Name", Order = 1)]`          |

---

## 📬 Special Format Annotations (Some Also Validate)

| Annotation           | Purpose                          |
|----------------------|----------------------------------|
| `[EmailAddress]`     | Validates email format           |
| `[Phone]`            | Validates phone number           |
| `[Url]`              | Validates a valid URL            |
| `[CreditCard]`       | Validates credit card number     |
| `[FileExtensions]`   | Validates file extensions        |


# 📋 ASP.NET Core Data Annotations – Gotchas & Fixes

This guide highlights **common issues** ("gotchas") with built-in ASP.NET Core data annotations and suggests ways to **validate more strictly** where needed.

---

## ✅ `[EmailAddress]`

| Behavior                     | Accepts invalid formats like `sam@sample,com` or `user@domain` |
|-----------------------------|---------------------------------------------------------------|
| Why                         | Uses simple regex, not fully RFC-compliant                    |
| ✅ Fix                      | Use `System.Net.Mail.MailAddress` or FluentValidation         |
| 🔧 Regex Alternative        | `[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]`           |

---

## 📞 `[Phone]`

| Behavior                     | Accepts random text like `phone123`, `123`, `()+--`            |
|-----------------------------|----------------------------------------------------------------|
| Why                         | Permissive built-in pattern                                     |
| ✅ Fix                      | Use stricter regex                                              |
| 🔧 Regex Example            | `[RegularExpression(@"^\+?\d{10,15}$")]`                        |

---

## 🌐 `[Url]`

| Behavior                     | Accepts malformed URLs like `http:/bad`, `example.com`         |
|-----------------------------|----------------------------------------------------------------|
| Why                         | Doesn't fully validate full URL structure                      |
| ✅ Fix                      | Use custom regex or URI parsing logic                          |
| 🔧 Regex Example            | `[RegularExpression(@"^https?:\/\/[^\s/$.?#].[^\s]*$")]`       |

---

## 💳 `[CreditCard]`

| Behavior                     | Validates only number format (Luhn check), not real cards      |
|-----------------------------|----------------------------------------------------------------|
| ✅ Fix                      | Use payment provider validation (e.g., Stripe, PayPal)          |

---

## 🔁 `[Compare]`

| Behavior                     | Only works within same model                                    |
|-----------------------------|----------------------------------------------------------------|
| ✅ Fix                      | Compare manually in controller or use FluentValidation           |

---

## 🎯 `[Range]`

| Behavior                     | Accepts `null` if not combined with `[Required]`               |
|-----------------------------|----------------------------------------------------------------|
| ✅ Fix                      | Always pair with `[Required]`                                   |
| ✅ Example                  | `[Required]`, `[Range(1, 100)]`                                 |

---

## 🧪 `[RegularExpression]`

| Behavior                     | Won’t auto-trim or enforce full match without anchors         |
|-----------------------------|----------------------------------------------------------------|
| ✅ Fix                      | Always use `^...$` and handle trimming                         |

---

## 📏 `[StringLength]`, `[MaxLength]`, `[MinLength]`

| Behavior                     | Doesn’t trim strings, whitespace-only passes without `[Required]` |
| ✅ Fix                      | Combine with `[Required]` and trim manually in controller/model   |

---

## 🔄 Summary Table

| Annotation         | Common Problem                                  | Suggested Fix                                   |
|--------------------|-------------------------------------------------|-------------------------------------------------|
| `[EmailAddress]`   | Accepts `sam@sample,com`                        | Use `MailAddress` or stricter regex             |
| `[Phone]`          | Accepts random digits/characters                | Custom regex                                     |
| `[Url]`            | Accepts malformed URLs                          | Custom regex or URI parser                      |
| `[CreditCard]`     | Only checks number format, not validity         | Use payment APIs for real validation            |
| `[Compare]`        | Limited to same model context                   | Manual check or use FluentValidation            |
| `[Range]`          | Accepts nulls unless `[Required]` used          | Combine with `[Required]`                       |
| `[RegularExpression]` | Doesn’t auto-trim, needs anchoring            | Add `^...$`, handle whitespace manually         |
| `[StringLength]`   | Doesn't trim or reject whitespace-only strings  | Combine with `[Required]` + manual trim         |

---

## 🧠 Pro Tip

> Consider using **FluentValidation** or **custom validation attributes** for more flexible, expressive, and reusable rules — especially when built-in annotations fall short.

---
