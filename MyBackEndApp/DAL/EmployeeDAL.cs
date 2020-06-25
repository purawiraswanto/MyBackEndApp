using MyBackEndApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MyBackEndApp.DAL
{
    public class EmployeeDAL
    {
        private string GetConnStr()
        {
            return WebConfigurationManager
                .ConnectionStrings["MyConnectionString"].ConnectionString;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> lstEmployee = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Employees order by EmpName asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstEmployee.Add(new Employee
                        {
                            EmpId = Convert.ToInt32(dr["EmpId"]),
                            EmpName = dr["EmpName"].ToString(),
                            Designation = dr["Designation"].ToString(),
                            Department = dr["Department"].ToString(),
                            Qualification = dr["Qualification"].ToString(),
                            BirthDate = Convert.ToDateTime(dr["Birthdate"])
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lstEmployee;
        }

        public Employee GetEmployeeByID(int EmpID)
        {
            Employee emp = new Employee();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Employees where EmpId=@EmpId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@EmpId", EmpID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                    emp.EmpName = dr["EmpName"].ToString();
                    emp.Designation = dr["Designation"].ToString();
                    emp.Department = dr["Department"].ToString();
                    emp.Qualification = dr["Qualification"].ToString();
                    emp.BirthDate = Convert.ToDateTime(dr["Birthdate"]);
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return emp;
        }

        public void InsertEmployee(Employee emp)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                //Penting untuk membuat parameterized query seperti dibawah, untuk melindungi dari SQL Injection.
                string strSql = @"insert into Employees(EmpName,Designation,Department,Qualification,Birthdate)
                values(@EmpName,@Designation,@Department,@Qualification,@Birthdate)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Qualification", emp.Qualification);
                cmd.Parameters.AddWithValue("@Birthdate", emp.BirthDate);
                try
                {
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception("Gagal tambah data...");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

    }
}