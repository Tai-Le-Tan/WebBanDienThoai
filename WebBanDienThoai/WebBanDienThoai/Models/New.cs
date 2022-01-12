﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;


    public partial class New
    {
        public int IdNew { get; set; }
        [Display(Name = "Tên tin tức")]
        [Required(ErrorMessage = "Tên tin tức  Không Được Bỏ Trống")]
        [MaxLength(250, ErrorMessage = "Tên tin tức Không Được Vượt Quá 250 Kí Tự")]
        public string NameNew { get; set; }
        [Display(Name = "Đường Dẫn URL")]
        [Required(ErrorMessage = "URL Không Được Bỏ Trống")]
        [MaxLength(250, ErrorMessage = "URL Không Được Vượt Quá 250 Kí Tự")]
        [Index(IsUnique = true)]
        [Column(TypeName = "varchar")]
        public string Alias { get; set; }
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Display(Name = "Nội Dung")]
        public string Content { get; set; }
        [Display(Name = "Tác Giả")]
        public string Author { get; set; }
        [Display(Name = "Tiêu đề trang")]
        [MaxLength(250, ErrorMessage = "MetaTitle Không Được Quá 250 Kí Tự")]
        public string MetaTitle { get; set; }
        [Display(Name = "Từ Khóa SEO")]
        [MaxLength(250, ErrorMessage = "MetaKeyWord Không Được Quá 250 Kí Tự")]
        public string MetaKeyword { get; set; }
        [Display(Name = "Mô tả SEO")]
        public string MetaDescription { get; set; }
        [Display(Name = "Ngày đăng")]
        public Nullable<System.DateTime> CreatedAt { get; set; }
        [Display(Name = "Trạng Thái")]
        public Nullable<bool> Status { get; set; }
        [Display(Name = "Mô tả ngắn")]
        [Required(ErrorMessage = "Không Được Bỏ Trống")]
        [MaxLength(250, ErrorMessage = "URL Không Được Vượt Quá 250 Kí Tự")]
        public string DescriptShort { get; set; }
    }
}
