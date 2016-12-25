using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomWeb.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Newtonsoft.Json;
using System.IO;
using System.Data.Entity.Core.Objects;
using Microsoft.Office.Interop.Word;

namespace DiplomWeb.Controllers
{
    public class UserVigilsController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecordVigils
        public ActionResult Index()
        {
            //string id = User.Identity.GetUserId();
            //ApplicationUser applicationUser = db.Users.Find(id);
            //var role = db.Roles.Where(d=>d.)
            ////IEnumerable<Vigil> listVigil = db.Vigils.Include(v => v.ApplicationRole).

            string id = User.Identity.GetUserId();
         //   IEnumerable<String> list = UserManager.GetRoles(id);
           // db.ApplicationRole.Where(p => list.Contains(p.Name));
           // db.Vigils.Include(v => v.ApplicationRole);
            //List<ApplicationRole> rl = db.ApplicationRole.Where(p => list.Contains(p.Name)).ToList();
           // List<Vigil> vigil = new List<Vigil>();
            //foreach (ApplicationRole ap in rl)
            //{
            //    vigil.AddRange(ap.Vigils.ToList());
            //}
            //List<RecordVigil> records = new List<RecordVigil>();
            //foreach (Vigil vg in vigil)
            //{

            //    records.AddRange(vg.RecordVigils);
            //}
          //  ViewBag.VigilId = new SelectList(vigil, "Id", "Name");

            //    original.Any(p => otherPeople.Contains(p));
            return View();
        }

        public ActionResult Vigil(int id)
        {

            string idUser = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(idUser);
        //    IEnumerable<String> listRoles = UserManager.GetRoles(idUser);
            List<Vigil> vig = user.Vigils.ToList();
            ViewBag.Vigil = vig;
            Vigil vigil =db.Vigils.Find(id);
            ViewBag.VigilName = vigil.Name;
            ViewBag.VigilId = vigil.Id;
            ViewBag.UserName = user.SecondName + " " + user.FirstName;
            List<RecordVigil> list = vigil.RecordVigils.ToList();
            List<object> record = new List<object>();
            foreach (var item in list)
            {
                bool editable = false;
                if (item.ApplicationUserID == User.Identity.GetUserId())
                {
                    editable = true;
                }
                record.Add(new { color=item.Color , title = item.ApplicationUser.SecondName+" " +item.ApplicationUser.FirstName, id = item.Id,
                start = item.StartAt.AddDays(1).ToUniversalTime(), end = item.EndAt.AddDays(1).ToUniversalTime(), description =item.Description ,allDay = true,editable=editable});
            }
          
            ViewBag.Record = record;
            ViewBag.Days = vigil.Days;
            ViewBag.VigilName = vigil.Name;
            return View();
        }
        [HttpPost]
        public FileContentResult DownloadFile([Bind(Include = "Id,TitleOne,Text,DateStart,DataEnd")] DateSelect dateSelect)
        {
            Vigil vig = db.Vigils.Find(dateSelect.Id);
            List<RecordVigil> rvig = vig.RecordVigils.Where(s => s.StartAt >= dateSelect.DateStart && s.StartAt <= dateSelect.DateEnd).ToList();
            object objMissing = System.Reflection.Missing.Value;
            object objEndofDocument = "\\endofdoc";
            Microsoft.Office.Interop.Word.Application appobj;
            Microsoft.Office.Interop.Word.Document docobj;
            appobj = new Microsoft.Office.Interop.Word.Application();
            appobj.Visible = false;
            docobj = appobj.Documents.Add(ref objMissing, ref objMissing, ref objMissing, ref objMissing);








            //adding text to document
            docobj.Content.SetRange(0, 0);
           
            //Add paragraph with Heading 1 style
            Microsoft.Office.Interop.Word.Paragraph para1 = docobj.Content.Paragraphs.Add(ref objMissing);
            para1.Range.Bold = 1;
            para1.Range.Text = dateSelect.TitleOne;

            para1.Range.Font.Name = "Times New Roman";
            para1.Range.Font.Size = 14;
            para1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            para1.Range.InsertParagraphAfter();

            //Add paragraph with Heading 2 style
            Microsoft.Office.Interop.Word.Paragraph para2 = docobj.Content.Paragraphs.Add(ref objMissing);
            para2.Range.Bold = 1;
            para2.Range.Text = dateSelect.Text;

            para2.Range.Font.Name = "Times New Roman";
            para2.Range.Font.Size = 14;
            para2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            para2.Range.InsertParagraphAfter();

            








            Microsoft.Office.Interop.Word.Table tableObj;
            Microsoft.Office.Interop.Word.Range RangeWord = docobj.Bookmarks.get_Item(ref objEndofDocument).Range;
            tableObj = docobj.Tables.Add(RangeWord, rvig.Count+1, 2, ref objMissing, ref objMissing);
            tableObj.Range.ParagraphFormat.SpaceAfter = 8;
            tableObj.Borders.Enable = 1;
            SetTextTable(tableObj.Cell(1, 1),"ФИО",WdParagraphAlignment.wdAlignParagraphCenter);
            SetTextTable(tableObj.Cell(1, 2), "Дата",WdParagraphAlignment.wdAlignParagraphCenter);
            int i = 2;
            foreach (RecordVigil record in rvig)
            {
                String name = String.Format("{0} {1} {2}", record.ApplicationUser.SecondName, record.ApplicationUser.FirstName, record.ApplicationUser.Patronymic);
                SetTextTable(tableObj.Cell(i, 1),
                   name,
                    WdParagraphAlignment.wdAlignParagraphJustify);
                SetTextTable(tableObj.Cell(i, 2), record.StartAt.ToString("d"),
                    WdParagraphAlignment.wdAlignParagraphJustify);
                i++;
            }
               
            docobj.SaveAs(Server.MapPath(@"~/Files/doc2.doc"));
            docobj.Close(ref objMissing, ref objMissing, ref objMissing);
            docobj = null;
            byte[] fileContents = System.IO.File.ReadAllBytes(Server.MapPath(@"~/Files/doc2.doc"));

            string fullPath = Request.MapPath(@"~/Files/doc2.doc");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            return File(fileContents, "application/msword",dateSelect.Text+".doc");
        }



