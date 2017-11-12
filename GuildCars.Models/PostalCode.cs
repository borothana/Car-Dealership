using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models
{
    public class PostalCode
    {
        [Key]
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
