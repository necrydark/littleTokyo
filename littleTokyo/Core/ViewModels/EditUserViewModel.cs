using littleTokyo.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace littleTokyo.Core.ViewModels
{
    public class EditUserViewModel
    {
        public ApplicationUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}
