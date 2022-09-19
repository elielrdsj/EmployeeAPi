namespace NovoProjeto.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long SocialSecurity { get; set; }
        public float Salary { get; set; }
        public string WorkSector { get; set; }
        public string Function { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
