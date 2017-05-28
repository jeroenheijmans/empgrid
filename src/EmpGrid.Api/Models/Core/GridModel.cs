namespace EmpGrid.Api.Models.Core
{
    public class GridModel
    {
        public EmpModel[] Emps { get; set; }
        public MediumModel[] Mediums { get; set; }

        public override string ToString()
        {
            return $"GridModel with {Emps?.Length} Emps and {Mediums?.Length} Mediums";
        }
    }
}
