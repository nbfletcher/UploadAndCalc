using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadAndCalc.Models
{
    public interface ICalcItemRepository
    {
        IEnumerable<CalcItem> AllItems { get; }
    }
}
