ReCaptcha-ASP.NET-MVC

Install via Nuget
Install-Package reCaptcha.AspNet.Mvc

Simple usage
1) Get Google reCAPTCHA at https://www.google.com/recaptcha
2) Add Site key and Secret key to your Web.config

<add key="ReCaptcha:SiteKey" value="your-site-key" />
<add key="ReCaptcha:SecretKey" value="your-secret-key" />

3) Add server-side integration to your back-end

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

4) Add client-side integration to your front-end

if you need multiple instance on one page please skip to next header

Add the following code to your Views/Merchants/Register.cshtml:

@ReCaptcha.GetHtml(@ViewBag.publicKey)

@if (ViewBag.RecaptchaLastErrors != null)
{
    <div>Oops! Invalid reCAPTCHA =(</div>
}

Add client-side integration to your front-end for explicit use or multiple instance

introduced in 1.2.3 requested in #11

@ReCaptcha.GetExplictHtml("example1", @ViewBag.publicKey)

@ReCaptcha.GetExplictHtml("example2", @ViewBag.publicKey)

@ReCaptcha.GetExplictScript()

@if (ViewBag.RecaptchaLastErrors != null)
{
    <div>Oops! Invalid reCAPTCHA =(</div>
}

Done!
Usage
@ReCaptcha.GetHtml(...)

Let's talk more about the most basic way to get started:

@ReCaptcha.GetHtml("site-key")

Arguments

The synopsis for the @ReCaptcha.GetHtml function is:

@ReCaptcha.GetHtml(publicKey, [theme], [type], [callback], [lang])

ReCaptcha Parameter
key 	value 	default 	description
publicKey 			Your sitekey.
theme 	dark/light 	light 	Optional. The color theme of the widget.
type 	audio/image 	image 	Optional. The type of CAPTCHA to serve.
callback 			Optional. Your callback function that's executed when the user submits a successful CAPTCHA response. The user's response, g-recaptcha-response, will be the input for your callback function.
lang 	See language codes 		Optional. Forces the widget to render in a specific language. Auto-detects the user's language if unspecified.
@ReCaptcha.GetExplictHtml(...)

For enabling multi captcha in one page. please check the example for Explicit rendering for multiple widgets

simple use

@ReCaptcha.GetExplictHtml("id","site-key")

Arguments

The synopsis for the @ReCaptcha.GetExplictHtml function is:

@ReCaptcha.GetExplictHtml(id, publicKey, [widgetRenderCallsArr], [theme], [type], [callback])

ReCaptcha Parameter
key 	value 	default 	description
id 			the recaptcha widget id. required uniquely identifies the recaptcha widget
publicKey 			Your sitekey.
widgetRenderCallsArr 		__recaptcha_widgetRenderCallsArr 	js variable name for recaptcha render calls.
theme 	dark/light 	light 	Optional. The color theme of the widget.
type 	audio/image 	image 	Optional. The type of CAPTCHA to serve.
callback 			Optional. Your callback function that's executed when the user submits a successful CAPTCHA response. The user's response, g-recaptcha-response, will be the input for your callback function.
@ReCaptcha. GetExplictScript([lang], [load], [widgetRenderCallsArr])

@ReCaptcha.GetExplictScript()

Arguments

The synopsis for the @ReCaptcha.GetExplictScript function is:

@ReCaptcha.GetExplictScript([lang], [load], [widgetRenderCallsArr])

ReCaptcha Parameter
key 	value 	default 	description
lang 	See language codes 		Optional. Forces the widget to render in a specific language. Auto-detects the user's language if unspecified.
load 		__recaptcha_onloadCallback 	js variable name for recaptcha on load explicit call.
widgetRenderCallsArr 		__recaptcha_widgetRenderCallsArr 	js variable name for recaptcha render calls.
@ReCaptcha.Validate(privateKey)

returns true for valid response from user, false otherwise.
ReCaptcha Parameter

privateKey 'Secret key' is Required. The shared key between your site and ReCAPTCHA.
@ReCaptcha.GetLastErrors(HttpContextBase context)

returns a IEnumerable<reCaptcha.ErrorCodes>. if returns null the no errors occurred.
ReCaptcha Parameter

context is your HttpContenxt e.g. this.HttpContext