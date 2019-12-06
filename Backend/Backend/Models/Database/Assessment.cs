namespace Backend.Models.Database
{
    public class AssessmentDatabaseModel
    {
        public string Id { get; set; }

        public byte Value { get; set; }

        public string UserId { get; set; }
        public virtual UserDatabaseModel User { get; set; }

        public string ProductId { get; set; }
        public virtual ProductDatabaseModel Product { get; set; }
    }
}