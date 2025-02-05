using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.Store.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medictionary.ViewComponents
{
    public class HomeIndustryViewComponent(IStore<Industry> _industryStore) : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
         
            var industry = _industryStore.FindOne(x=>!x.IsDeleted);

            return View("/Views/Shared/Components/HomeIndustry.cshtml", industry);
        }
    }
}