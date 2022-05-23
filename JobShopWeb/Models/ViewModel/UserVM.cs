using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace JobShopWeb.Models.ViewModel
{
    public class UserVM
    {

        public IEnumerable<User> UserList { get; set; }
    }
}
