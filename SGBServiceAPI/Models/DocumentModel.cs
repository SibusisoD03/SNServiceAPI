using System;

namespace SNServiceAPI.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentPath { get; set; }
        public DateTime UploadedDate { get; set; }
        public int UpdloadedBy { get; set; }
    }
}
