using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Payrollmanagementsystem.Models;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using X.PagedList.Extensions;

namespace PayrollManagementSystem.Repositories
{
    public class DepartmentRepository : IDepartment

    {
        private readonly ApplicationDbContext context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ApiResultVm> Add(DepartmentVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Department department = new Department
                {
                    Id = model.Id,

                    DeptName = model.DeptName,
                    DepartmentCode = model.DepartmentCode,
                    Description = model.Description,


                };
                context.Departments.Add(department);
                var res = await context.SaveChangesAsync();
                if (res > 0) result = new ApiResultVm { Succeeded = true, Message = "Success" };
                else result = new ApiResultVm { Succeeded = false, Message = "Server Error" };

            }
            catch (Exception ex)

            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }


            return result;
        }
        public async Task<ApiResultVm> UpdateDepartment(DepartmentVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentCode ==model.DepartmentCode);
                if (res == null) return new ApiResultVm { Succeeded = false, Message = "Invalid Item" };

                res.DeptName = model.DeptName;
                res.DepartmentCode = model.DepartmentCode;
                res.Description = model.Description;




                context.Departments.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Update Successfully! Updated Count:" + res2 };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }

            return result;
        }
        public async Task<ApiResultVm> DeleteDepartment(int DepartmentCode)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentCode == DepartmentCode);
                if (res == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Department not Found" };
                    return result;
                }
                context.Departments.Remove(res);
                var res2 = await context.SaveChangesAsync();
                if (res2 > 0)
                {
                    result = new ApiResultVm { Succeeded = true, Message = "Deleted Successfully" };

                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "unable to delete" };
                }
                return result;

            }
            catch (Exception ex)
            {
                {
                    result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                    return result;
                }
            }
        }
        public async Task<RDepartmentListVm> GetAllDepartments(int PageNo, int PageSize)
        {
            RDepartmentListVm result = new RDepartmentListVm();
            try
            {
                var res = context.Departments.ToPagedList(PageNo, PageSize);
                if (res != null)
                {
                    List<DepartmentVm> Vms = new List<DepartmentVm>();
                    foreach (var item in res)
                    {
                        {
                            DepartmentVm vm = new DepartmentVm
                            {
                                Id = item.Id,
                                DepartmentCode = item.DepartmentCode,
                                DeptName = item.DeptName,
                                Description = item.Description,

                            };
                            Vms.Add(vm);
                        }
                    }
                    result.Data = Vms;
                    result.Result = new ApiResultVm { Message = "Done", Succeeded = true };
                }
                else
                {
                    result.Result = new ApiResultVm { Message = "failed", Succeeded = false };
                }

            }

            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Message = ex.Message, Succeeded = false };
            }
            return result;
        }
        public async Task<RDepartmentVm> GetDepartmentByDepartmentCode(int DepartmentCode)
        {
            RDepartmentVm result = new RDepartmentVm();

            try
            {
                var res = await context.Departments.FirstOrDefaultAsync(x=>x.DepartmentCode==DepartmentCode);
                if (res != null)
                {
                    DepartmentVm department = new DepartmentVm()
                    {
                        Id = res.Id,
                       
                        
                        DeptName = res.DeptName,
                        Description = res.Description,

                    };
                    result.Data = department;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Something error occurred" };


                }

            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;

        }
        public async Task<int> GetEmployeeCountByDepartment(int DepartmentCode)
        {
            var count = await context.Employees.CountAsync(x => x.DepartmentCode == DepartmentCode);
            return count;
        }
        public async Task<bool> IsDepartmentCodeExists(string DepartmentCode)
        {
            return await context.Departments.AnyAsync(x => x.DepartmentCode.ToString() == DepartmentCode);
        }

        public async Task<bool> IsDepartmentNameExists(string DeptName)
        {
            return await context.Departments.AnyAsync(x => x.DeptName == DeptName);
        }
        public async Task<RDepartmentVm> SearchDepartment(int DepartmentCode)
        {
            RDepartmentVm result = new RDepartmentVm();

            try
            {
                var res = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentCode == DepartmentCode);
                if (res != null)
                {
                    DepartmentVm department = new DepartmentVm()
                    { DeptName = res.DeptName, };
                    result.Data = department;
                    result.Result = new ApiResultVm { Message = "Department name fetched ", Succeeded = true };
                    return result;
                }
                else
                {
                    result.Result = new ApiResultVm { Message = "Data not found", Succeeded = false };
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = "ex.Message" };
                return result;
            }
        }
        public async Task<ApiResultVm> ActivateDepartment(int DepartmentCode)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentCode == DepartmentCode);
                if (res == null)
                {
                    result = new ApiResultVm { Message = "Department not found", Succeeded = false };
                    return result;
                }
                res.IsActive = true;

                var res2 = await context.SaveChangesAsync();

                if (res2 > 0)
                {
                    result = new ApiResultVm { Message = "Department activated successfully", Succeeded = true };
                }
                else
                {
                    result = new ApiResultVm { Message = "Unable to activate department", Succeeded = false };
                }

            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Message = ex.Message, Succeeded = false };
            }
            return result;
        }
        public async Task<ApiResultVm> DeactivateDepartment(int DepartmentCode)
        {
            ApiResultVm result = new ApiResultVm();

            try
            {
                var department = await context.Departments
                    .FirstOrDefaultAsync(x => x.DepartmentCode == DepartmentCode);

                if (department == null)
                {
                    result = new ApiResultVm
                    {
                        Succeeded = false,
                        Message = "Department Not Found"
                    };

                    return result;
                }

                if (!department.IsActive)
                {
                    result = new ApiResultVm
                    {
                        Succeeded = false,
                        Message = "Department Is Already Deactivated"
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
                        Message = "Department Deactivated Successfully"
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
