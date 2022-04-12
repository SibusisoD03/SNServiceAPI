namespace SNServiceAPI.Models
{
    public class NominationModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int SecondedBy { get; set; }
        public int EmisCode { get; set; }
    }
}
