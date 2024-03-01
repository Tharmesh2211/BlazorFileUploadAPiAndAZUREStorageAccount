using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccount.Domain.Model
{
    public class FileOrBlobUpload
    {
        [Key]
        public int BlobFileId {  get; set; }
        public Guid BlobFileGuid { get; set; }
        public string BlobFileType {  get; set; }
        public string FileName { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
