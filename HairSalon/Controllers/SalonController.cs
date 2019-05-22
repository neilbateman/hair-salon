using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class SalonController : Controller
  {
    [HttpGet("/salons/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Salon thisSalon = Salon.Find(id);
      List<Stylist> salonStylists = thisSalon.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("thisSalon", thisSalon);
      model.Add("salonStylists", salonStylists);
      model.Add("allStylists", allStylists);
      return View(model);
    }

    [HttpGet("/salons")]
    public ActionResult Index()
    {
      List<Salon> allSalons = Salon.GetAll();
      return View(allSalons);
    }

    [HttpGet("/salons/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/salons")]
    public ActionResult Create(string name, string description)
    {
      Salon newSalon = new Salon(name, description);
      newSalon.Save();
      List<Salon> allSalons = Salon.GetAll();
      return View("Index", allSalons);
    }
    [HttpPost("/salons/delete-salons")]
      public ActionResult DeleteSalons(int salonId)
      {
          Salon.ClearAll();
          return RedirectToAction("Index");
      }
      [HttpPost("/salons/{salonId}/delete-salon")]
      public ActionResult Delete(int salonId)
      {
          Salon selectedSalon = Salon.Find(salonId);
          selectedSalon.DeleteSalon();
          return RedirectToAction("Index");
      }

    [HttpPost("/salons/{salonId}/stylists/new")]
    public ActionResult AddStylist(int stylistId, int salonId)
    {
      Salon salon = Salon.Find(salonId);
      Stylist stylist = Stylist.Find(stylistId);
      salon.AddStylist(stylist);
      return RedirectToAction("Show", new {id = salonId});
    }
  }
}
