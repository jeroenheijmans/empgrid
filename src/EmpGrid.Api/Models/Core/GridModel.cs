using System.Collections.Generic;

namespace EmpGrid.Api.Models.Core
{
    public class GridModel
    {
        public EmpModel[] Emps { get; set; }
        public IDictionary<string, MediumModel> Mediums { get; set; }

        public override string ToString()
        {
            return $"GridModel with {Emps?.Length} Emps and {Mediums?.Count} Mediums";
        }
    }
}
