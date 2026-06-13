using CafePOS_API.Models.DTOs.QueryParams;
using CafePOS_API.Models.DTOs.Requests;
using CafePOS_API.Models.DTOs.Response;
using CafePOS_API.Models.Entities;
using CafePOS_API.Repositories.Interfaces;
using CafePOS_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace CafePOS_API.Services.Implementations
{
    public class EmployeeServices(IEmployeeRepo _repo) : IEmployeeService
    {
        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            var user = await _repo.GeyByIdAsync(id);
            if (user == null) return false;

            var result = await _repo.DeleteAsync(user);
            if (!result.Succeeded) return false;
            return true;
        }
        //check

        public async Task<IEnumerable<EmployeeResponse>> GetAllAsync(UserQueryParam query) //fix
        {
            var users = _repo.GetQueryable();

            //if (!string.IsNullOrEmpty(query.Name))
            //    users = users.Where(u => u.FullName.Contains(query.Name));
            //if (!string.IsNullOrEmpty(query.Role))
            //    users = users.Where()

            return null;
        }

        public async Task<EmployeeResponse> GetByIdAsync(string id)
        {
            var user = await _repo.GeyByIdAsync(id);
            var roles = await _repo.GetRolesAsync(user);
            return MapToResponse(user, roles);
        }

        public async Task<bool> UpdateEmployeeAsync(string id, EmployeeRequest request)
        {
            var user = await _repo.GeyByIdAsync(id);
            if (user == null) return false;
            user.FullName = request.Name;
            user.Email = request.Email;
            var result = await _repo.UpdateAsync(user);
            if (!result.Succeeded) return false;
            return true;
        }
        private EmployeeResponse MapToResponse(Employee employee, IList<string> Roles)
        {
            return new EmployeeResponse(employee.Id, employee.FullName, employee.Email!, Roles);;
        }
    }
}
