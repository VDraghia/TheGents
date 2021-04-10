namespace ProjectManagementCollection.Models
{
    public class ListFactorDescriptor
    {
        public int FactorId { get; set; }

        public int Position { get; set; }

        public string MainCategoryDesc { get; set; }

        public string SubCategoryDesc { get; set; }

        public bool Checked { get; set; }
    }
}
