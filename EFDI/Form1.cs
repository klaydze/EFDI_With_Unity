using Accenture.Online.Service.Implementation;
using EFDI.Domain.Models;
using EFDI.Services.Contracts;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Forms;

namespace EFDI
{
    public partial class Form1 : Form
    {
        //IUnityContainer container;

        [Dependency]
        public IUnityContainer UContainer { get; set; }
        [Dependency]
        public IRecipeService RecipeService { get; set;}

        public Form1()
        {
            InitializeComponent();
            //Approach #1
            //container = new UnityContainer();
            //container.RegisterType<IRecipeService, RecipeService>();

            //Approach #2
            //container = new UnityContainer();
            //var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            //container.LoadConfiguration();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string enc = CryptographicService.CustomEncrypt("patrick.a.de.ocampo@accenture.com", false);
            string dec = CryptographicService.CustomDecrypt("bkCOOLpHZKji4n%2fjxSBhXaI1jWpFacG%2bW2zjnr3AezF%2f95rpBRgli4vS3sYYYKz2WKSTCWHZy9byViZg7sL4WJiMNGY61zQbUXSMhFxqvOzkQmvRRmnU3hTZTFu2uqwH");

            RecipeDomain recipe = new RecipeDomain()
            {
                RecipeName = "Dumplings",
                CategoryId = 3
            };
            //RecipeService.Insert(recipe);
            //RecipeService.Save();

            //var d = RecipeService.GetEntityById(1);
            //d.RecipeName = "Rice Toppings";
            //RecipeService.Update(d);
            //RecipeService.Save();













            //Approach #1
            //IRecipeService service = container.Resolve<IRecipeService>();
            //service.AddRecipe(new Recipe { Name = "Spaghetti", CategoryId = 2 });

            //Approach #2
            //RecipeService.AddRecipe(new Recipe { Name = "Spaghetti", CategoryId = 2 });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = UContainer.Resolve<Form2>();
            frm2.Show();
        }
    }
}
