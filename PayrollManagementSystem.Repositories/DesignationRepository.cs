using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Payrollmanagementsystem.Models;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Text;
using X.PagedList.Extensions;

namespace PayrollManagementSystem.Repositories
{
    public class DesignationRepository : IDesignation
    {
        private readonly ApplicationDbContext context;
        public DesignationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ApiResultVm> AddDesignation(DesignationVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Designation designation = new Designation
                {
                    Id = model.Id,
                    DesignationName = model.DesignationName,
                    DesignationCode = model.DesignationCode,
                    Description = model.Description,


                };
                context.Designations.Add(designation);
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
        public async Task<ApiResultVm> UpdateDesignation(DesignationVm model)
        {
            ApiResultVm result = new ApiResultVm();

            try
            {
                var res = await context.Designations.FindAsync(model.Id);
                if (res == null) return new ApiResultVm { Succeeded = false, Message = "Invalid Item" };

                res.DesignationName = model.DesignationName;
                res.DesignationCode = model.DesignationCode;
                res.Description = model.Description;




                context.Designations.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Update Successfully! Updated Count:" + res2 };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }

            return result;
        }
        public async Task<ApiResultVm> DeleteDesignation(int DesignationCode)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Designations.FindAsync(DesignationCode);
                if (res == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Designation not found" };
                    return result;
                }

                context.Designations.Remove(res);
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
                result = new ApiResultVm
                {
                    Succeeded = false,
                    Message = ex.Message
                };
                return result;
            }

        }

        public async Task<RDesignationVm> GetDesignationByDesignationCode(int DesignationCode)
        {
            RDesignationVm result = new RDesignationVm();

            try
            {
                var res = await context.Designations.FindAsync(DesignationCode);
                if (res != null)
                {
                    DesignationVm designation = new DesignationVm()
                    {
                        Id = res.Id,
                        DesignationCode = res.DesignationCode,
                        DesignationName = res.DesignationName,
                        Description = res.Description,

                    };
                    result.Data = designation;
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
        public async Task<RDesignationListVm> GetAllDesignations(int PageNo, int PageSize)
        {
            RDesignationListVm result = new RDesignationListVm();

            try
            {
                var res = context.Designations.ToPagedList(PageNo, PageSize);
                if (res != null)
                {
                    List<DesignationVm> Vms = new List<DesignationVm>();
                    foreach (var item in res)
                    {
                        {
                            DesignationVm vm = new DesignationVm
                            {
                                Id = item.Id,
                                DesignationCode = item.DesignationCode,
                                DesignationName = item.DesignationName,
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
        public async Task<RDesignationVm> SearchDesignation(string name)
        {
            RDesignationVm result = new RDesignationVm();

            try
            {
                var res = await context.Designations.FindAsync(name);
                if (res != null)
                {
                    DesignationVm designation = new DesignationVm()
                    { DesignationName = res.DesignationName, };
                    result.Data = designation;
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
        public async Task<ApiResultVm> ActivateDesignation(int DesignationCode)
        {
            {
                ApiResultVm result = new ApiResultVm();
                try
                {
                    var res = await context.Designations.FirstOrDefaultAsync(x => x.DesignationCode == DesignationCode);
                    if (res == null)
                    {
                        result = new ApiResultVm { Message = "Designation not found", Succeeded = false };
                        return result;
                    }
                    res.IsActive = true;

                    var res2 = await context.SaveChangesAsync();

                    if (res2 > 0)
                    {
                        result = new ApiResultVm { Message = "Designation activated successfully", Succeeded = true };
                    }
                    else
                    {
                        result = new ApiResultVm { Message = "Unable to activate designation", Succeeded = false };
                    }

                }
                catch (Exception ex)
                {
                    result = new ApiResultVm { Message = ex.Message, Succeeded = false };
                }
                return result;
            }
        }
        public async Task<ApiResultVm> DeactivateDesignation(int DesignationCode)
        {
            ApiResultVm result = new ApiResultVm();

            try
            {
                var department = await context.Designations
                    .FirstOrDefaultAsync(x => x.DesignationCode == DesignationCode);

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
