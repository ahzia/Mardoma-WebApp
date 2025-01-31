﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiveSystem.Models
{
    public class DocumentUpsetViewModel
    {
        [Key]
        public int Id { get; set; }
        //DateTime.year is also int
        [Required]
        public int Year { get; set; }
        [Required]
        public String Topic { get; set; }
        //Studies or Project Document
        [Required]
        public String Organization { get; set; }
        //proposal, PMF ...
        [Required]
        public String Province { get; set; }
        public String About { get; set; }
        //to copy the file to server
        public IFormFile file { get; set; }
        public String fileName { get; set; }
        public String Where { get; set; }
        public String other { get; set; }
    }
}
