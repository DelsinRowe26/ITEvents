using ITEvents.Entities;
using ITEvents.Pages;
using ITEvents.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ITEvents.Windows
{
	/// <summary>
	/// Логика взаимодействия для OrganizerWin.xaml
	/// </summary>
	public partial class OrganizerWin : Window
	{
		public OrganizerWin(Organizer organizer)
		{
			InitializeComponent();

			FrmMainOrg.Navigate(new OrgPg(organizer));
			MainWindow window = new MainWindow();
			window.Close();
		}

		private void FrmMainOrg_ContentRendered(object sender, EventArgs e)
		{
			if (FrmMainOrg.CanGoBack)
			{
				btnBack.Visibility = Visibility.Visible;
			}
			else
			{
				btnBack.Visibility= Visibility.Hidden;
			}
        }

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			FrmMainOrg.GoBack();
		}
	}
}
