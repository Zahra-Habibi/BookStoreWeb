﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }


        [Required]
        public string ISBN { get; set; }


        [Required]
        public string Author { get; set; }


        [Required]
        [Range(1,10000)]
        [Display(Name ="listofPRICE")]
        public double ListPrice { get; set; }


        [Required]
        [Range(1, 10000)]
        [Display(Name ="price")]
        public double Price { get; set; }


        [Required]
        [Range(1, 10000)]
        [Display(Name ="price for 1-50")]
        public double Price50 { get; set; }


        [Required]
        [Range(1, 10000)]
        [Display(Name ="price +100")]
        public double Price100 { get; set; }


        [ValidateNever]
        public string ImgUrl { get; set; }


        [Required]
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category category { get; set; }


        [Required]
        public int CoverTypId { get; set; }


        [ForeignKey("CoverTypId")]
        [ValidateNever]
        public CoverType coverType { get; set; }

    }
}