        public ActionResult FormDateDownload()
        {
            
            return PartialView();
        }


        private void SetTextTable(Cell cell, string text,WdParagraphAlignment aligment)
        {
            cell.Range.Text = text;
            cell.Range.Font.Name = "Times New Roman";
            cell.Range.Font.Size = 14;
            cell.Range.ParagraphFormat.Alignment = aligment;
        }

        public ActionResult Sheduler(int id)
        {
            //string id = User.Identity.GetUserId();
            //ApplicationUser applicationUser = db.Users.Find(id);
            //var role = db.Roles.Where(d=>d.)
    //        ////IEnumerable<Vigil> listVigil = db.Vigils.Include(v => v.ApplicationRole).
    //        dp.events.list = [
    //{
    //        start: "2013-03-25T00:00:00",
    //  end: "2013-03-27T00:00:00",
    //  id: "1",
    //  resource: "A",
    //  text: "Event 1"
    //},
    //{
    //        start: "2013-03-26T12:00:00",
    //  end: "2013-03-27T00:00:00",
    //  id: "2",
    //  resource: "B",
    //  text: "Event 2"
    //}
    //];
            Vigil vigil =db.Vigils.Find(id);
            List<RecordVigil> rv = vigil.RecordVigils.ToList();
            List<object> events = new List<object>();
            List<object> listUser = new List<object>();
            //List<ApplicationUser> users = vigil.VigilGroups.SelectMany(s => s.ApplicationUsers).ToList();
            List<ApplicationUser> users = vigil.ApplicationUsers.ToList();
            foreach (ApplicationUser item in users)
            {
                listUser.Add(new { name=item.SecondName+" "+item.FirstName, id=item.Id });
            }
            foreach (RecordVigil item in rv)
            {
        
           
                events.Add(new { start=item.StartAt.AddDays(1).ToUniversalTime(), end= item.EndAt.ToUniversalTime(),resource=item.ApplicationUserID,
                text="",  moveDisabled = true,
                    resizeDisabled = true
                });
            }
            ViewBag.Events = events;
            ViewBag.Users = listUser;
            ViewBag.Name = vigil.Name;
            //    original.Any(p => otherPeople.Contains(p));
            return View();
        }


