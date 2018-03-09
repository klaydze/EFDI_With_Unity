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

        RecipeDomain currentRecipeDomain = new RecipeDomain();

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            RecipeDomain recipe = new RecipeDomain()
            {
                RecipeName = textBox1.Text,
                CategoryId = Convert.ToInt32(numericUpDown1.Value)
            };

            RecipeService.Insert(recipe);
            RecipeService.Save();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter the Recipe Id in the textbox and click Find button", "Find Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);

            currentRecipeDomain = RecipeService.GetEntityById(int.Parse(txtRecipeId.Text));
            txtRecipeId.Text = currentRecipeDomain?.RecipeId.ToString();
            txtRecipeName.Text = currentRecipeDomain?.RecipeName;
            txtRecipeCategoryId.Text = currentRecipeDomain?.CategoryId.ToString();
        }

        private void ClearFields()
        {
            txtRecipeId.Clear();
            txtRecipeName.Clear();
            txtRecipeCategoryId.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter the Recipe Id in the textbox and click Find button before clicking Update button", "Update Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (currentRecipeDomain != null)
            {
                currentRecipeDomain.RecipeName = txtRecipeName.Text;
                currentRecipeDomain.CategoryId = int.Parse(txtRecipeCategoryId.Text);

                RecipeService.Update(currentRecipeDomain);
                RecipeService.Save();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentRecipeDomain != null &&
                MessageBox.Show("Are you sure you want to delete this Recipe?", "Delete Recipe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RecipeService.Delete(currentRecipeDomain.RecipeId);
                RecipeService.Save();
            }
        }
    }
}
