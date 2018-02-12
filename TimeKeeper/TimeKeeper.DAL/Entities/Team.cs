﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Team : BaseClass<string>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Engagement> Engagements { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
