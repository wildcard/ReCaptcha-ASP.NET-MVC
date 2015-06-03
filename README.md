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
