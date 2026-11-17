using Payrollmanagementsystem.Models;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PayrollManagementSystem.Repositories
{
    public class PayrollRepository : IPayroll
    {
        private readonly ApplicationDbContext context;
        public PayrollRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ApiResultVm> GeneratePayroll(PayrollVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == model.EmployeeCode);
                if (employee == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Employee not found" };
                    return result;
                }

                
                var salaryStructure = await context.SalaryStructures.FirstOrDefaultAsync(s => s.EmployeeCode == model.EmployeeCode);

                if (salaryStructure == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Salary structure not found for this employee" };
                    return result;
                }

                
                var existingPayroll = await context.Payrolls.FirstOrDefaultAsync(p => p.EmployeeCode == model.EmployeeCode && p.Month == model.Month && p.Year == model.Year);


                if (existingPayroll != null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Payroll already generated for this month" };
                    return result;
                }

                
                decimal basicPay = salaryStructure.BasicPay;
                decimal allowances = salaryStructure.Allowances;
                decimal deductions = salaryStructure.Deductions;
                decimal netPay = basicPay + allowances - deductions;

                var payroll = new Payroll
                {
                    EmployeeCode = model.EmployeeCode,
                    EmployeeName=model.EmployeeName,
                    Month = model.Month,
                    Year = model.Year,
                    BasicPay = basicPay,
                    Allowances = allowances,
                    Deductions = deductions,
                    NetPay = netPay,
                    GeneratedDate = DateTime.Now
                };

                await context.Payrolls.AddAsync(payroll);
                await context.SaveChangesAsync();

                result = new ApiResultVm { Succeeded = true, Message = "Payroll generated successfully" };
                return result;
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }
        public async Task<ApiResultVm> UpdatePayroll(PayrollVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var payroll = await context.Payrolls.FirstOrDefaultAsync(p => p.EmployeeCode == model.EmployeeCode);
                if (payroll == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Payroll not found" };
                    return result;
                }
                payroll.BasicPay = model.BasicPay;
                payroll.Allowances = model.Allowances;
                payroll.Deductions = model.Deductions;
                payroll.NetPay = model.NetPay;
                context.Payrolls.Update(payroll);
                await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Payroll updated successfully" };
                return result;
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }

        public async Task<ApiResultVm> DeletePayroll(int EmployeeCode)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var payroll = await context.Payrolls.FirstOrDefaultAsync(p => p.EmployeeCode == EmployeeCode);
                if (payroll == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Payroll not found" };
                    return result;
                }
                context.Payrolls.Remove(payroll);
                await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Payroll deleted successfully" };
                return result;
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }

        }

        public async Task<RPayrollVm> GetPayrollByEmployeeCode(int EmployeeCode)
        {
            RPayrollVm result = new RPayrollVm();
            try
            {
                var payroll = await context.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.EmployeeCode == EmployeeCode);
                if (payroll == null)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Payroll not found" };
                    return result;
                }
                var payrollVm = new PayrollVm
                {
                    Id = payroll.Id,
                    EmployeeCode = payroll.EmployeeCode,
                    EmployeeName = payroll.EmployeeName,
                    Month = payroll.Month,
                    Year = payroll.Year,
                    BasicPay = payroll.BasicPay,
                    Allowances = payroll.Allowances,
                    Deductions = payroll.Deductions,
                    NetPay = payroll.NetPay,
                    GeneratedDate = payroll.GeneratedDate
                };
                result.Result = new ApiResultVm { Succeeded = true, Message = "Payroll fetched successfully" };
                result.Data = payrollVm;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }


        }
        public async Task<RPayrollListVm> GetAllPayrolls(int PageNo, int PageSize)
        {
            RPayrollListVm result = new RPayrollListVm();
            try
            {
                var payrolls = await context.Payrolls.Include(p => p.Employee).Skip((PageNo - 1) * PageSize).Take(PageSize).ToListAsync();
                var payrollVmList = new List<PayrollVm>();
                foreach (var payroll in payrolls)
                {
                    payrollVmList.Add(new PayrollVm
                    {
                        Id = payroll.Id,
                        EmployeeCode = payroll.EmployeeCode,
                        EmployeeName = payroll.EmployeeName,
                        Month = payroll.Month,
                        Year = payroll.Year,
                        BasicPay = payroll.BasicPay,
                        Allowances = payroll.Allowances,
                        Deductions = payroll.Deductions,
                        NetPay = payroll.NetPay,
                        GeneratedDate = payroll.GeneratedDate
                    });
                }
                result.Result = new ApiResultVm { Succeeded = true, Message = "Payrolls fetched successfully" };
                result.Data = payrollVmList;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }
        public async Task<RPayrollVm> GetPayrollByEmployeeName(string employeeName)
        {
            RPayrollVm result = new RPayrollVm();
            try
            {
                var payroll = await context.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.EmployeeName == employeeName);
                if (payroll == null)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Payroll not found" };
                    return result;
                }
                var payrollVm = new PayrollVm
                {
                    Id = payroll.Id,
                    EmployeeCode = payroll.EmployeeCode,
                    EmployeeName = payroll.EmployeeName,
                    Month = payroll.Month,
                    Year = payroll.Year,
                    BasicPay = payroll.BasicPay,
                    Allowances = payroll.Allowances,
                    Deductions = payroll.Deductions,
                    NetPay = payroll.NetPay,
                    GeneratedDate = payroll.GeneratedDate
                };
                result.Result = new ApiResultVm { Succeeded = true, Message = "Payroll fetched successfully" };
                result.Data = payrollVm;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }
        public async Task<RPayrollVm> GetPayrollByMonth(int month, int year)
        {
            RPayrollVm result = new RPayrollVm();
            try
            {
                var payroll = await context.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.Month == month && p.Year == year);
                if (payroll == null)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Payroll not found" };
                    return result;
                }
                var payrollVm = new PayrollVm
                {
                    Id = payroll.Id,
                    EmployeeCode = payroll.EmployeeCode,
                    EmployeeName = payroll.EmployeeName,
                    Month = payroll.Month,
                    Year = payroll.Year,
                    BasicPay = payroll.BasicPay,
                    Allowances = payroll.Allowances,
                    Deductions = payroll.Deductions,
                    NetPay = payroll.NetPay,
                    GeneratedDate = payroll.GeneratedDate
                };
                result.Result = new ApiResultVm { Succeeded = true, Message = "Payroll fetched successfully" };
                result.Data = payrollVm;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }
        public async Task<RPayrollVm> GetPayrollByDepartment(int departmentCode)
        {
            RPayrollVm result = new RPayrollVm();
            try
            {
                var payroll = await context.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.Employee.DepartmentCode == departmentCode);
                if (payroll == null)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Payroll not found" };
                    return result;
                }
                var payrollVm = new PayrollVm
                {
                    Id = payroll.Id,
                    EmployeeCode = payroll.EmployeeCode,
                    EmployeeName = payroll.EmployeeName,
                    Month = payroll.Month,
                    Year = payroll.Year,
                    BasicPay = payroll.BasicPay,
                    Allowances = payroll.Allowances,
                    Deductions = payroll.Deductions,
                    NetPay = payroll.NetPay,
                    GeneratedDate = payroll.GeneratedDate
                };
                result.Result = new ApiResultVm { Succeeded = true, Message = "Payroll fetched successfully" };
                result.Data = payrollVm;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }
        public async Task<RPayrollVm> GetPayrollByDesignation(int DesignationCode)
        {
            RPayrollVm result = new RPayrollVm();
            try
            {
                var payroll = await context.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.Employee!.DesignationCode == DesignationCode);
                if (payroll == null)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Payroll not found" };
                    return result;
                }
                var payrollVm = new PayrollVm
                {
                    Id = payroll.Id,
                    EmployeeCode = payroll.EmployeeCode,
                    EmployeeName = payroll.EmployeeName,
                    Month = payroll.Month,
                    Year = payroll.Year,
                    BasicPay = payroll.BasicPay,
                    Allowances = payroll.Allowances,
                    Deductions = payroll.Deductions,
                    NetPay = payroll.NetPay,
                    GeneratedDate = payroll.GeneratedDate
                };
                result.Result = new ApiResultVm { Succeeded = true, Message = "Payroll fetched successfully" };
                result.Data = payrollVm;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }
        public async Task<bool> IsPayrollGenerated(int employeeId, int month, int year)
        {
            try
            {
                var payroll = await context.Payrolls.FirstOrDefaultAsync(p => p.EmployeeCode == employeeId && p.Month == month && p.Year == year);
                return payroll != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}


   

