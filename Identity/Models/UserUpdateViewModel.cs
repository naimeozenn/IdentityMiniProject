using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name ="Email:")]
        [Required(ErrorMessage ="Email alanı gereklidir")]
        [EmailAddress(ErrorMessage ="Geçerli bir Email giriniz")]
        public string Email { get; set; }

        [Display(Name = "Telefon:")]
        public string PhoneNumber { get; set; }

        public string PictureUrl { get; set; }

        
        public IFormFile Picture { get; set; }

        [Display(Name = "Ad:")]
        [Required(ErrorMessage = "Ad alanı gereklidir")]
        public string Name { get; set; }

        [Display(Name = "Soyad:")]
        [Required(ErrorMessage = "Soyad alanı gereklidir")]
        public string Surname { get; set; }
    }
}
