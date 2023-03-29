using ITEvents.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITEvents.Pages
{
	/// <summary>
	/// Логика взаимодействия для Events.xaml
	/// </summary>
	public partial class Events : Page
	{

		Organizer organizer1;

		public Events(Organizer organizer)
		{
			InitializeComponent();

			var events = EventEntitiesIT.GetContext().Events.ToList();
			lvEvents.ItemsSource = events;
			DataContext = this;

			tblAllAmount.Text = events.Count().ToString();
			//cmbFilterEvent.ItemsSource = events;

			UpdateData();
			if(organizer != null)
			{
				organizer1 = organizer;
				btnCreatEvent.Visibility = Visibility.Visible;
			}
		}

		public string[] FilterList { get; set; } =
		{
			"Без фильтра",
			"ИТ",
			"Биг Дата",
			"Аналитика",
			"Информационная безопасность",
			"Дизайн"
		};

		private void UpdateData()
		{
			var result = EventEntitiesIT.GetContext().Events.ToList();

			if(cmbFilterEvent.SelectedIndex == 1)
			{
				result = result.Where(p => p.Direction == "ИТ").ToList();
			}
			if (cmbFilterEvent.SelectedIndex == 2)
			{
				result = result.Where(p => p.Direction == "Биг Дата").ToList();
			}
			if (cmbFilterEvent.SelectedIndex == 3)
			{
				result = result.Where(p => p.Direction == "Аналитика").ToList();
			}
			if (cmbFilterEvent.SelectedIndex == 4)
			{
				result = result.Where(p => p.Direction == "Информационная безопасность").ToList();
			}
			if (cmbFilterEvent.SelectedIndex == 5)
			{
				result = result.Where(p => p.Direction == "Дизайн").ToList();
			}

			if (dpFilterDate.Text != "")
			{
				result = result.Where(p => p.DATE == dpFilterDate.Text).ToList();
			}
			lvEvents.ItemsSource = result;
			tblResultAmount.Text = result.Count().ToString();
		}

		private void dpFilterDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateData();
		}

		private void cmbFilterEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateData();
		}

		private void lvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedItem = lvEvents.SelectedItem;

			var moreEvent = new MoreEvent();

			moreEvent.DataContext = selectedItem;

			NavigationService.Navigate(moreEvent);
		}

		private void btnCreatEvent_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new pgCreatEvent(organizer1));
        }
    }
}
