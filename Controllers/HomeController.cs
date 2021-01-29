using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Learn.Models;
using System.Windows;

namespace Learn.Controllers
{
public class HomeController : Controller
{
private LearnContext dbContext;
public HomeController(LearnContext context)
{
dbContext = context;
}
private int LevelOneNum()
{
var exclude = new HashSet<int>() { 7 };
var range = Enumerable.Range(1, 8).Where(i => !exclude.Contains(i));
Random rand = new Random();
int index = rand.Next(0, 7 - exclude.Count);
return range.ElementAt(index);
}
private int LevelTwoNum()
{
var exclude = new HashSet<int>() { 7, 11 };
var range = Enumerable.Range(1, 12).Where(i => !exclude.Contains(i));
Random rand = new Random();
int index = rand.Next(0, 11 - exclude.Count);
return range.ElementAt(index);
}
private int LevelThreeNum()
{
var exclude = new HashSet<int>() { 7, 11, 13, };
var range = Enumerable.Range(1, 16).Where(i => !exclude.Contains(i));
Random rand = new Random();
int index = rand.Next(0, 15 - exclude.Count);
return range.ElementAt(index);
}

[HttpGet]
[Route("")]
public IActionResult Registration()
{
return View();
}

[HttpPost("users/register")]
public IActionResult Registering(LogRegWrapper user)
{
    if(ModelState.IsValid)
    {
        if(dbContext.Users.Any(u => u.Email == user.Register.Email))
        {
            ModelState.AddModelError("Register.Email", "Already Registered? Please Log In.");
            return Registration();
        }
        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        user.Register.Password = Hasher.HashPassword(user.Register, user.Register.Password);
        dbContext.Users.Add(user.Register);
        dbContext.SaveChanges();
        HttpContext.Session.SetInt32("UserId", user.Register.UserId);
        return RedirectToAction("DivideLevelOne");
    }
    else
    {
        return Registration();
    }
}

[HttpPost("logging")]
public IActionResult Logging(LogRegWrapper user)
{
if(ModelState.IsValid)
{
    User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == user.Login.Email);
    if(userInDb == null)
    {
        ModelState.AddModelError("Login.Email", "Invalid Email/Password");
        return Registration();
    }
    PasswordHasher<LoggedUser> Hasher = new PasswordHasher<LoggedUser>();
    PasswordVerificationResult Result = Hasher.VerifyHashedPassword(user.Login, userInDb.Password, user.Login.Password);
    if(Result == 0)
    {
        ModelState.AddModelError("Login.Email", "Invalid Email/Password");
        return Registration();
    }
    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
    return RedirectToAction("DivideLevelOne");
}
else
{
    return Registration();
}
}

[HttpGet("logout")]
    public IActionResult Logout()
    {
        int? LoggedUser = HttpContext.Session.GetInt32("UserId");
        if(LoggedUser == null)
        {
            return RedirectToAction("Registration");
        }
        HttpContext.Session.Clear();
        return RedirectToAction("Registration");
    }


[HttpGet]
[Route("dividelevelone")]
public IActionResult DivideLevelOne()
{
int? LoggedId = HttpContext.Session.GetInt32("UserId");
    if(LoggedId == null)
    {
        return RedirectToAction("Registration");
    }
ViewBag.LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)LoggedId);
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Dividend = rand.Next(1, 8);
    Cards[j].Divisor = LevelOneNum();
    Cards[j].Whole = Cards[j].Dividend / Cards[j].Divisor;
    Cards[j].Modulus = Cards[j].Dividend % Cards[j].Divisor;
    if (Cards[j].Dividend < Cards[j].Divisor)
    {
    Cards[j].Whole = null;
    Cards[j].Answer = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    }
    else
    {
    if (Cards[j].Modulus == 0)
    {
        Cards[j].Answer = null;
    }
    else
    {
        Cards[j].Answer = new Fraction(Cards[j].Modulus, Cards[j].Divisor);
    }
    }
}
return View("DivideLevelOne", Cards);
}


[HttpGet]
[Route("divideleveltwo")]
public IActionResult DivideLevelTwo()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Dividend = rand.Next(1, 12);
    Cards[j].Divisor = LevelTwoNum();
    Cards[j].Whole = Cards[j].Dividend / Cards[j].Divisor;
    Cards[j].Modulus = Cards[j].Dividend % Cards[j].Divisor;
    if (Cards[j].Dividend < Cards[j].Divisor)
    {
    Cards[j].Whole = null;
    Cards[j].Answer = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    }
    else
    {
    if (Cards[j].Modulus == 0)
    {
        Cards[j].Answer = null;
    }
    else
    {
        Cards[j].Answer = new Fraction(Cards[j].Modulus, Cards[j].Divisor);
    }
    }
}
return View("DivideLevelTwo", Cards);
}

