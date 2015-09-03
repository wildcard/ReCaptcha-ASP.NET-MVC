ReCaptcha-ASP.NET-MVC
=====================

ReCaptcha ASP.NET MVC latest version wrapper

https://www.google.com/recaptcha

# Simple usage

### 1) Get Google reCAPTCHA at https://www.google.com/recaptcha

### 2) Add Site key and Secret key to your Web.config

```xml
<add key="ReCaptcha:SiteKey" value="your-site-key" />
<add key="ReCaptcha:SecretKey" value="your-secret-key" />
```

### 3) Add server-side integration to your back-end

```c#
public class AccountsController : Controller
{
    [HttpGet]
    public ActionResult Register()
    {
        ViewBag.Recaptcha = ReCaptcha.GetHtml(ConfigurationManager.AppSettings["ReCaptcha:SiteKey"]);

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterMerchantViewModel request)
    {
        try
        {
            if (ModelState.IsValid && ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptcha:SecretKey"]))
            {
                // Do what you need

                return View("RegisterConfirmation");
            }

            ViewBag.RecaptchaLastErros = ReCaptcha.GetLastErrors(this.HttpContext);
            ViewBag.Recaptcha = ReCaptcha.GetHtml(ConfigurationManager.AppSettings["ReCaptcha:SiteKey"]);

            return View(request);
        }
        catch (Exception)
        {
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}
```

### 4) Add client-side integration to your front-end

Add the following code to your ``Views/Merchants/Register.cshtml``:

```c#
@ViewBag.Recaptcha
            
@if (ViewBag.RecaptchaLastErros != null)
{
    <div>Oops! Invalid reCAPTCHA =(</div>
}
```

## Done!

---

## Usage

### `@ReCaptcha.GetHtml(...)`

Let's talk more about the most basic way to get started:

``` razor
@ReCaptcha.GetHtml("site-key")
```

#### Arguments

The synopsis for the `@ReCaptcha.GetHtml` function is:

``` razor
@ReCaptcha.GetHtml(publicKey, [theme], [type], [callback], [lang])
```

1. `publicKey` is a string .
2. `theme` is a string .
3. `type` is a string .
4. `callback` is a string .
5. `lang` is a string .

##### ReCaptcha Parameter [reCaptcha doc](https://developers.google.com/recaptcha/docs/display) 

key | value | default | description
----|-------|---------|------------
`publicKey` | | | Your sitekey.
`theme` | dark/light | light | Optional. The color theme of the widget.
`type` | audio/image | image | Optional. The type of CAPTCHA to serve.
`callback` |  |  | Optional. Your callback function that's executed when the user submits a successful CAPTCHA response. The user's response, g-recaptcha-response, will be the input for your callback function.
`lang` | See [language codes](https://developers.google.com/recaptcha/docs/language) | | Optional. Forces the widget to render in a specific language. Auto-detects the user's language if unspecified.

### `@ReCaptcha.Validate(...)`

##### ReCaptcha Parameter

### `@ReCaptcha.GetLastErrors(HttpContextBase context)`

##### ReCaptcha Parameter
