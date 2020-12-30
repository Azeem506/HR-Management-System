using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_Management_System.Models
{
    public class DBManager
    {
        public static void AddUser(string login, String password, String mail, String fname, String lname, String cpassword)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"INSERT INTO dbo.User1(Login, Password, Mail, FName, LName, CPassword) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", login, password, mail, fname, lname, cpassword);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }

        }

        public static void Login(int login, String password, String mail, String fname, String lname, String cpassword)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"INSERT INTO dbo.User1(Login, Password, Mail, FName, LName, CPassword) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", login, password, mail, fname, lname, cpassword);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }

        }


        public static bool Validate(String Login, String Password)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Select Login from dbo.User1 where Login='{0}' and Password='{1}' ", Login, Password);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    UserDTO userDTO = new UserDTO();
                    UserDTO dto = userDTO;
                    dto.Login = reader.GetString(0);

                    return true;
                }

                return false;
            }

        }


        internal static void AddUser(string first, string last, string mail, string password, string confirm)
        {
            throw new NotImplementedException();
        }

        public static void AddContact(string first, string last, string mail, string comment)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"INSERT INTO dbo.Contact(FName, LName, Email, Comment) VALUES('{0}','{1}','{2}','{3}')", first, last, mail, comment);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }


        public static bool ValidateEmail(String valMail)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Select Mail from dbo.User1 where Mail='{0}'", valMail);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() == true)
                {
                    String email = reader.GetString(reader.GetOrdinal("Mail"));
                    if (email == valMail)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }

        }


        public static void UpdatePass(String mail, String newpass, String confirm)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Update dbo.User1 SET Password='{0}',CPassword='{1}' WHERE Mail='{2}'", newpass, confirm, mail);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }


        public static void AddCompany(String name, String user, String password, String mail, String phone, String image)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            //int u = 1;
            //int r = 1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"INSERT INTO dbo.Company(CmpName,UserName, Password, Email, Phone, Image) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", name, user, password, mail, phone, image);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }

        }


        public static void UpdateCompany(String id, String name, String user, String password, String mail, String phone)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Update dbo.Company SET CmpName='{0}',UserName='{1}',Password='{2}',Email='{3}',Phone='{4}' WHERE CmpId='{5}'", name, user, password, mail, phone, id);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }

        public static void DeleteCompany(String id)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Delete from dbo.Company  WHERE CmpId='{0}'", id);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }
        public static List<CompanyDTO> ShowProfile(int id)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                CompanyDTO dto = new CompanyDTO();
                conn.Open();

                String sqlQuery = String.Format(@"Select * from dbo.Company WHERE CmpId='{0}'", id);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<CompanyDTO> cmplist = new List<CompanyDTO>();

                while (reader.Read() == true)
                {
                    dto.CmpId = reader.GetString(reader.GetOrdinal("CmpId"));
                    dto.CmpName = reader.GetString(reader.GetOrdinal("CmpName"));
                    dto.userName = reader.GetString(reader.GetOrdinal("UserName"));
                    dto.Password = reader.GetString(reader.GetOrdinal("Password"));
                    dto.Email = reader.GetString(reader.GetOrdinal("Email"));
                    dto.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    dto.Image = reader.GetString(reader.GetOrdinal("Image"));

                    cmplist.Add(dto);


                }

                return cmplist;
            }

        }
        public static void AddDepartment(String user, String password, String mail, String phone, String name, String cname)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            //int u = 1;
            //int r = 1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"INSERT INTO dbo.Department(UserName,Password,Email, Phone, DeptName,CmpName ) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", user, password, mail, phone, name, cname);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }

        }
        public static void UpdateDepartment(String id, String user, String password, String mail, String phone, String name)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Update dbo.Department SET UserName='{0}',Password='{1}',Email='{2}',Phone='{3}',DeptName='{4}' WHERE DeptId='{5}'", user, password, mail, phone, name, id);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }
        public static void DeleteDepartment(string id)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Delete from dbo.Department  WHERE DeptId='{0}'", id);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }

        public static void AddEmployee(String fname, String lname, String user, String password, String mail, String phone, String image, String cname,String dname)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            //int u = 1;
            //int r = 1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"INSERT INTO dbo.Employee(FName,LName,UserName, Password, Email, Phone,EmpImage,CmpName,DeptName) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", fname, lname, user, password, mail, phone, image, cname, dname); ;

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }

        }

        public static List<Company> GetCmpName()
        {
            List<Company> list = new List<Company>(); 
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Select * from dbo.Company");

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() == true)
                {
                    Company cmp = new Company();
                    cmp.CmpName = reader["CmpName"].ToString();

                    list.Add(cmp);
                    //String email = reader.GetString(reader.GetOrdinal("CmpName"));

                }
                return list;
            }
        }
        public static void UpdateEmployee(string id, String fname, String lname, String user, String password, String mail, String phone)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Update dbo.Employee SET FName='{0}',LName='{1}',UserName='{2}',Password='{3}',Email='{4}',Phone='{5}' WHERE EmpId='{6}'", fname, lname, user, password, mail, phone, id);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }

        public static void DeleteEmployee(string id)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"Delete from dbo.Employee  WHERE EmpId='{0}'", id);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }
        }

        public static void AddSalary(String basicsal, String allowance, String medallowance, String tax, String labour, String fund, String totalsal, String empcompany, String empdept, String empsal)
        {
            String connString = @"Data Source=DESKTOP-8RFI652\SQLEXPRESS;Initial Catalog=Eazisols;Integrated Security=True";
            //int u = 1;
            //int r = 1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sqlQuery = String.Format(@"INSERT INTO dbo.Salary(Basic,Allowance,Medical,Tax,LabourWelfare, Fund,NatSalary,CmpName,DeptName,EmpName) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", basicsal, allowance, medallowance, tax, labour, fund, totalsal, empcompany, empdept, empsal); ;

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                int rec = cmd.ExecuteNonQuery();


            }

        }
    }
}