[HttpGet]
[Route("dividelevelthree")]
public IActionResult DivideLevelThree()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Dividend = rand.Next(1, 16);
    Cards[j].Divisor = LevelThreeNum();
    Cards[j].Whole = Cards[j].Dividend / Cards[j].Divisor;
    Cards[j].Modulus = Cards[j].Dividend % Cards[j].Divisor;
    if (Cards[j].Dividend < Cards[j].Divisor)
    {
    Cards[j].Whole = null;
    Cards[j].Answer = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    }
    else
    {
    if (Cards[j].Modulus == 0)
    {
        Cards[j].Answer = null;
    }
    else
    {
        Cards[j].Answer = new Fraction(Cards[j].Modulus, Cards[j].Divisor);
    }
    }
}
return View("DivideLevelThree", Cards);
}
public IActionResult DivideQuizOne()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Dividend = rand.Next(1, 8);
    Cards[j].Divisor = LevelOneNum();
    Cards[j].Whole = Cards[j].Dividend / Cards[j].Divisor;
    Cards[j].Modulus = Cards[j].Dividend % Cards[j].Divisor;
    if (Cards[j].Dividend < Cards[j].Divisor)
    {
    Cards[j].Whole = null;
    Cards[j].Answer = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    }
    else
    {
    if (Cards[j].Modulus == 0)
    {
        Cards[j].Answer = null;
    }
    else
    {
        Cards[j].Answer = new Fraction(Cards[j].Modulus, Cards[j].Divisor);
    }
    }
}
return View("DivideQuizOne", Cards);
}

[HttpGet]
[Route("multiplylevelone")]
public IActionResult MultiplyLevelOne()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Dividend = rand.Next(1, 8);
    Cards[j].Divisor = rand.Next(1, 10);
    Cards[j].Whole = Cards[j].Dividend * Cards[j].Divisor;
}
return View("MultiplyLevelOne", Cards);
}

[HttpGet]
// [Route("user/multiplyleveltwo")]
public IActionResult MultiplyLevelTwo()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Dividend = rand.Next(1, 12);
    Cards[j].Divisor = rand.Next(1, 12);
    Cards[j].Whole = Cards[j].Dividend * Cards[j].Divisor;
}
return View("MultiplyLevelTwo", Cards);
}

[HttpGet]
// [Route("user/multiplylevelthree")]
public IActionResult MultiplyLevelThree()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Dividend = rand.Next(1, 10);
    Cards[j].Divisor = rand.Next(1, 16);
    Cards[j].Whole = Cards[j].Dividend * Cards[j].Divisor;
}
return View("MultiplyLevelThree", Cards);
}

