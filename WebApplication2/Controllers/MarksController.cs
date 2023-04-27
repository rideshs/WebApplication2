using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using CsvHelper;


namespace WebApplication2.Controllers
{
    public class MarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Marks
        /* public async Task<IActionResult> Index()
         {
             var marks = await _context.Marks.ToListAsync();
             var studentPredictions = PredictStudentSuccess(marks);
             ViewData["StudentPredictions"] = studentPredictions;
             return View(marks);
         }*/
        public async Task<IActionResult> Index()
        {
            var marks = await _context.Marks.ToListAsync();
            var finalMarks = marks.Select(mark => new FinalMark
            {
                StudentId = mark.StudentId,
                CourseId = mark.CourseId,
                SubjectId = mark.SubjectId,
                Marks = CalculateFinalMarks(mark)
            }).ToList();

            var studentPredictions = PredictStudentSuccess(finalMarks);
            ViewData["StudentPredictions"] = studentPredictions;

            return View(finalMarks);
        }









        private float CalculateFinalMarks(Marks marks)
        {
            // You can modify this formula as per your requirement
            return marks.PreboardMarks * 0.6f + marks.AssignmentMarks * 0.4f;
        }
        // Method for predicting student success
        private List<StudentPrediction> PredictStudentSuccess(List<FinalMark> finalMarks)
        {
            var predictions = new List<StudentPrediction>();

            foreach (var finalMark in finalMarks)
            {
                var prediction = new StudentPrediction();
                prediction.StudentId = finalMark.StudentId;
                prediction.CourseId = finalMark.CourseId;
                prediction.SubjectId = finalMark.SubjectId;

                var totalMarks = finalMark.Marks;

                // You can modify this threshold value as per your requirement
                if (totalMarks >= 40)
                {
                    prediction.Success = true;
                }
                else
                {
                    prediction.Success = false;
                }

                predictions.Add(prediction);
            }

            return predictions;
        }



        // Method for uploading marks data
        /* [HttpPost]
         public IActionResult UploadMarksData(IFormFile file)
         {
             var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
             {
                 // Set the configuration options here
             };

             using (var stream = new StreamReader(file.OpenReadStream()))
             using (var csv = new CsvReader(stream, csvConfig))
             {
                 var marks = csv.GetRecords<Marks>().ToList();
                 var finalMarks = marks.Select(mark => new FinalMark
                 {
                     StudentId = mark.StudentId,
                     CourseId = mark.CourseId,
                     SubjectId = mark.SubjectId,
                     Marks = CalculateFinalMarks(mark)
                 }).ToList();
               *//*  var predictions = PredictStudentSuccess(finalMarks);*//*
                 return RedirectToAction("UploadMarksData");*/
        [HttpPost]
        public IActionResult UploadMarksData(UploadMarksDataModel model)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Set the configuration options here
            };
            try
            {

                using (var stream = new StreamReader(model.CsvFile.OpenReadStream()))
                using (var csv = new CsvReader(stream, csvConfig))
                {
                    var marks = csv.GetRecords<Marks>().ToList();
                    var finalMarks = marks.Select(mark => new FinalMark
                    {
                        StudentId = mark.StudentId,
                        CourseId = mark.CourseId,
                        SubjectId = mark.SubjectId,
                        Marks = CalculateFinalMarks(mark)
                    }).ToList();
                    var predictions = PredictStudentSuccess(finalMarks);
                    return View("Predictions", predictions);
                }
            }

            catch (Exception ex)
            {
                // If an error occurred, display an error message to the user
                ViewData["ErrorMessage"] = "An error occurred while processing the CSV file: " + ex.Message;
                return View("Upload", model);
            }


        }





    }
}






        








    

    // StudentPredictionController
    public class StudentPredictionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentPredictionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentPredictions
        public async Task<IActionResult> Index()
        {
            var studentPredictions = await _context.StudentPredictions.ToListAsync();
            return View(studentPredictions);
        }
    }




