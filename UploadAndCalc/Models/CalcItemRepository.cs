using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UploadAndCalc.Models
{

    public class CalcItemRepository : ICalcItemRepository
    {
        private readonly AppDbContext _appDbContext;

        public CalcItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<CalcItem> AllItems {
            get {
                return _appDbContext.CalcItems;
            }
        }

        public double GetMean()
        {
            double ltotal = 0;
            int lcount = 0;
            foreach (CalcItem lItem in _appDbContext.CalcItems) {
                lcount++;
                ltotal += lItem.CalcItemValue;
            }
            return (double)ltotal / lcount;
        }

        public string GetRanges(){
            string lreturn = ""; 
            int[] larray = new int[] {0,0,0,0,0,0,0,0,0,0}; 
            foreach (CalcItem lItem in _appDbContext.CalcItems){
                int lrange = (int)Math.Floor((double)(lItem.CalcItemValue / 10));
                larray[lrange < 0 ? 0 : lrange]++;
                //lreturn += (lrange < 0 ? 0 : lrange).ToString()+",";
            }
            for (int i = 0; i < 10; i++) {
                lreturn += "["+(i*10).ToString()+" <"+ ((i+1) * 10).ToString()+"]="+ larray[i]+";";
            }
            return lreturn;
        }

        public double GetStandardDeviation()
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (CalcItem lItem in _appDbContext.CalcItems)
            {
                double tmpM = M;
                M += (lItem.CalcItemValue - tmpM) / k;
                S += (lItem.CalcItemValue - tmpM) * (lItem.CalcItemValue - M);
                k++;
            }
            return Math.Sqrt(S / (k - 2));
        }

        public void ClearItems() {
            //reset db
            _appDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.CalcItems");
        }

        public bool uploadFileReults(string filePath)
        {
            var lcount = 0;

            long number1 = 0;

            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadToEnd();
                string[] items = line.Split(',');
                foreach(string item in items)
                {
                    if (item.Length > 0)
                    {
                        System.Console.WriteLine("ok"); 
                        System.Console.WriteLine(item);
                        _appDbContext.Database.ExecuteSqlCommand("INSERT INTO  dbo.CalcItems (CalcItemValue) VALUES (" + item + ")");
                        //this could be a stored proc, but will leave as insert for now
                        lcount++;
                    }
                }
            }
            return lcount == 0 ? false : true;
        }
    }
}
