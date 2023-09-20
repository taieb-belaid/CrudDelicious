#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudDelicious.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace CrudDelicious.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;   

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context; 
    }
    public IActionResult Index()
    {
        ViewBag.All_Dishes =  _context.Dishes.ToList();
        Console.WriteLine(ViewBag.All_Dishes);
        return View();
    }

    [HttpGet("adddish")]
    public IActionResult Adddish()
    {
        return View("Adddish");
    }
    [HttpPost("/dish/add")]
    public IActionResult AddDish(Dish newDish)
    {
        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction ("Index");
    }
    
    //__________Select_One_Dish______

    [HttpGet("/dish/onedish/{DishId}")]
    public IActionResult OneDish (int DishId)
    {
        Dish? One_Dish = _context.Dishes.SingleOrDefault(d => d.DishId == DishId);
        return View(One_Dish);
    }

    //___________Delete______________
    
    [HttpGet("/dish/delete/{DishId}")]
     public IActionResult Delete (int DishId)
     {
        Dish? Delete_Dish = _context.Dishes.SingleOrDefault(d => d.DishId == DishId);
        _context.Dishes.Remove(Delete_Dish);
        _context.SaveChanges();
        return RedirectToAction("Index");
     }
     // ___________Edit_Dish_________
     [HttpGet("/dish/edit/{DishId}")]
     public IActionResult EditDish (int DishId)
     {
        Dish? One_Dish = _context.Dishes.FirstOrDefault(d=>d.DishId==DishId);
        return View(One_Dish);
     }

     //___________Update_Dish________
     [HttpPost("/dish/update/{DishId}")]
     public IActionResult Update(int DishId, Dish newDish)
     {
        Dish? Old_Dish = _context.Dishes.FirstOrDefault(d=>d.DishId==DishId);
        Old_Dish.Name = newDish.Name;
        Old_Dish.Chef = newDish.Chef;
        Old_Dish.Calories = newDish.Calories;
        Old_Dish.Description = newDish.Description;
        Old_Dish.Tastiness = newDish.Tastiness;
        Old_Dish.UpdatedAt = newDish.UpdatedAt;
        _context.SaveChanges();
        return RedirectToAction("Index");
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