[HttpGet]
[Route("addlevelone")]
public IActionResult AddLevelOne()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Id = j;
    Cards[j].Dividend = rand.Next(1, 6);
    Cards[j].Divisor = LevelOneNum();
    Cards[j].DividendTwo = rand.Next(1, 6);
    Cards[j].DivisorTwo = LevelOneNum();
    Cards[j].Whole = Cards[j].Dividend / Cards[j].Divisor;
    Cards[j].WholeTwo = Cards[j].DividendTwo / Cards[j].DivisorTwo;
    Cards[j].Modulus = Cards[j].Dividend % Cards[j].Divisor;
    Cards[j].ModulusTwo = Cards[j].DividendTwo % Cards[j].DivisorTwo;
    Fraction first = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    Fraction second = new Fraction(Cards[j].DividendTwo, Cards[j].DivisorTwo);
    Cards[j].Answer = new Fraction(first + second);
    frac = new Fraction(Cards[j].Answer);
    Cards[j].Answer = frac;
    Cards[j].DecimalDividend = (decimal)frac.Numerator;
    Cards[j].DecimalDivisor = (decimal)frac.Denominator;
    Cards[j].AnswerDividend = (int)frac.Numerator;
    Cards[j].AnswerDivisor = (int)frac.Denominator;
    Cards[j].AnswerWhole = Cards[j].AnswerDividend / Cards[j].AnswerDivisor;
    Cards[j].AnswerModulus = Cards[j].AnswerDividend % Cards[j].AnswerDivisor;
    // Format the fractions to be added:
    if (Cards[j].Modulus == 0)
    {
    Cards[j].First = Cards[j].Dividend.ToString() + "/" + Cards[j].Divisor.ToString();
    }
    else
    {
    Cards[j].First = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    }
    if (Cards[j].ModulusTwo == 0)
    {
    Cards[j].Second = Cards[j].DividendTwo.ToString() + "/" + Cards[j].DivisorTwo.ToString();
    }
    else
    {
    Cards[j].Second = new Fraction(Cards[j].DividendTwo, Cards[j].DivisorTwo);
    }

    // Format the Cards[j].Answer to display as a whole number over 1 and Cards[j].AnswerTwo as a reduced fraction: 
    if (Cards[j].AnswerDividend < Cards[j].AnswerDivisor)
    {
    Cards[j].AnswerWhole = null;
    Cards[j].AnswerTwo = new Fraction(Cards[j].AnswerDividend, Cards[j].AnswerDivisor);
    }
    else
    {
    if (Cards[j].AnswerModulus == 0)
    {
        Cards[j].Answer = Cards[j].Answer.ToString() + "/1";
        Cards[j].AnswerTwo = null;
    }
    else
    {
        Cards[j].AnswerTwo = new Fraction(Cards[j].AnswerModulus, Cards[j].AnswerDivisor);
    }
    }
    // Calculate the decimal answer if the AnswerDivisor is not == 0.
    if (Cards[j].AnswerDivisor != 0)
    {
    Cards[j].AnswerDecimal = Decimal.Round((Cards[j].DecimalDividend / Cards[j].DecimalDivisor), 3);
    }
    else
    {
    Cards[j].AnswerDecimal = Cards[j].DecimalDividend / 1;
    }
}
return View("AddLevelOne", Cards);
}

[HttpGet]
[Route("addleveltwo")]
public IActionResult AddLevelTwo()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Id = j;
    Cards[j].Dividend = rand.Next(1, 10);
    Cards[j].Divisor = LevelOneNum();
    Cards[j].DividendTwo = rand.Next(1, 10);
    Cards[j].DivisorTwo = LevelOneNum();
    Cards[j].Whole = Cards[j].Dividend / Cards[j].Divisor;
    Cards[j].WholeTwo = Cards[j].DividendTwo / Cards[j].DivisorTwo;
    Cards[j].Modulus = Cards[j].Dividend % Cards[j].Divisor;
    Cards[j].ModulusTwo = Cards[j].DividendTwo % Cards[j].DivisorTwo;
    Fraction first = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    Fraction second = new Fraction(Cards[j].DividendTwo, Cards[j].DivisorTwo);
    Cards[j].Answer = new Fraction(first + second);
    frac = new Fraction(Cards[j].Answer);
    Cards[j].Answer = frac;
    Cards[j].DecimalDividend = (decimal)frac.Numerator;
    Cards[j].DecimalDivisor = (decimal)frac.Denominator;
    Cards[j].AnswerDividend = (int)frac.Numerator;
    Cards[j].AnswerDivisor = (int)frac.Denominator;
    Cards[j].AnswerWhole = Cards[j].AnswerDividend / Cards[j].AnswerDivisor;
    Cards[j].AnswerModulus = Cards[j].AnswerDividend % Cards[j].AnswerDivisor;
    // Format the fractions to be added:
    if (Cards[j].Modulus == 0)
    {
    Cards[j].First = Cards[j].Dividend.ToString() + "/" + Cards[j].Divisor.ToString();
    }
    else
    {
    Cards[j].First = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    }
    if (Cards[j].ModulusTwo == 0)
    {
    Cards[j].Second = Cards[j].DividendTwo.ToString() + "/" + Cards[j].DivisorTwo.ToString();
    }
    else
    {
    Cards[j].Second = new Fraction(Cards[j].DividendTwo, Cards[j].DivisorTwo);
    }

    // Format the Cards[j].Answer to display as a whole number over 1 and Cards[j].AnswerTwo as a reduced fraction: 
    if (Cards[j].AnswerDividend < Cards[j].AnswerDivisor)
    {
    Cards[j].AnswerWhole = null;
    Cards[j].AnswerTwo = new Fraction(Cards[j].AnswerDividend, Cards[j].AnswerDivisor);
    }
    else
    {
    if (Cards[j].AnswerModulus == 0)
    {
        Cards[j].Answer = Cards[j].Answer.ToString() + "/1";
        Cards[j].AnswerTwo = null;
    }
    else
    {
        Cards[j].AnswerTwo = new Fraction(Cards[j].AnswerModulus, Cards[j].AnswerDivisor);
    }
    }
    // Calculate the decimal answer if the AnswerDivisor is not == 0.
    if (Cards[j].AnswerDivisor != 0)
    {
    Cards[j].AnswerDecimal = Decimal.Round((Cards[j].DecimalDividend / Cards[j].DecimalDivisor), 3);
    }
    else
    {
    Cards[j].AnswerDecimal = Cards[j].DecimalDividend / 1;
    }
}
return View("AddLevelTwo", Cards);
}

