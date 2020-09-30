using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Learn.Models;

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
      var number = 2.83;
      var result = Fraction.ToFractions(number, 4);
      Console.WriteLine(result);

      Console.WriteLine("THIS is the Fraction function output: ");
      Console.WriteLine(frac);
      int[] Operand = new int[24];
      string[] FractionAnswer = new string[12];
      double[] DecimalAnswer = new double[12];
      string[] ReducedFractionAnswer = new string[12];
      string[] portionFraction = new string[12];


      for (int i = 0; i < 24; i++)
      {
        Operand[i] = rand.Next(1, 10);
      }
      ViewBag.Operand = Operand;
      for (int j = 0; j < 12; j++)
      {
        int dividend = Operand[j];
        int divisor = Operand[j + 1];
        int? whole = Operand[j] / Operand[j + 1];
        int modulus = Operand[j] % Operand[j + 1];
        if (dividend <= divisor)
        {
          Console.WriteLine("Dividend is Bigger.");
          FractionAnswer[j] = new Fraction(Operand[j], Operand[j + 1]);
        }
        else
        {
          Console.WriteLine("Divisor is Bigger.");
          Console.Write("Dividend" + dividend);
          Console.Write("Divisor" + divisor);
          Console.WriteLine("Whole" + whole);
          Console.WriteLine("Modulus" + modulus);
          if (modulus == 0)
          {
            FractionAnswer[j] = whole.ToString();
          }
          else
          {
            portionFraction[j] = new Fraction(modulus, divisor);
            FractionAnswer[j] = whole.ToString() + " " + portionFraction[j];
            Console.WriteLine("FractionAnswer[j]" + FractionAnswer[j]);
          }
        }

        DecimalAnswer[j] = (Math.Round((double)dividend / divisor, 3));
        ReducedFractionAnswer[j] = Fraction.ToFraction(DecimalAnswer[j]);

      }

      Console.WriteLine(FractionAnswer);
      ViewBag.FractionAnswer = FractionAnswer;
      ViewBag.DecimalAnswer = DecimalAnswer;
      ViewBag.ReducedFractionAnswer = ReducedFractionAnswer;
      return View("LevelOne");
    }

    [HttpGet]
    [Route("user/leveltwo")]

    public IActionResult LevelTwo()
    {
      return View();
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
