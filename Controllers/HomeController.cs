using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
      // List<User> AllUsers = dbContext.Users.ToList();
      return View();
    }

    [HttpGet]
    [Route("user/levelone")]
    public IActionResult LevelOne()
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
        Cards[j].Divisor = rand.Next(1, 10);
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
      return View("LevelOne", Cards);
    }

    [HttpGet]
    [Route("user/leveltwo")]
    public IActionResult LevelTwo()
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
        Cards[j].Dividend = rand.Next(1, 20);
        Cards[j].Divisor = rand.Next(1, 10);
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
      return View("LevelTwo", Cards);
    }

    [HttpGet]
    [Route("user/levelthree")]
    public IActionResult LevelThree()
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
        Cards[j].Dividend = rand.Next(1, 20);
        Cards[j].Divisor = rand.Next(1, 20);
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
      return View("LevelThree", Cards);
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
