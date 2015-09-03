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

            ViewBag.RecaptchaLastErrors = ReCaptcha.GetLastErrors(this.HttpContext);
            
            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];

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
@ReCaptcha.GetHtml(@ViewBag.publicKey)
            
@if (ViewBag.RecaptchaLastErrors != null)
{
    <div>Oops! Invalid reCAPTCHA =(</div>
}
```

## Done!


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

##### ReCaptcha Parameter [reCaptcha doc](https://developers.google.com/recaptcha/docs/display) 

key | value | default | description
----|-------|---------|------------
`publicKey` | | | Your sitekey.
`theme` | dark/light | light | Optional. The color theme of the widget.
`type` | audio/image | image | Optional. The type of CAPTCHA to serve.
`callback` |  |  | Optional. Your callback function that's executed when the user submits a successful CAPTCHA response. The user's response, g-recaptcha-response, will be the input for your callback function.
`lang` | See [language codes](https://developers.google.com/recaptcha/docs/language) | | Optional. Forces the widget to render in a specific language. Auto-detects the user's language if unspecified.

### `@ReCaptcha.Validate(privateKey)`

see [recaptcha doc](https://developers.google.com/recaptcha/docs/verify)

returns true for valid response from user, false otherwise.

##### ReCaptcha Parameter

privateKey 'Secret key' is Required. The shared key between your site and ReCAPTCHA.

### `@ReCaptcha.GetLastErrors(HttpContextBase context)`

see [recaptcha doc](https://developers.google.com/recaptcha/docs/verify#error-code-reference)

returns a `IEnumerable<reCaptcha.ErrorCodes>`. 
if returns null the no errors occurred. 

##### ReCaptcha Parameter

context is your HttpContenxt e.g. `this.HttpContext`
