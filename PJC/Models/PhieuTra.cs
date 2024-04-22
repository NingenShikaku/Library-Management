using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PJC.Models
{
    public class PhieuTra
    {
        [Display (Name = "Mã phiếu trả")]
        public string MaPM { get; set; }

        [Display(Name = "Mã sách")]
        public string MaSach { get; set; }

        [Display(Name = "Mã độc giả")]
        public string MaDG { get; set; }

        [Display(Name = "Ngày hẹn trả")]
        public DateTime NgayHenTra { get; set; }

        [Display(Name = "Ngày trả")]
        public DateTime NgayTra { get; set; }

        [Display(Name = "Số lượng mượn")]
        public int SoLuongMuon { get; set; }

        [Display(Name = "Số lượng trả")]
        public int SoLuongTra { get; set; }

        [Display(Name = "Trạng thái trả")]
        public string? TrangThai { get; set; }

        [Display (Name = "User")]
        public string User { get; set; }

        [Display (Name = "Tiền phạt")]
        public double TienPhat { get; set; }
    }
}