[HttpGet]
[Route("addlevelthree")]
public IActionResult AddLevelThree()
{
Random rand = new Random();
Fraction frac = new Fraction();
// Instantiate an array of Fraction Card objects
FractionCard[] Cards = new FractionCard[12];
for (int i = 0; i < 12; ++i)
{
    Cards[i] = new FractionCard();
}
for (int j = 0; j < 12; ++j)
{
    Cards[j].Id = j;
    Cards[j].Dividend = rand.Next(1, 10);
    Cards[j].Divisor = LevelTwoNum();
    Cards[j].DividendTwo = rand.Next(1, 10);
    Cards[j].DivisorTwo = LevelTwoNum();
    Cards[j].Whole = Cards[j].Dividend / Cards[j].Divisor;
    Cards[j].WholeTwo = Cards[j].DividendTwo / Cards[j].DivisorTwo;
    Cards[j].Modulus = Cards[j].Dividend % Cards[j].Divisor;
    Cards[j].ModulusTwo = Cards[j].DividendTwo % Cards[j].DivisorTwo;
    Fraction first = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    Fraction second = new Fraction(Cards[j].DividendTwo, Cards[j].DivisorTwo);
    Cards[j].Answer = new Fraction(first + second);
    frac = new Fraction(Cards[j].Answer);
    Cards[j].Answer = frac;
    Cards[j].DecimalDividend = (decimal)frac.Numerator;
    Cards[j].DecimalDivisor = (decimal)frac.Denominator;
    Cards[j].AnswerDividend = (int)frac.Numerator;
    Cards[j].AnswerDivisor = (int)frac.Denominator;
    Cards[j].AnswerWhole = Cards[j].AnswerDividend / Cards[j].AnswerDivisor;
    Cards[j].AnswerModulus = Cards[j].AnswerDividend % Cards[j].AnswerDivisor;
    // Format the fractions to be added:
    if (Cards[j].Modulus == 0)
    {
    Cards[j].First = Cards[j].Dividend.ToString() + "/" + Cards[j].Divisor.ToString();
    }
    else
    {
    Cards[j].First = new Fraction(Cards[j].Dividend, Cards[j].Divisor);
    }
    if (Cards[j].ModulusTwo == 0)
    {
    Cards[j].Second = Cards[j].DividendTwo.ToString() + "/" + Cards[j].DivisorTwo.ToString();
    }
    else
    {
    Cards[j].Second = new Fraction(Cards[j].DividendTwo, Cards[j].DivisorTwo);
    }

    // Format the Cards[j].Answer to display as a whole number over 1 and Cards[j].AnswerTwo as a reduced fraction: 
    if (Cards[j].AnswerDividend < Cards[j].AnswerDivisor)
    {
    Cards[j].AnswerWhole = null;
    Cards[j].AnswerTwo = new Fraction(Cards[j].AnswerDividend, Cards[j].AnswerDivisor);
    }
    else
    {
    if (Cards[j].AnswerModulus == 0)
    {
        Cards[j].Answer = Cards[j].Answer.ToString() + "/1";
        Cards[j].AnswerTwo = null;
    }
    else
    {
        Cards[j].AnswerTwo = new Fraction(Cards[j].AnswerModulus, Cards[j].AnswerDivisor);
    }
    }
    // Calculate the decimal answer if the AnswerDivisor is not == 0.
    if (Cards[j].AnswerDivisor != 0)
    {
    Cards[j].AnswerDecimal = Decimal.Round((Cards[j].DecimalDividend / Cards[j].DecimalDivisor), 3);
    }
    else
    {
    Cards[j].AnswerDecimal = Cards[j].DecimalDividend / 1;
    }
}
return View("AddLevelThree", Cards);
}

public IActionResult Privacy()
{
return View();
}

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
}
}
