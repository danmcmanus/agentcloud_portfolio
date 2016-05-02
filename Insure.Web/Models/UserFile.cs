using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Insure.Web.Models
{
    public class UserFile
    {
        public int UserFilesId { get; set; }
        public int UserFileId { get; set; }
        public string FileId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public AppUser AppUser { get; set; }
        public virtual ICollection<File> Files { get; set; }

        
        public UserFile()
        {
            this.AppUser.Id = Convert.ToInt32(this.IdentityUser.Id);
            
        }
    }
}