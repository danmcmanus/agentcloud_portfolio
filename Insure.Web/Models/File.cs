using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Backload.Contracts.Services.Database;

namespace Insure.Web.Models
{
    public class File : IBackloadStorageProviderFile
    {
        public File()
        {
        }
        public File(IBackloadStorageProviderFile file)
        {
            this.Update(file);
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RowId { get; set; }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Original { get; set; }

        [Required]
        [StringLength(25)]
        public string Type { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public DateTime UploadTime { get; set; }

        public byte[] Data { get; set; }

        public byte[] Preview { get; set; }

        [Required]
        [StringLength(512)]
        public string Path { get; set; }

        [MaxLength(512)]
        public byte[] Meta { get; set; }


        public IBackloadStorageProviderFile Update(IBackloadStorageProviderFile file)
        {
            this.RowId = file.RowId;
            this.Id = file.Id;
            this.Name = file.Name;
            this.Original = file.Original;
            this.Type = file.Type;
            this.Size = file.Size;
            this.UploadTime = file.UploadTime;
            this.Data = file.Data;
            this.Preview = file.Preview;
            this.Path = file.Path;
            this.Meta = file.Meta;

            

            return this;
        }
    }
}