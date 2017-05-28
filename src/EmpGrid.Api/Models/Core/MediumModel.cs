namespace EmpGrid.Api.Models.Core
{
    public class MediumModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"MediumModel {Id}: '{Name}'";
        }
    }
}
