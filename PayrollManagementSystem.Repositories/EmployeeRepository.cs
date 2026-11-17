using Microsoft.EntityFrameworkCore;
using Payrollmanagementsystem.Models;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using X.PagedList.Extensions;

namespace PayrollManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context) 
        { 
            this.context = context;
        }

       public async Task<ApiResultVm> Add(EmployeeVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Employee employee = new Employee
                {
                    EmployeeName = model.EmployeeName,
                    EmployeeEmail = model.EmployeeEmail,
                    EmployeePhone = model.EmployeePhone,
                    DateOfJoining = model.DateOfJoining,
                    EmployeeCode= model.EmployeeCode,
                    DepartmentCode = model.DepartmentCode,
                    DesignationCode = model.DesignationCode,
                    Gender = model.Gender,
                    Address = model.Address,
                    CreatedOn=model.CreatedOn,
                    UpdatedOn=model.UpdatedOn
                };
                await context.Employees.AddAsync(employee);
                var res = await context.SaveChangesAsync();
                if (res > 0)
                {
                    result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Server Error" };
                }
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<ApiResultVm> DeleteEmployee( int EmployeeCode)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Employees.FirstOrDefaultAsync(X => X.EmployeeCode == EmployeeCode);
                if (res == null) 
                { 
                   result = new ApiResultVm { Succeeded = false, Message = "Employee Not Found" };
                    return result;
                }
                context.Employees.Remove(res);
                var res2 = await context.SaveChangesAsync();
                if (res2 > 0)
                result = new ApiResultVm { Succeeded = true, Message = "Success" };
                else result = new ApiResultVm { Succeeded = false, Message = "Server Error" };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<REmployeeListVm> GetAllEmployees(int PageNo,int PageSize)
        {
            REmployeeListVm result = new REmployeeListVm();
            try
            {
                var res = context.Employees.ToPagedList(PageNo, PageSize);
                if (res != null)
                {
                    List<EmployeeVm> employeeList = new List<EmployeeVm>();
                    foreach (var item in res)
                    {
                        EmployeeVm employee = new EmployeeVm
                        {
                            EmployeeCode = item.EmployeeCode,
                            EmployeeName = item.EmployeeName,
                            EmployeeEmail = item.EmployeeEmail,
                            EmployeePhone = item.EmployeePhone,
                            DateOfJoining = item.DateOfJoining,
                            Gender = item.Gender,
                            Address = item.Address,
                        };
                        employeeList.Add(employee);
                    }
                    result.Data = employeeList;
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<REmployeeVm> GetEmployeeByDepartment(int DepartmentCode)
        {
            REmployeeVm result= new REmployeeVm();
            try
            {
                var res = await context.Employees.FirstOrDefaultAsync(X => X.DepartmentCode == DepartmentCode);
                if (res != null)
                {
                    EmployeeVm employee = new EmployeeVm
                    {
                        EmployeeCode = res.EmployeeCode,
                        DepartmentCode = res.DepartmentCode,
                        DesignationCode = res.DesignationCode,
                        EmployeeName = res.EmployeeName,
                        EmployeeEmail = res.EmployeeEmail,
                        EmployeePhone = res.EmployeePhone,
                        DateOfJoining = res.DateOfJoining,
                        Gender = res.Gender,
                        Address = res.Address,
                    };
                    result.Data = employee;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Employee Not Found" };
                }


            }
            catch (Exception ex) { 
            result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<REmployeeVm> GetEmployeeByDesignation(int DesignationCode)
        {
            REmployeeVm result = new REmployeeVm();
            try
            {
                var res = await context.Employees.FirstOrDefaultAsync(X => X.DesignationCode == DesignationCode);
                if (res != null)
                {
                    EmployeeVm employee = new EmployeeVm()
                    {
                        EmployeeCode = res.EmployeeCode,
                        DepartmentCode = res.DepartmentCode,
                        EmployeeName = res.EmployeeName,
                        EmployeeEmail = res.EmployeeEmail,
                        EmployeePhone = res.EmployeePhone,
                        DateOfJoining = res.DateOfJoining,
                        Gender = res.Gender
                    };
                    result.Data = employee;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };

                }  
                else
                    {
                        result.Result = new ApiResultVm { Succeeded = false, Message = "Employee Not Found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public  async Task<REmployeeVm> GetEmployeeByEmployeeCode(int EmployeeCode)
        {
            REmployeeVm result = new REmployeeVm();
            try
            {
                var res = await context.Employees.FirstOrDefaultAsync(X => X.EmployeeCode==EmployeeCode);
                if (res != null)
                {
                    EmployeeVm employee = new EmployeeVm
                    {
                        EmployeeCode = res.EmployeeCode,
                        DesignationCode=res.DesignationCode,
                        EmployeeName = res.EmployeeName,
                        EmployeeEmail = res.EmployeeEmail,
                        EmployeePhone = res.EmployeePhone,
                        DateOfJoining = res.DateOfJoining,
                        Gender = res.Gender,
                    };
                    result.Data = employee;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Employee Not Found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<REmployeeVm> GetEmployeeByName(string name)
        {
            REmployeeVm result = new REmployeeVm();
            try
            {
                var res= await context.Employees.FirstOrDefaultAsync(e => e.EmployeeName == name);  
                if (res != null)
                {
                    EmployeeVm employee = new EmployeeVm
                    {
                        EmployeeCode = res.EmployeeCode,
                        EmployeeName = res.EmployeeName,
                        EmployeeEmail = res.EmployeeEmail,
                        EmployeePhone = res.EmployeePhone,
                        DateOfJoining = res.DateOfJoining,
                        Gender = res.Gender,
                    };
                    result.Data = employee;
                    result.Result = new ApiResultVm { Message = "Success", Succeeded = true };
                }
                else
                {
                    result.Result = new ApiResultVm { Message = "Employee Not Found", Succeeded = false };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Message = ex.Message, Succeeded = false };
            }
            return result;
        }
        public async Task<bool> IsEmailExists(string email)
        {
            try
            {
                var res= await context.Employees.AnyAsync(e => e.EmployeeEmail == email);
                return res;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> IsPhoneNumberExists(string phoneNumber)
        {
            try
            {
                var res = await context.Employees.AnyAsync(e => e.EmployeePhone == phoneNumber);
                return res;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<ApiResultVm> UpdateEmployee(EmployeeVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Employees.FindAsync(model.EmployeeCode);
                if (res == null) return new ApiResultVm { Succeeded = false, Message = "Employee Not Found" };
                {
                    res.EmployeeName = model.EmployeeName;
                    res.EmployeeEmail = model.EmployeeEmail;
                    res.EmployeePhone = model.EmployeePhone;
                    res.EmployeeCode = model.EmployeeCode;
                    res.DateOfJoining = model.DateOfJoining;
                    res.Gender = model.Gender;

                    context.Employees.Update(res);
                    var res2 = await context.SaveChangesAsync();
                    result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }

            } catch (Exception ex) 
                {
                    result = new ApiResultVm {Message=ex.Message, Succeeded = false};
                }
                return result;


        }
        public async Task<ApiResultVm> ActivateEmployee(int EmployeeCode)
        {
            {
                ApiResultVm result = new ApiResultVm();
                try
                {
                    var res = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeCode == EmployeeCode);
                    if (res == null)
                    {
                        result = new ApiResultVm { Message = "Employee not found", Succeeded = false };
                        return result;
                    }
                    res.IsActive = true;

                    var res2 = await context.SaveChangesAsync();

                    if (res2 > 0)
                    {
                        result = new ApiResultVm { Message = "Employee activated successfully", Succeeded = true };
                    }
                    else
                    {
                        result = new ApiResultVm { Message = "Unable to activate Employee", Succeeded = false };
                    }

                }
                catch (Exception ex)
                {
                    result = new ApiResultVm { Message = ex.Message, Succeeded = false };
                }
                return result;
            }
        }
        public async Task<ApiResultVm> DeactivateEmployee(int EmployeeCode)
        {
            ApiResultVm result = new ApiResultVm();

            try
            {
                var department = await context.Employees
                    .FirstOrDefaultAsync(x => x.EmployeeCode == EmployeeCode);

                if (department == null)
                {
                    result = new ApiResultVm
                    {
                        Succeeded = false,
                        Message = "Employee Not Found"
                    };

                    return result;
                }

                if (!department.IsActive)
                {
                    result = new ApiResultVm
                    {
                        Succeeded = false,
                        Message = "Employee Is Already Deactivated"
                    };

                    return result;
                }

                department.IsActive = false;

                var res = await context.SaveChangesAsync();

                if (res > 0)
                {
                    result = new ApiResultVm
                    {
                        Succeeded = true,
                        Message = "Employee Deactivated Successfully"
                    };
                }
                else
                {
                    result = new ApiResultVm
                    {
                        Succeeded = false,
                        Message = "Server Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new ApiResultVm
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }

            return result;
        }
    }
}
   