        [HttpPost]
        public JsonResult SaveDate(DateTime startAt, DateTime endAt, string title, string color,int vigilId, string description)
        {
            string userId = User.Identity.GetUserId();
           RecordVigil recordVigil= new RecordVigil() { ApplicationUserID = userId, EndAt = endAt, StartAt = startAt, Title = title, VigilID = vigilId,Color =color, Description=description };
            db.RecordVigils.Add(recordVigil);
            db.SaveChanges();
            return new JsonResult { Data = new {Id=recordVigil.Id, start = startAt, end = endAt, title = title, color = color }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        [HttpPost]
        public JsonResult UpdateDate(int id,DateTime startAt, DateTime endAt)
        {
            RecordVigil recordVigil = db.RecordVigils.Find(id);
            recordVigil.StartAt = startAt;
            recordVigil.EndAt = endAt;
            db.Entry(recordVigil).State = EntityState.Modified;
            db.SaveChanges();
            return new JsonResult { Data = new { start = startAt, end = endAt}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // POST: RecordVigils/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,ApplicationUserID,VigilID,DateVigil,Note")] RecordVigil recordVigil)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(recordVigil).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", recordVigil.ApplicationUserID);
        //    ViewBag.VigilID = new SelectList(db.Vigils, "Id", "Name", recordVigil.VigilID);
        //    return View(recordVigil);
        //}




        // GET: RecordVigils/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RecordVigil recordVigil = db.RecordVigils.Find(id);
        //    if (recordVigil == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", recordVigil.ApplicationUserID);
        //    ViewBag.VigilID = new SelectList(db.Vigils, "Id", "Name", recordVigil.VigilID);
        //    return View(recordVigil);
        //}



   




        //// GET: RecordVigils/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RecordVigil recordVigil = db.RecordVigils.Find(id);
        //    if (recordVigil == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(recordVigil);
        //}

        //public ActionResult ViewCalendar()
        //{
        //    return View();
        //}


      
     
        [HttpPost]
        public JsonResult ChangeEvent(int id,string title, string color,string description)
        {
            RecordVigil recordVigil = db.RecordVigils.Find(id);
            recordVigil.Title = title;
            recordVigil.Description = description;
            recordVigil.Color = color;
            db.Entry(recordVigil).State = EntityState.Modified;
            db.SaveChanges();
            return new JsonResult { Data = new { title = title, color = color, id = id , description = description }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        [HttpPost]
        public JsonResult DeleteEvent(int id)
        {
            RecordVigil recordVigil = db.RecordVigils.Find(id);
            db.RecordVigils.Remove(recordVigil);
            db.SaveChanges();
            return new JsonResult { Data = new { id = id }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UserVigils/GetEvents
        
        public JsonResult GetEvents(int id)
        {
            //  var v = db.RecordVigils.OrderBy(a => a.StartAt).ToList();
            List<RecordVigil> records = db.Vigils.Find(id).RecordVigils.ToList();

            string userId = User.Identity.GetUserId();
       //     ApplicationUser user = db.Users.Find(id);
         //   IEnumerable<String> list = UserManager.GetRoles(id);
           // List<ApplicationRole> rl = db.ApplicationRole.Where(p => list.Contains(p.Name)).ToList();
            //List<Vigil> vigil = new List<Vigil>();
            //foreach (ApplicationRole ap in rl)
           //{
             //   vigil.AddRange(ap.Vigils.ToList());
            //}
            //List<RecordVigil> records = new List<RecordVigil>();
            //foreach (Vigil vg in vigil)
            //{
            //    records.AddRange(vg.RecordVigils);
            //}
            //List<Vigil> list = 

            List<RecordVigil> vlo = new List<RecordVigil>();
            foreach (RecordVigil item in records)
            {
                bool editable = false;
                if (item.ApplicationUserID == userId)
                {
                    editable = true;
                }
            
                vlo.Add(new RecordVigil(){ Id=item.Id, Title = item.ApplicationUser.UserName+" "+item.Vigil.Name, Description = item.Description, StartAt = item.StartAt, EndAt = item.EndAt, IsFullDay = true, Color=item.Color,Editable=editable});

            }
            
            return new JsonResult { Data = vlo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

           
        }





       



      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
