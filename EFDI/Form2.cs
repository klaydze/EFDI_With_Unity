using EFDI.Data.Models;
using EFDI.Services.Contracts;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Forms;

namespace EFDI
{
    public partial class Form2 : Form
    {
        [Dependency]
        public ICategoryService CategoryService { get; set; }

        [Dependency]
        public IGenericService<Category> Generic1 { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var t = CategoryService.AddCategory(null);
        }
    }
}
