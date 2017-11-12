using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models
{
    public class State
    {
        [Key]
        public string StateAbbreviation { get; set; }
        public string StateName { get; set; }
    }
}
