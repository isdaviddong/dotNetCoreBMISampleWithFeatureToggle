using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace WebBMI.Pages
{
    public class IndexModel : PageModel
    {
        public float BmiResult = 0;
        public bool isEnabledFeatureGender = false;

        [BindProperty]
        public int fieldHeight { get; set; }
        [BindProperty]
        public int fieldWeight { get; set; }

        public readonly IConfiguration Configuration;
        public IndexModel(IConfiguration configuration, IFeatureManager featureManager)
        {
            Configuration = configuration;

            if (featureManager != null)
                isEnabledFeatureGender = featureManager.IsEnabledAsync("featureGender").Result;
        }

        public void OnGet()
        {

        }

        public void OnPostCalculate()
        {
            HealthMgr.BmiCalculator bc = new HealthMgr.BmiCalculator();

            bc.Height = fieldHeight;
            bc.Weight = fieldWeight;

            BmiResult = bc.Calculate() ;
        }
    }
}