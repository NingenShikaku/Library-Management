using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace PJC.Models
{
    public class StoreContext
    {

        public StoreContext()
        {
        }
        public string ConnectionString { get; set; }
        public StoreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public List<Sach> GetSanPham()
        {
            List<Sach> list = new List<Sach>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from SACH ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sach()
                        {
                            MaSach = reader["MaSach"].ToString(),
                            TenSach = reader["TenSach"].ToString(),
                            TenTG = reader["TenTG"].ToString(),
                            NhaXB = reader["NhaXB"].ToString(),
                            TheLoai = reader["TheLoai"].ToString(),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            GiaTien = double.Parse(reader["GiaTien"].ToString()),
                            ImageUrl = reader["ImageUrl"].ToString(),
                        }); ;
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<Sach> GetSanPhamSearch(string searchString)
        {
            List<Sach> list = new List<Sach>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from SACH where TenSach like @tensach ", conn);
                cmd.Parameters.AddWithValue("tensach", "%" + searchString + "%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sach()
                        {
                            MaSach = reader["MaSach"].ToString(),
                            TenSach = reader["TenSach"].ToString(),
                            TenTG = reader["TenTG"].ToString(),
                            NhaXB = reader["NhaXB"].ToString(),
                            TheLoai = reader["TheLoai"].ToString(),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            GiaTien = double.Parse(reader["GiaTien"].ToString()),
                            ImageUrl = reader["ImageUrl"].ToString(),
                        }); ;
                    }
                    reader.Close();
                }
                conn.Close();
            }

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();



                MySqlCommand cmd = new MySqlCommand("select * from SACH where TenTG like @tacgia ", conn);
                cmd.Parameters.AddWithValue("tacgia", "%" + searchString + "%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sach()
                        {
                            MaSach = reader["MaSach"].ToString(),
                            TenSach = reader["TenSach"].ToString(),
                            TenTG = reader["TenTG"].ToString(),
                            NhaXB = reader["NhaXB"].ToString(),
                            TheLoai = reader["TheLoai"].ToString(),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            GiaTien = double.Parse(reader["GiaTien"].ToString()),
                            ImageUrl = reader["ImageUrl"].ToString(),
                        }); ;
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<TaiKhoan> GetTaiKhoan()
        {
            List<TaiKhoan> list = new List<TaiKhoan>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from taikhoan ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaiKhoan()
                        {
                            User = reader["User"].ToString(),
                            PassWord = reader["PassWord"].ToString(),
                            PhanQuyen = int.Parse(reader["PhanQuyen"].ToString()),
                            TenND = reader["TenND"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            CMND = reader["CMND"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public TaiKhoan GetTaiKhoanByUser(string id)
        {
            TaiKhoan tk = new TaiKhoan();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from taikhoan where User=@user";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("user", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tk.User = reader["User"].ToString();
                        tk.PassWord = reader["PassWord"].ToString();
                        tk.PhanQuyen = int.Parse(reader["PhanQuyen"].ToString());
                        tk.TenND = reader["TenND"].ToString();
                        tk.SDT = reader["SDT"].ToString();
                        tk.CMND = reader["CMND"].ToString();
                    }

                }
            }
            return tk;
        }
        public int CreateUser(TaiKhoan tk)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into taikhoan values(@User, @PassWord,@PhanQuyen,@TenND,@SDT,@CMND)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("User", tk.User);
                cmd.Parameters.AddWithValue("PassWord", tk.PassWord);
                cmd.Parameters.AddWithValue("PhanQuyen", tk.PhanQuyen);
                cmd.Parameters.AddWithValue("TenND", tk.TenND);
                cmd.Parameters.AddWithValue("SDT", tk.SDT);
                cmd.Parameters.AddWithValue("CMND", tk.CMND);

                return (cmd.ExecuteNonQuery());

            }
        }
        public bool DeleteUser(string username)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string delPhieuMuonCmd = "Delete from phieumuon where User=@user";
                MySqlCommand cmd1 = new MySqlCommand(delPhieuMuonCmd, conn);
                cmd1.Parameters.AddWithValue("User", username);

                string delPhieuTraCmd = "Delete from phieutra where User=@user";
                MySqlCommand cmd2 = new MySqlCommand(delPhieuTraCmd, conn);
                cmd2.Parameters.AddWithValue("User", username);

                bool check = (cmd1.ExecuteNonQuery() + cmd2.ExecuteNonQuery())>= 0;
                if (check)
                {
                    var delTaiKhoanCmd = "Delete from taikhoan where User=@user";
                    MySqlCommand cmd = new MySqlCommand(delTaiKhoanCmd, conn);
                    cmd.Parameters.AddWithValue("User", username);
                    return cmd.ExecuteNonQuery() > 0;
                }
                return false;
            }
        }
        public int DeleteUser(TaiKhoan tk)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from taikhoan where User=@user";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("User", tk.User);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int UpdateUser(TaiKhoan tk)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update taikhoan set  PassWord=@PassWord,PhanQuyen=@PhanQuyen,TenND=@TenND,SDT=@SDT,CMND=@CMND where User=@User";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("PassWord", tk.PassWord);
                cmd.Parameters.AddWithValue("PhanQuyen", tk.PhanQuyen);
                cmd.Parameters.AddWithValue("TenND", tk.TenND);
                cmd.Parameters.AddWithValue("SDT", tk.SDT);
                cmd.Parameters.AddWithValue("CMND", tk.CMND);
                cmd.Parameters.AddWithValue("User", tk.User);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int CreateSach(Sach s)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into sach values(@MaSach, @TenSach,@TenTG,@NhaXB,@TheLoai,@SoLuong,@GiaTien,@ImageUrl,@MieuTa)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaSach", s.MaSach);
                cmd.Parameters.AddWithValue("TenSach", s.TenSach);
                cmd.Parameters.AddWithValue("TenTG", s.TenTG);
                cmd.Parameters.AddWithValue("NhaXB", s.NhaXB);
                cmd.Parameters.AddWithValue("TheLoai", s.TheLoai);
                cmd.Parameters.AddWithValue("SoLuong", s.SoLuong);
                cmd.Parameters.AddWithValue("GiaTien", s.GiaTien);
                cmd.Parameters.AddWithValue("ImageUrl", "/img/sach/" + s.ImageUrl);
                cmd.Parameters.AddWithValue("MieuTa", s.MieuTa);

                return (cmd.ExecuteNonQuery());

            }
        }
        public int UpdateProduct(Sach s)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update sach set TenSach=@TenSach,TenTG=@TenTG,NhaXB=@NhaXB,TheLoai=@TheLoai,SoLuong=@SoLuong,GiaTien=@GiaTien,ImageUrl=@ImageUrl,MieuTa=@MieuTa where MaSach=@MaSach ";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("TenSach", s.TenSach);
                cmd.Parameters.AddWithValue("TenTG", s.TenTG);
                cmd.Parameters.AddWithValue("NhaXB", s.NhaXB);
                cmd.Parameters.AddWithValue("TheLoai", s.TheLoai);
                cmd.Parameters.AddWithValue("SoLuong", s.SoLuong);
                cmd.Parameters.AddWithValue("GiaTien", s.GiaTien);
                cmd.Parameters.AddWithValue("ImageUrl", "/img/sach/" + s.ImageUrl);
                cmd.Parameters.AddWithValue("MieuTa", s.MieuTa);
                cmd.Parameters.AddWithValue("MaSach", s.MaSach);
                return (cmd.ExecuteNonQuery());
            }
        }
        public Sach GetSachByMa(string id)
        {
            Sach s = new Sach();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from sach where MaSach=@masach";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("masach", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        s.MaSach = reader["MaSach"].ToString();
                        s.TenSach = reader["TenSach"].ToString();
                        s.TenTG = reader["TenTG"].ToString();
                        s.NhaXB = reader["NhaXB"].ToString();
                        s.TheLoai = reader["TheLoai"].ToString();
                        s.SoLuong = int.Parse(reader["SoLuong"].ToString());
                        s.GiaTien = double.Parse(reader["GiaTien"].ToString());
                        s.ImageUrl = reader["ImageUrl"].ToString();
                        s.MieuTa = reader["MieuTa"].ToString();
                    }

                }
            }
            return s;
        }

        public bool DeleteSach(string maSach)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str1 = "Delete from phieutra where MaSach=@masach";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                cmd1.Parameters.AddWithValue("masach", maSach);

                var str2 = "Delete from phieumuon where MaSach=@masach";
                MySqlCommand cmd2 = new MySqlCommand(str2, conn);
                cmd2.Parameters.AddWithValue("masach", maSach);

                int rowAffected = cmd1.ExecuteNonQuery() + cmd2.ExecuteNonQuery();


                bool check = rowAffected >= 0;
                if (check)
                {
                    var str = "Delete from sach where MaSach=@masach";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("masach", maSach);
                    return cmd.ExecuteNonQuery() > 0;
                }
                return false;
            }
        }

        public int DeleteSach(Sach s)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str1 = "Delete from phieutra where MaSach=@masach";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                cmd1.Parameters.AddWithValue("masach", s.MaSach);

                int rowAffected = cmd1.ExecuteNonQuery();


                bool check = rowAffected >= 0;
                if (check)
                {
                    var str = "Delete from sach where MaSach=@masach";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("masach", s.MaSach);
                    return (cmd.ExecuteNonQuery());
                }
                return -1;
            }
        }
        public List<DocGia> GetDocGia()
        {
            List<DocGia> list = new List<DocGia>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from docgia ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DocGia()
                        {
                            MaDG = reader["MaDG"].ToString(),
                            TenDG = reader["TenDG"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            MatSach = int.Parse(reader["MatSach"].ToString()),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public int CreateDocGia(DocGia dg)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into docgia values(@MaDG, @TenDG,@SDT,@DiaChi,@GioiTinh,@MatSach)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaDG", dg.MaDG);
                cmd.Parameters.AddWithValue("TenDG", dg.TenDG);
                cmd.Parameters.AddWithValue("SDT", dg.SDT);
                cmd.Parameters.AddWithValue("DiaChi", dg.DiaChi);
                cmd.Parameters.AddWithValue("GioiTinh", dg.GioiTinh);
                cmd.Parameters.AddWithValue("MatSach", dg.MatSach);

                return (cmd.ExecuteNonQuery());

            }
        }
        public int UpdateDocGia(DocGia dg)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update docgia set TenDG=@TenDG,SDT=@SDT,DiaChi=@DiaChi,GioiTinh=@GioiTinh,MatSach=@MatSach where MaDG=@MaDG";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("TenDG", dg.TenDG);
                cmd.Parameters.AddWithValue("SDT", dg.SDT);
                cmd.Parameters.AddWithValue("DiaChi", dg.DiaChi);
                cmd.Parameters.AddWithValue("GioiTinh", dg.GioiTinh);
                cmd.Parameters.AddWithValue("MatSach", dg.MatSach);
                cmd.Parameters.AddWithValue("MaDG", dg.MaDG);
                return (cmd.ExecuteNonQuery());
            }
        }

        public bool DeleteDocGia(string maDocGia)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str1 = "Delete from phieutra where MaDG=@madg";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                cmd1.Parameters.AddWithValue("madg", maDocGia);

                var str2 = "Delete from phieumuon where MaDG=@madg";
                MySqlCommand cmd2 = new MySqlCommand(str2, conn);
                cmd2.Parameters.AddWithValue("madg", maDocGia);

                int rowAffected = cmd1.ExecuteNonQuery() + cmd2.ExecuteNonQuery();


                bool check = rowAffected >= 0;
                if (check)
                {
                    var str = "Delete from docgia where MaDG=@madg";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("madg", maDocGia);
                    return cmd.ExecuteNonQuery() > 0;
                }
                return false;
            }
        }

        //public int DeleteDocGia(DocGia dg)
        //{
        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();
        //        var str = "Delete from docgia where MaDG=@madg";
        //        MySqlCommand cmd = new MySqlCommand(str, conn);
        //        cmd.Parameters.AddWithValue("madg", dg.MaDG);
        //        return (cmd.ExecuteNonQuery());
        //    }
        //}
        public DocGia GetDocGiaByMaDG(string id)
        {
            DocGia dg = new DocGia();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from docgia where MaDG=@madg";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("madg", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dg.MaDG = reader["MaDG"].ToString();
                        dg.TenDG = reader["TenDG"].ToString();
                        dg.SDT = reader["SDT"].ToString();
                        dg.DiaChi = reader["DiaChi"].ToString();
                        dg.GioiTinh = reader["GioiTinh"].ToString();
                        dg.MatSach = int.Parse(reader["MatSach"].ToString());
                    }

                }
            }
            return dg;
        }
        public List<PhieuMuon> GetPhieuMuon()
        {
            List<PhieuMuon> list = new List<PhieuMuon>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieumuon ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhieuMuon()
                        {
                            MaPM = reader["MaPM"].ToString(),
                            MaDG = reader["MaDG"].ToString(),
                            MaSach = reader["MaSach"].ToString(),
                            NgayMuon = DateTime.Parse(reader["NgayMuon"].ToString()),
                            NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                            SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                            User = reader["User"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public int CreatePhieuMuon(PhieuMuon pm)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into phieumuon values(@MaPM, @MaDG, @MaSach,@NgayMuon,@NgayHenTra,@SoLuongMuon,@User)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaPM", pm.MaPM);
                cmd.Parameters.AddWithValue("MaDG", pm.MaDG);
                cmd.Parameters.AddWithValue("MaSach", pm.MaSach);
                cmd.Parameters.AddWithValue("NgayMuon", pm.NgayMuon);
                cmd.Parameters.AddWithValue("NgayHenTra", pm.NgayHenTra);
                cmd.Parameters.AddWithValue("SoLuongMuon", pm.SoLuongMuon);
                cmd.Parameters.AddWithValue("User", pm.User);
                int matsach = 0;
                using (MySqlConnection conn1 = GetConnection())
                {
                    conn1.Open();
                    var str1 = "select MatSach from docgia where MaDG=@MaDG";
                    MySqlCommand cmd1 = new MySqlCommand(str1, conn1);
                    cmd1.Parameters.AddWithValue("MaDG", pm.MaDG);
                    using (var reader = cmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            matsach = int.Parse(reader["MatSach"].ToString());
                        }
                    }
                    if (matsach == 3)
                        return 100;
                }
                return (cmd.ExecuteNonQuery());
            }
        }
        public int UpdatePhieuMuon(PhieuMuon pm)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update phieumuon set MaDG=@MaDG,MaSach=@MaSach,NgayMuon=@NgayMuon,NgayHenTra=@NgayHenTra,SoLuongMuon=@SoLuongMuon,User=@User where MaPM=@MaPM";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaDG", pm.MaDG);
                cmd.Parameters.AddWithValue("MaSach", pm.MaSach);
                cmd.Parameters.AddWithValue("NgayMuon", pm.NgayMuon);
                cmd.Parameters.AddWithValue("NgayHenTra", pm.NgayHenTra);
                cmd.Parameters.AddWithValue("SoLuongMuon", pm.SoLuongMuon);
                cmd.Parameters.AddWithValue("User", pm.User);
                cmd.Parameters.AddWithValue("MaPM", pm.MaPM);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int UpdateSoLuongSach(string id)
        {
            int demsoluong = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select*from phieutra where MaPM=@mapm";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapm", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (string.Compare(reader["MaPM"].ToString(), id, false) == 0)
                        {
                            demsoluong++;
                        }
                    }
                    reader.Close();
                }
                conn.Close();

            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update phieumuon set SoLuongMuon=@SoLuongMuon where MaPM=@MaPM";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("SoLuongMuon", demsoluong);
                cmd.Parameters.AddWithValue("MaPM", id);
                return (cmd.ExecuteNonQuery());
            }
        }
        public bool DeletePhieuMuon(string maphieumuon)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string delPhieuTraCmd = "Delete from phieutra where MaPM=@mapm";
                MySqlCommand cmd = new MySqlCommand(delPhieuTraCmd, conn);
                cmd.Parameters.AddWithValue("mapm", maphieumuon);

                bool check = cmd.ExecuteNonQuery() >= 0;
                if (check)
                {
                    var delPhieuMuonCmd = "Delete from phieumuon where MaPM=@mapm";
                    MySqlCommand cmd1 = new MySqlCommand(delPhieuMuonCmd, conn);
                    cmd1.Parameters.AddWithValue("mapm", maphieumuon);
                    return cmd1.ExecuteNonQuery() > 0;
                }
                return false;
            }
        }
        /*public int DeletePhieuMuon(PhieuMuon pm)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from phieumuon where MaPM=@mapm";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapm", pm.MaPM);
                return (cmd.ExecuteNonQuery());
            }
        }*/
        public PhieuMuon GetPhieuMuonByMaPM(string id)
        {
            PhieuMuon pm = new PhieuMuon();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from phieumuon where MaPM=@mapm";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapm", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pm.MaPM = reader["MaPM"].ToString();
                        pm.MaDG = reader["MaDG"].ToString();
                        pm.NgayMuon = DateTime.Parse(reader["NgayMuon"].ToString());
                        pm.NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString());
                        pm.SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString());
                        pm.User = reader["User"].ToString();
                    }

                }
            }
            return pm;
        }
        public List<PhieuMuon> GetPhieuMuonByMADG(string id)
        {
            List<PhieuMuon> list = new List<PhieuMuon>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieumuon where MaDG=@madg ", conn);
                cmd.Parameters.AddWithValue("madg", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhieuMuon()
                        {
                            MaPM = reader["MaPM"].ToString(),
                            MaDG = reader["MaDG"].ToString(),
                            NgayMuon = DateTime.Parse(reader["NgayMuon"].ToString()),
                            NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                            SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                            User = reader["User"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<PhieuTra> GetPhieuTra()
        {
            List<PhieuTra> list = new List<PhieuTra>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from phieutra ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader["Ngaytra"].ToString().Length != 0)
                        {

                            list.Add(new PhieuTra()
                            {
                                MaPM = reader["MaPM"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDG = reader["MaDG"].ToString(),
                                NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                                NgayTra = DateTime.Parse(reader["NgayTra"].ToString()),
                                SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                                SoLuongTra = int.Parse(reader["SoLuongTra"].ToString()),
                                TrangThai = reader["TrangThai"].ToString(),
                                User = reader["User"].ToString(),
                                TienPhat = double.Parse(reader["TienPhat"].ToString()),

                            });
                        }


                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
       
        public int CreatePhieuTra(PhieuTra pt)
        {
            int soluongsach = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into phieutra values(@MaPM, @MaSach, @MaDG, @NgayHenTra, @NgayTra," +
                    "@SoLuongMuon, @SoLuongTra,@TrangThai, @User,@TienPhat)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaPM", pt.MaPM);
                cmd.Parameters.AddWithValue("MaSach", pt.MaSach);
                cmd.Parameters.AddWithValue("MaDG", pt.MaDG);
                cmd.Parameters.AddWithValue("NgayHenTra", pt.NgayHenTra);
                cmd.Parameters.AddWithValue("NgayTra", pt.NgayTra);
                cmd.Parameters.AddWithValue("SoLuongMuon", pt.SoLuongMuon);
                cmd.Parameters.AddWithValue("SoLuongTra", pt.SoLuongTra);
                cmd.Parameters.AddWithValue("TrangThai", pt.TrangThai);
                cmd.Parameters.AddWithValue("User", pt.User);
                cmd.Parameters.AddWithValue("TienPhat", pt.TienPhat);
                cmd.ExecuteNonQuery();
                using (MySqlConnection conn3 = GetConnection())
                {
                    conn3.Open();
                    var str3 = "Select soLuong from sach where MaSach=@MaSach";
                    MySqlCommand cmd3 = new MySqlCommand(str3, conn3);
                    cmd3.Parameters.AddWithValue("MaSach", pt.MaSach);
                    using (var reader3 = cmd3.ExecuteReader())
                    {
                        while (reader3.Read())
                        {
                            soluongsach = int.Parse(reader3["soLuong"].ToString());
                        }

                    }
                }
                using (MySqlConnection conn2 = GetConnection())
                {
                    conn2.Open();
                    var str2 = "update sach set soLuong=@soluongsach where MaSach=@MaSach";
                    MySqlCommand cmd2 = new MySqlCommand(str2, conn2);
                    cmd2.Parameters.AddWithValue("soluongsach", soluongsach - 1);
                    cmd2.Parameters.AddWithValue("masach", pt.MaSach);
                    return cmd2.ExecuteNonQuery();
                }
            }



        }
        
        public int DeletePhieuTra(PhieuTra pt)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from phieutra where MaPM=@mapm";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapm", pt.MaPM);
                return (cmd.ExecuteNonQuery());
            }
        }
        public PhieuTra GetPhieuTraByMaPM(string id, string masach, string madocgia)
        {
            PhieuTra pt = new PhieuTra();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from phieutra where MaPM=@mapm and MaSach=@masach and MaDG=@madocgia";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapm", id);
                cmd.Parameters.AddWithValue("masach", masach);
                cmd.Parameters.AddWithValue("madocgia", madocgia);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["NgayTra"].ToString().Length == 0)
                        {
                            pt.MaPM = reader["MaPM"].ToString();
                            pt.MaSach = reader["MaSach"].ToString();
                            pt.MaDG = reader["MaDG"].ToString();
                            pt.NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString());
                            pt.NgayTra = DateTime.Parse(reader["NgayTra"].ToString());
                            pt.SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString());
                            pt.SoLuongTra = int.Parse(reader["SoLuongTra"].ToString());
                            pt.TrangThai = reader["TrangThai"].ToString() ;
                            pt.User = reader["User"].ToString();
                        }
                        else
                        {
                            pt.MaPM = reader["MaPM"].ToString();
                            pt.MaSach = reader["MaSach"].ToString();
                            pt.MaDG = reader["MaDG"].ToString();
                            pt.NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString());
                            pt.NgayTra = DateTime.Parse(reader["NgayTra"].ToString());
                            pt.SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString());
                            pt.SoLuongTra = int.Parse(reader["SoLuongTra"].ToString());
                            pt.TrangThai = reader["TrangThai"].ToString();
                            pt.User = reader["User"].ToString();
                        }

                    }

                }
            }
            return pt;
        }

        public List<PhieuTra> GetPhieuChuaTra()
        {
            List<PhieuTra> list = new List<PhieuTra>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieutra ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["TrangThai"].ToString().Length != 0)
                        {
                            list.Add(new PhieuTra()
                            {
                                MaPM = reader["MaPM"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDG = reader["MaDG"].ToString(),
                                NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                                NgayTra = DateTime.Parse(reader["NgayTra"].ToString()),
                                SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                                SoLuongTra = int.Parse(reader["SoLuongTra"].ToString()),
                                TrangThai = (string)null,
                                User = reader["User"].ToString(),
                                TienPhat = double.Parse(reader["TienPhat"].ToString()),
                            });
                        }
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<PhieuTra> GetPhieuTraByMaPM(string id)
        {
            List<PhieuTra> list = new List<PhieuTra>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieutra where MaPM=@mapm", conn);
                cmd.Parameters.AddWithValue("mapm", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader["Ngaytra"].ToString().Length != 0)
                        {
                            list.Add(new PhieuTra()
                            {
                                MaPM = reader["MaPM"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDG = reader["MaDG"].ToString(),
                                NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                                NgayTra = DateTime.Parse(reader["NgayTra"].ToString()),
                                SoLuongMuon = int.Parse(reader["TinhTrangSach"].ToString()),
                                SoLuongTra = int.Parse(reader["TinhTrangTra"].ToString()),
                                User = reader["User"].ToString(),
                                TienPhat = double.Parse(reader["TienPhat"].ToString()),
                            });
                        }
                        else
                        {
                            list.Add(new PhieuTra()
                            {
                                MaPM = reader["MaPM"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDG = reader["MaDG"].ToString(),
                                NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                                NgayTra = DateTime.Parse(reader["NgayTra"].ToString()),
                                SoLuongMuon = int.Parse(reader["TinhTrangSach"].ToString()),
                                SoLuongTra = int.Parse(reader["TinhTrangTra"].ToString()),
                                User = reader["User"].ToString(),
                                TienPhat = double.Parse(reader["TienPhat"].ToString()),
                            });
                        }


                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<PhieuTra> GetPhieuTraBiPhat()
        {
            List<PhieuTra> list = new List<PhieuTra>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieutra ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader["Ngaytra"].ToString().Length != 0 && double.Parse(reader["TienPhat"].ToString()) != 0)
                        {
                            list.Add(new PhieuTra()
                            {
                                MaPM = reader["MaPM"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDG = reader["MaDG"].ToString(),
                                NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                                NgayTra = DateTime.Parse(reader["NgayTra"].ToString()),
                                SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                                SoLuongTra = int.Parse(reader["SoLuongTra"].ToString()),
                                TrangThai = (string)null,
                                User = reader["User"].ToString(),
                                TienPhat = double.Parse(reader["TienPhat"].ToString()),

                            });
                        }


                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public int Login(string id, string pass)
        {
            List<TaiKhoan> list = new List<TaiKhoan>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from taikhoan ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaiKhoan()
                        {
                            User = reader["User"].ToString(),
                            PassWord = reader["PassWord"].ToString(),
                            PhanQuyen = int.Parse(reader["PhanQuyen"].ToString()),
                            TenND = reader["TenND"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            CMND = reader["CMND"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            foreach (TaiKhoan tk in list)
            {
                if (string.Compare(id, tk.User, false) == 0)
                {
                    if (string.Compare(pass, tk.PassWord, false) == 0)
                    {
                        if (tk.PhanQuyen == 1)
                            return 1;
                        else return 0;
                    }
                }
            }
            return -1;
        }
        public int DoiMK(DoiMK s)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update taikhoan set  PassWord=@PassWord where User=@User";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("PassWord", s.PassWord);
                cmd.Parameters.AddWithValue("User", s.User);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int DemSach()
        {
            int soluongsach1 = 0;
            List<Sach> list = new List<Sach>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from SACH ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        soluongsach1++;
                        list.Add(new Sach()
                        {
                            MaSach = reader["MaSach"].ToString(),
                            TenSach = reader["TenSach"].ToString(),
                            TenTG = reader["TenTG"].ToString(),
                            NhaXB = reader["NhaXB"].ToString(),
                            TheLoai = reader["TheLoai"].ToString(),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            GiaTien = double.Parse(reader["GiaTien"].ToString()),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return soluongsach1;

        }
        public int DemDocGia()
        {
            int soluong = 0;
            List<DocGia> list = new List<DocGia>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from docgia ", conn);

                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        soluong++;
                        list.Add(new DocGia()
                        {
                            MaDG = reader["MaDG"].ToString(),
                            TenDG = reader["TenDG"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            MatSach = int.Parse(reader["MatSach"].ToString()),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return soluong;
        }
        public int DemPhieuMuon()
        {
            int soluongphieumuon = 0;
            List<PhieuMuon> list = new List<PhieuMuon>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieumuon ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        soluongphieumuon++;
                        list.Add(new PhieuMuon()
                        {
                            MaPM = reader["MaPM"].ToString(),
                            MaDG = reader["MaDG"].ToString(),
                            MaSach = reader["MaSach"].ToString(),
                            NgayMuon = DateTime.Parse(reader["NgayMuon"].ToString()),
                            NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                            SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                            User = reader["User"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return soluongphieumuon;
        }
        public int DemPhieuTra()
        {
            List<PhieuTra> list = new List<PhieuTra>();
            int soluongphieutra = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieutra ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader["TrangThai"].ToString().Length != 0)
                        {
                            list.Add(new PhieuTra()
                            {
                                MaPM = reader["MaPM"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDG = reader["MaDG"].ToString(),
                                NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                                NgayTra = DateTime.Parse(reader["NgayTra"].ToString()),
                                SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                                SoLuongTra = int.Parse(reader["SoLuongTra"].ToString()),
                                TrangThai = (string)null,
                                User = reader["User"].ToString(),
                                TienPhat = double.Parse(reader["TienPhat"].ToString()),

                            });
                        }
                        else
                        {
                            soluongphieutra++;
                            string maPM = reader["MaPM"].ToString();
                            string maSach = reader["MaSach"].ToString();
                            string maDG = reader["MaDG"].ToString();
                            string ngayHenTra = reader["NgayHenTra"].ToString();
                            string ngayTra = reader["NgayTra"].ToString();
                            string soLuongMuon = reader["SoLuongMuon"].ToString();
                            string soLuongTra = reader["SoLuongTra"].ToString();
                            string trangThai = reader["TrangThai"].ToString();
                            string user = reader["User"].ToString();
                            string tienPhat = reader["TienPhat"].ToString();

                            list.Add(new PhieuTra()
                            {

                                MaPM = maPM,
                                MaSach = maSach,
                                MaDG = maDG,
                                NgayHenTra = DateTime.Parse(ngayHenTra),
                                NgayTra = DateTime.Parse(ngayTra),
                                SoLuongMuon = int.Parse(soLuongMuon),
                                SoLuongTra = int.Parse(soLuongTra),
                                TrangThai = trangThai,
                                User = user,
                                TienPhat = double.Parse(tienPhat),

                            });
                        }


                    }
                    reader.Close();
                }
                conn.Close();
            }
            return soluongphieutra;
        }
        public int DemPhieuChuaTra()
        {
            List<PhieuTra> list = new List<PhieuTra>();
            int soluongphieuchuatra = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieutra ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["TrangThai"].ToString().Length != 0)
                        {
                            soluongphieuchuatra++;
                            list.Add(new PhieuTra()
                            {
                                MaPM = reader["MaPM"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDG = reader["MaDG"].ToString(),
                                NgayHenTra = DateTime.Parse(reader["NgayHenTra"].ToString()),
                                NgayTra = DateTime.Parse(reader["NgayTra"].ToString()),
                                SoLuongMuon = int.Parse(reader["SoLuongMuon"].ToString()),
                                SoLuongTra = int.Parse(reader["SoLuongTra"].ToString()),
                                TrangThai = reader["TrangThai"].ToString(),
                                User = reader["User"].ToString(),
                                TienPhat = double.Parse(reader["TienPhat"].ToString()),
                            });
                        }
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return soluongphieuchuatra;
        }
        public double DemDoanhThu()
        {
            double tong = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from phieutra ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Ngaytra"].ToString().Length != 0)
                        {
                            tong = tong + double.Parse(reader["TienPhat"].ToString());
                        }
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return tong;
        }
    }
}
