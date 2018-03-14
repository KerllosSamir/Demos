using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnglr.Models;

namespace TestAnglr.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee  
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>  
        ///   
        /// Get All Employee  
        /// </summary>  
        /// <returns></returns>  
        public JsonResult Get_AllEmployee()
        {
            using (AHCC_Mowah_AssetsAndInventory_DevEntities Obj = new AHCC_Mowah_AssetsAndInventory_DevEntities())
            {
                List<Employee> Emp = Obj.Employees.ToList();
                return Json(Emp, JsonRequestBehavior.AllowGet);
            }
        }
        //[Route("Get_AllEmployee/{RowsPerPage}/{PageIndex}")]
        public JsonResult Search(int RowsPerPage, int PageIndex)
        {
            using (AHCC_Mowah_AssetsAndInventory_DevEntities Obj = new AHCC_Mowah_AssetsAndInventory_DevEntities())
            {
                var q = Obj.Employees.AsQueryable();
                string ColName = "Emp_Id";
                string SortOrder = "";
                if (!string.IsNullOrEmpty(ColName))
                {
                    if (SortOrder.ToString() == "asc")
                        switch (ColName)
                        {
                            case "Emp_Id":
                                q = q.OrderBy(dm => dm.Emp_Id);
                                break;
                            case "Emp_Name":
                                q = q.OrderBy(dm => dm.Emp_Name);
                                break;
                            case "Emp_City":
                                q = q.OrderBy(dm => dm.Emp_City);
                                break;
                            case "Emp_Age":
                                q = q.OrderBy(dm => dm.Emp_Age);
                                break;
                        }
                    else
                        switch (ColName)
                        {
                            case "Emp_Id":
                                q = q.OrderByDescending(dm => dm.Emp_Id);
                                break;
                            case "Emp_Name":
                                q = q.OrderByDescending(dm => dm.Emp_Name);
                                break;
                            case "Emp_City":
                                q = q.OrderByDescending(dm => dm.Emp_City);
                                break;
                            case "Emp_Age":
                                q = q.OrderByDescending(dm => dm.Emp_Age);
                                break;
                        }
                }
                int TotalRowCount = q.Count();

                q = q.Skip((PageIndex - 1) * RowsPerPage).Take(RowsPerPage);

                return Json(new { data = q.ToList(), total_count = TotalRowCount }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// Get Employee With Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public JsonResult Get_EmployeeById(string Id)
        {
            using (AHCC_Mowah_AssetsAndInventory_DevEntities Obj = new AHCC_Mowah_AssetsAndInventory_DevEntities())
            {
                int EmpId = int.Parse(Id);
                return Json(Obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// Insert New Employee  
        /// </summary>  
        /// <param name="Employe"></param>  
        /// <returns></returns>  
        public string Insert_Employee(Employee Employe)
        {
            if (Employe != null)
            {
                using (AHCC_Mowah_AssetsAndInventory_DevEntities Obj = new AHCC_Mowah_AssetsAndInventory_DevEntities())
                {
                    Obj.Employees.Add(Employe);
                    Obj.SaveChanges();
                    return "Employee Added Successfully";
                }
            }
            else
            {
                return "Employee Not Inserted! Try Again";
            }
        }
        /// <summary>  
        /// Delete Employee Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Delete_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (AHCC_Mowah_AssetsAndInventory_DevEntities Obj = new AHCC_Mowah_AssetsAndInventory_DevEntities())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.Employees.Attach(Emp);
                        Obj.Employees.Remove(Emp);
                    }
                    Obj.SaveChanges();
                    return "Employee Deleted Successfully";
                }
            }
            else
            {
                return "Employee Not Deleted! Try Again";
            }
        }
        /// <summary>  
        /// Update Employee Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Update_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (AHCC_Mowah_AssetsAndInventory_DevEntities Obj = new AHCC_Mowah_AssetsAndInventory_DevEntities())
                {
                    var Emp_ = Obj.Entry(Emp);
                    Employee EmpObj = Obj.Employees.Where(x => x.Emp_Id == Emp.Emp_Id).FirstOrDefault();
                    EmpObj.Emp_Age = Emp.Emp_Age;
                    EmpObj.Emp_City = Emp.Emp_City;
                    EmpObj.Emp_Name = Emp.Emp_Name;
                    Obj.SaveChanges();
                    return "Employee Updated Successfully";
                }
            }
            else
            {
                return "Employee Not Updated! Try Again";
            }
        }
    }

}