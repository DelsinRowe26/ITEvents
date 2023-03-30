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
	/// Логика взаимодействия для pgCreatEvent.xaml
	/// </summary>
	public partial class pgCreatEvent : Page
	{

		Organizer organizer1;

		public pgCreatEvent(Organizer organizer)
		{
			organizer1 = organizer;
			InitializeComponent();
			DataContext = this;

			var dbConn = EventEntitiesIT.GetContext();
			var jury = dbConn.Jury.ToList();
			cmbJury1.ItemsSource = jury;
			cmbJury2.ItemsSource = jury;
			cmbJury3.ItemsSource = jury;
		}

		public string[] ListDirection { get; set; } =
		{
			"ИТ",
			"Биг Дата",
			"Аналитика",
			"Информационная безопасность",
			"Дизайн"
		};

		public string[] ListTime { get; set; } =
		{
			"9.00",
			"10.30",
			"12.00",
			"13.00",
			"14.30",
			"16.00"
		};

		private void AddTable()
		{
			if (tbEventName.Text != "" && tbCity.Text != "" && cmbDirection.Text != "" && cmbJury1.Text != "" && cmbJury2.Text != "" && cmbJury3.Text != "" && cmbTime1.Text != "" && cmbTime2.Text != "" && cmbTime3.Text != "")
			{
				var start = dpStart.Text;
				var end = dpEnd.Text;
				var eventName = tbEventName.Text;
				var direction = cmbDirection.Text;
				var city = tbCity.Text;

				var act1 = tbAct1.Text;
				var act2 = tbAct2.Text;
				var act3 = tbAct3.Text;
				var time1 = cmbTime1.Text;
				var time2 = cmbTime2.Text;
				var time3 = cmbTime3.Text;
				var jury1 = cmbJury1.Text;
				var jury2 = cmbJury2.Text;
				var jury3 = cmbJury3.Text;

				var firstname = organizer1.Fisrtname;
				var secondname = organizer1.Secondname;
				var patronymic = organizer1.Patronymic;
				var FSP = firstname + " " + secondname + "" + patronymic;
				var idOrganizer = organizer1.ID_Organizer;

				DateTime? dateSt = dpStart.SelectedDate;
				DateTime? dateEn = dpEnd.SelectedDate;

				int daySt = dateSt.Value.Day;
				int dayEn = dateEn.Value.Day;
				int countDay = dayEn - daySt;

				var dbConn = EventEntitiesIT.GetContext();

				Direction direction1 = new Direction();
				City city1 = new City();

				city1 = dbConn.City.Where(p => p.CITY1.ToLower() == city.ToLower()).FirstOrDefault();
				direction1 = dbConn.Direction.Where(p => p.DirectionName.ToLower() == direction.ToLower()).FirstOrDefault();

				int lastID_Event = dbConn.Events.OrderByDescending(x => x.ID_Events).FirstOrDefault()?.ID_Events ?? 0;
				int lastID_Activity = dbConn.Activity.OrderByDescending(x => x.ID_Activity).FirstOrDefault()?.ID_Activity ?? 0;

				int day1 = 0;
				int day2 = 0;
				int day3 = 0;

				if (countDay == 0)
				{
					day1 = 1;
					day2 = 1;
					day3 = 1;
				}
				else if(countDay == 1)
				{
					day1 = 1;
					day2 = 2;
					day3 = 2;
				}
				else if(countDay == 2)
				{
					day1 = 1;
					day2 = 2;
					day3 = 3;
				}

				var newAct = new Activity
				{
					ID_Activity = lastID_Activity++,
					EventName = eventName,
					Activity_1 = act1,
					Activity_2 = act2,
					Activity_3 = act3,
					DAY_1 = day1,
					DAY_2 = day2,
					DAY_3 = day3,
					TimeStart_1 = TimeSpan.Parse(time1),
					TimeStart_2 = TimeSpan.Parse(time2),
					TimeStart_3 = TimeSpan.Parse(time3),
					Moderator_1 = FSP,
					Moderator_2 = null,
					Moderator_3 = null,
					Jury_1_1 = jury1,
					Jury_1_2 = null,
					Jury_1_3 = null,
					Jury_2_1 = jury2,
					Jury_2_2 = null,
					Jury_2_3 = null,
					Jury_3_1 = jury3,
					Jury_3_2 = null,
					Jury_3_3 = null,
					Jury_4_1 = jury1,
					Jury_4_2 = jury2,
					Jury_4_3 = null,
					Jury_5_1 = jury3,
					Jury_5_2 = jury1,
					Jury_5_3 = jury2,
					Winner = null
				};

				var newEvent = new Entities.Events
				{
					ID_Events = lastID_Event++,
					Events1 = eventName,
					DATE = start,
					DAYS = countDay++,
					ID_City = city1.ID_City,
					Direction = direction,
					ID_Direction = direction1.ID_Direction,
					ID_Organizer = organizer1.ID_Organizer
				};

				dbConn.Events.Add(newEvent);
				dbConn.Activity.Add(newAct);
				dbConn.SaveChanges();

			}
			
			
		}

		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			//AddTable();
			var direction = cmbDirection.Text;
			var time1 = cmbTime1.Text;
			var time2 = cmbTime2.Text;
			var time3 = cmbTime3.Text;
			var jury1 = cmbJury1.Text;
			var jury2 = cmbJury2.Text;
			var jury3 = cmbJury3.Text;
		}

		private void cmbTime1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(cmbTime1.SelectedItem != null )
			{
				cmbTime2.Items.Remove(cmbTime1.SelectedItem);
				cmbTime3.Items.Remove(cmbTime1.SelectedItem);
			}
		}

		private void cmbTime2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void cmbTime3_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
