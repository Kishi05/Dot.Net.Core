using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool isDummy { get; set; }

        public override string ToString()
        {
            return $"ID : {Id} | Name : {Name} | Email : {Email}";
        }
    }
}
