using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Models
{
    public class Product
    {
        [Display(Name = "Ürün Id")]
        public int Id { get; set; }

        [Required (ErrorMessage = "Lütfen ürün adı girin")]
        [Display(Name = "Ürün Adı")]
        public string? Name { get; set; }

        [Range(0,1000000000000000, ErrorMessage = "Ürün fiyatı 0'dan büyük olmalıdır")]
        [Required (ErrorMessage = "Lütfen fiyatı belirtin")]
        [Display(Name = "Ürün Fiyatı")]
        public decimal? Price { get; set; }

        [Display(Name = "Ürün Görseli")]
        public string? Image { get; set; }

        [Required (ErrorMessage = "Lütfen ürünün aktiflik bilgisini belirtin")]
        [Display(Name = "Aktiflik Durumu" ) ]
        public bool IsActive { get; set; }

        [Required (ErrorMessage = "Lütfen bir ürün kategorisi seçiniz")]
        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }


    }
}