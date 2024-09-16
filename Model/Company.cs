using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEF.Model
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime? LastSalaryUpdate { get; set; }

        public List<Employee> Employees { get; set; }
    }
}