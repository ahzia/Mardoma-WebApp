using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArchiveSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MimeKit;

namespace ArchiveSystem.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment hostingEnviroment;

        [BindProperty] //to directly access object of document
        public DocumentUpsetViewModel Document { get; set; }
        public string WebRootPath { get; private set; }

        //public Document Document { get; set; }
        //for dependincy injection:constractor
        public DocumentController(ApplicationDbContext db,
            IHostingEnvironment hostingEnviroment)
        {
            _db = db;
            this.hostingEnviroment = hostingEnviroment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Document = new DocumentUpsetViewModel();
            if (id == null) {
                //New Document
                //return the new document object:
                return View(Document);
            }
            //update
            //retrive data and display:
            Document doc = _db.Documents.FirstOrDefault(u => u.Id == id);
            if (doc == null) {
                return NotFound();
            }
            ///hear
            //file
            Document = new DocumentUpsetViewModel
            {
                Id=doc.Id,
                Year = doc.Year,
                Grant = doc.Grant,
                Catagory = doc.Catagory,
                SubCatagory = doc.SubCatagory,
                About = doc.About,
                other=doc.other,
                Region=doc.Region
            };
            return View(Document);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //for sequrity
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (Document.Id == 0)
                {
                    if (Document.file != null) {
                        //aslout phisical path of www
                        String uploadFolder=Path.Combine(hostingEnviroment.WebRootPath, "Document");
                        //to make unique file name
                        uniqueFileName= Guid.NewGuid().ToString() + "_" + Document.file.FileName;
                        String filePath=Path.Combine(uploadFolder, uniqueFileName);
                        Document.file.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                    //create
                    Document doc = new Document
                    {
                        Year = Document.Year,
                        Grant = Document.Grant,
                        Catagory = Document.Catagory,
                        SubCatagory = Document.SubCatagory,
                        About=Document.About,
                        file=uniqueFileName,
                        other=Document.other,
                        Region=Document.Region,
                        fileName=Document.file.FileName
                    };
                    _db.Documents.Add(doc);
                }

                else
                {
                    Document doc = _db.Documents.FirstOrDefault(u => u.Id == Document.Id);
                    if (doc == null)
                    {
                        return NotFound();
                    }
                    if (Document.file != null)//new file uploaded
                    {
                        //aslout phisical path of www
                        String uploadFolder = Path.Combine(hostingEnviroment.WebRootPath, "Document");
                        //to make unique file name
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + Document.file.FileName;
                        String filePath = Path.Combine(uploadFolder, uniqueFileName);
                        Document.file.CopyTo(new FileStream(filePath, FileMode.Create));
                        doc.fileName = Document.file.FileName;
                    }
                    else {
                        //new file not uploaded 
                        uniqueFileName = doc.file;
                    }

                    doc.Year = Document.Year;
                    doc.Grant = Document.Grant;
                    doc.Catagory = Document.Catagory;
                    doc.SubCatagory = Document.SubCatagory;
                    doc.About = Document.About;
                    doc.file = uniqueFileName;
                    doc.Region = Document.Region;
                    doc.other = Document.other;
                    doc.fileName = Document.file.FileName;                    
                    //edit up

                    _db.Documents.Update(doc);

                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Document);
        }
        #region API Calls
        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Documents.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var documentFromDb = await _db.Documents.FirstOrDefaultAsync(u => u.Id == id);
            if (documentFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Documents.Remove(documentFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        //private string fileName { get; set; }
       
        
        //to Download file:
        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var documentFromDb = await _db.Documents.FirstOrDefaultAsync(u => u.Id == id);
            if (documentFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            string file = documentFromDb.file;
            //hear
            string webPath = hostingEnviroment.WebRootPath;
            String path1 = Path.Combine(webPath, "Document");
            String filePath = Path.Combine(path1, file);
            Console.Write(filePath);
            return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }
        #endregion
    }
}