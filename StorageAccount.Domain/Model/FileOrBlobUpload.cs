using System.ComponentModel.DataAnnotations;

namespace StorageAccount.Domain.Model
{
    public class FileOrBlobUpload
    {
        [Key]
        public int FileId {  get; set; }
        public Guid FileGuid { get; set; }
        public string FileType {  get; set; }
        public string FileName { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
