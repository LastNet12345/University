namespace University.Core
{
#nullable disable
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        // Foreign Key
        public int StudentId { get; set; }
        // public int StudentFK { get; set; } <-- går ej

        // Nav prop
        public Student Student { get; set; }
    }
}