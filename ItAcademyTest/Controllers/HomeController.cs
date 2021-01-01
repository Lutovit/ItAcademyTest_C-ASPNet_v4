using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItAcademyTest.Models;
using ItAcademyTest.SomeLogic;

namespace ItAcademyTest.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult Find(TestModel tm)
        {
            if (ModelState.IsValid)
            {
                if (tm.Size <= 0 || tm.Size > 100)
                {
                    return RedirectToAction("WrongArrSize");
                }

                else if (tm.MinValue >= tm.MaxValue)
                {
                    return Redirect("/Home/WrongMinMaxVal");
                }

                else if (tm.xValue < tm.MinValue || tm.xValue > tm.MaxValue)
                {
                    return Redirect("/Home/XvalOutOfRange");
                }

                else
                {
                    int[] arr = Logic.ArrIntCreate(tm.Size, tm.MinValue, tm.MaxValue);

                    Logic.MySort(arr);

                    tm.ArrayOfInt = arr;

                    if (Logic.IsThereAnyAnswer(arr, tm.xValue))
                    {
                        tm.NumberOfFirstElementAboveX = Logic.bsearch(arr, tm.xValue);

                        return View(tm);
                    }
                    else
                    {
                        return NoAnswer(tm);
                    }
                }                

            }
            else 
            {
                return Redirect("/Home/WrongInput");
            }            
        }

        [Authorize]
        public ActionResult WrongArrSize() 
        {
            return View();        
        }

        [Authorize]
        public ActionResult NoAnswer(TestModel tm)
        {
            ViewBag.Ob = tm;
            return View("NoAnswer");
        }


        [Authorize]
        public ActionResult WrongInput()
        {
            return View();
        }

        [Authorize]
        public ActionResult WrongMinMaxVal()
        {
            return View();
        }

        [Authorize]
        public ActionResult XvalOutOfRange()
        {
            return View();
        }

    }
}