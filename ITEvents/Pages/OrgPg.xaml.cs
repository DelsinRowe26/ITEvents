using ITEvents.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITEvents.Pages
{
	/// <summary>
	/// Логика взаимодействия для OrgPg.xaml
	/// </summary>
	public partial class OrgPg : Page
	{

		Organizer organizer1 = new Organizer();

		public OrgPg(Organizer organizer)
		{
			InitializeComponent();

			BitmapImage image = new BitmapImage();
			image.BeginInit();
			image.UriSource = new Uri("pack://application:,,,/Resources/Organizer/" + organizer.Photo + ".jpg");
			image.EndInit();

			DateTime now = DateTime.Now;
			DateTime morning1 = DateTime.Parse("9:00");
			DateTime morning2 = DateTime.Parse("11:00");
			DateTime day1 = DateTime.Parse("11:01");
			DateTime day2 = DateTime.Parse("18:00");
			DateTime evning1 = DateTime.Parse("18:01");
			DateTime evning2 = DateTime.Parse("23:59");

			if (now >= morning1 && now <= morning2)
			{
				tblGood.Text = "Доброе";
				tblTimeOfDay.Text = "утро";
			}
			else if (now >= day1 && now <= day2)
			{
				tblGood.Text = "Добрый";
				tblTimeOfDay.Text = "день";
			}
			else if (now >= evning1 && now <= evning2)
			{
				tblGood.Text = "Добрый";
				tblTimeOfDay.Text = "вечер";
			}

			if (organizer.Gender == "мужской")
			{
				tblGender.Text = "Mr";
			}
			else
			{
				tblGender.Text = "Mrs";
			}

			tblSecondname.Text = organizer.Secondname;

			imgOrg.Source = image;

			organizer1 = organizer;
		}

		private void btnMyProfile_Click(object sender, RoutedEventArgs e)
		{
			//Organizer organizer = new Organizer();
			var myProfile = new MyProfilePg();
			myProfile.DataContext = organizer1;
			NavigationService.Navigate(myProfile);
		}

		private void btnMembersList_Click(object sender, RoutedEventArgs e)
		{

			List<Members> members1 = EventEntitiesIT.GetContext().Members.ToList();

			var pgList = new pgListJury();

			pgList.lvListJuryMemb.ItemsSource = members1;

			NavigationService.Navigate(pgList);
        }

		private void btnJuryList_Click(object sender, RoutedEventArgs e)
		{
			List<Jury> juries = EventEntitiesIT.GetContext().Jury.ToList();

			var pgList = new pgListJury();

			pgList.lvListJuryMemb.ItemsSource = juries;

			NavigationService.Navigate(pgList);
		}

		private void btnEvents_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Events(organizer1));
		}
	}
}
