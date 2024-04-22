using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PJC.Models
{
    public class PhieuMuon
    {
        [Display(Name = "Mã phiếu mượn")]
        public string MaPM { get; set; }

        [Display(Name = "Mã độc giả")]
        public string MaDG { get; set; }

        [Display(Name = "Mã sách")]
        public string MaSach{ get; set; }

        [Display(Name = "Ngày mượn")]
        public DateTime NgayMuon { get; set; }

        [Display(Name = "Ngày hẹn trả")]
        public DateTime NgayHenTra { get; set; }

        [Display(Name = "Số lượng mượn")]
        public int SoLuongMuon { get; set; }

        [Display (Name = "Tên thủ thư")]
        public string User { get; set; }

    }
}
