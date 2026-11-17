using Microsoft.EntityFrameworkCore;
using Payrollmanagementsystem.Models;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 



namespace PayrollManagementSystem.Repositories
{
    public class SalaryStructureRepository : ISalaryStructure
    {
        private readonly ApplicationDbContext context;
        public SalaryStructureRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ApiResultVm> AddSalaryStructure(SalaryStructureVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                SalaryStructure salaryStructure = new SalaryStructure
                {
                    Id = model.Id,
                    EmployeeCode=model.EmployeeCode,
                    BasicPay = model.BasicPay,
                    Allowances = model.Allowances,
                    Deductions = model.Deductions,
                    EffectiveFrom = model.EffectiveFrom,
                    EffectiveTo = model.EffectiveTo,
                    LastUpdatedOn = model.LastUpdatedOn,
                    IsActive = model.IsActive
                };
                context.SalaryStructures.Add(salaryStructure);
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

        public async Task<ApiResultVm> UpdateSalaryStructure(SalaryStructureVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var salaryStructure = await context.SalaryStructures.FirstOrDefaultAsync(e=>e.EmployeeCode==model.EmployeeCode);
                if (salaryStructure == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Salary structure not found" };
                    return result;
                }
                salaryStructure.BasicPay = model.BasicPay;
                salaryStructure.Allowances = model.Allowances;
                salaryStructure.Deductions = model.Deductions;
                salaryStructure.EffectiveFrom = model.EffectiveFrom;
                salaryStructure.EffectiveTo = model.EffectiveTo;
                salaryStructure.LastUpdatedOn = DateTime.Now;
                salaryStructure.IsActive = model.IsActive;
                context.SalaryStructures.Update(salaryStructure);
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

        public async Task<RSalaryStructureVm> GetSalaryStructureByEmployeeCode(int EmployeeCode)
        {
            RSalaryStructureVm result = new RSalaryStructureVm();

            try
            {
                var res = await context.SalaryStructures.FirstOrDefaultAsync(x => x.EmployeeCode == EmployeeCode);
                if (res != null)
                {
                    SalaryStructureVm salaryStructure = new SalaryStructureVm()
                    {


                        BasicPay = res.BasicPay,
                        Allowances = res.Allowances,
                        Deductions = res.Deductions,
                        EffectiveFrom = res.EffectiveFrom,
                        EffectiveTo = res.EffectiveTo,
                        IsActive = res.IsActive
                    };


                    result.Data = salaryStructure;
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

        public async Task<ApiResultVm> DeleteSalaryStructure(int EmployeeCode)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var salaryStructure = await context.SalaryStructures.FirstOrDefaultAsync(x => x.EmployeeCode == EmployeeCode);
                if (salaryStructure == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Salary structure not found" };
                    return result;
                }
                context.SalaryStructures.Remove(salaryStructure);
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

        public async Task<RSalaryStructureListVm> GetAllSalaryStructures(int PageN0, int PageSize)
        {
            RSalaryStructureListVm result = new RSalaryStructureListVm();
            try
            {
                var salaryStructures = await context.SalaryStructures
                    .Skip((PageN0 - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
                List<SalaryStructureVm> salaryStructureVms = new List<SalaryStructureVm>();
                foreach (var salaryStructure in salaryStructures)
                {
                    SalaryStructureVm salaryStructureVm = new SalaryStructureVm
                    {
                        Id = salaryStructure.Id,
                        EmployeeCode = salaryStructure.EmployeeCode,
                        BasicPay = salaryStructure.BasicPay,
                        Allowances = salaryStructure.Allowances,
                        Deductions = salaryStructure.Deductions,
                        EffectiveFrom = salaryStructure.EffectiveFrom,
                        EffectiveTo = salaryStructure.EffectiveTo,
                        LastUpdatedOn = salaryStructure.LastUpdatedOn,
                        IsActive = salaryStructure.IsActive
                    };
                    salaryStructureVms.Add(salaryStructureVm);
                }
                result.Data = salaryStructureVms;
                result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }





        public async Task<bool> IsSalaryStructureExists(int employeeCode)
        {
            return await context.SalaryStructures.AnyAsync(s => s.EmployeeCode == employeeCode);
        }

        public async Task<ApiResultVm> ActivateSalaryStructure(int id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var salaryStructure = await context.SalaryStructures.FindAsync(id);
                if (salaryStructure == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Salary structure not found" };
                    return result;
                }
                salaryStructure.IsActive = true;
                context.SalaryStructures.Update(salaryStructure);
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

        public async Task<ApiResultVm> DeactivateSalaryStructure(int EmployeeCode)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var salaryStructure = await context.SalaryStructures.FirstOrDefaultAsync(x => x.EmployeeCode == EmployeeCode);
                if (salaryStructure == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Salary structure not found" };
                    return result;
                }
                salaryStructure.IsActive = false;
                context.SalaryStructures.Update(salaryStructure);
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
    }
}
