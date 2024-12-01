using Microsoft.Maui.Controls;
using Campuscloset.Models;

namespace Campuscloset.Pages
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Item item)
        {
            InitializeComponent();

            // Set the BindingContext to the passed item
            BindingContext = item;
        }
    }
}
