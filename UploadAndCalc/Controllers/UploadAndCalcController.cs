using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadAndCalc.Models;

namespace UploadAndCalc.Controllers
{
    public class UploadAndCalcController : Controller
    {

        private readonly AppDbContext _appDbContext;
        public UploadAndCalcController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        private bool IsNumeric(string line)
        {
            throw new NotImplementedException();
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            var lcount = 0;

            CalcItemRepository lCalcItemRepository = new CalcItemRepository(_appDbContext);

            lCalcItemRepository.ClearItems();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        var filePath = Path.GetTempFileName();
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);

                        }
                        //if import file
                        if (lCalcItemRepository.uploadFileReults(filePath))
                        {
                            lcount++;
                            filePaths.Add(filePath);
                        }
                    }
                 }
            }
            return Ok(new { mean = lCalcItemRepository.GetMean(), stddev = lCalcItemRepository.GetStandardDeviation(), range = lCalcItemRepository.GetRanges() });
        }


    }
}
