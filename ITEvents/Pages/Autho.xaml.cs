using ITEvents.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ITEvents.Pages
{
	/// <summary>
	/// Логика взаимодействия для Autho.xaml
	/// </summary>
	public partial class Autho : Page
	{

		private int _countUnsuccessful = 0;
		private Members members = null;
		private Moderators moderator = null;
		private Organizer organizer = null;
		private Jury jury = null;

		public Autho()
		{
			InitializeComponent();
		}

		private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Events(null));
        }

		private void btnEnter_Click(object sender, RoutedEventArgs e)
		{
			string login = tbLogin.Text;
			string password = pbPassword.Password;
			var dbConn = EventEntitiesIT.GetContext();

			if(login.Length > 0 && password.Length > 0 )
			{
				members = dbConn.Members.Where(x => x.Email == login && x.Password == password).FirstOrDefault();
				moderator = dbConn.Moderators.Where(x => x.Email == login && x.Password == password).FirstOrDefault();
				organizer = dbConn.Organizer.Where(x => x.Email == login && x.Password == password).FirstOrDefault();
				jury = dbConn.Jury.Where(x => x.Email == login && x.Password == password).FirstOrDefault();
				if(_countUnsuccessful < 3)
				{
					if(members != null)
					{
						clearForm();
						MembersWin membersWin = new MembersWin();
						membersWin.ShowDialog();
					}
					else if(moderator != null)
					{
						clearForm();
						ModeratorWin moderatorWin = new ModeratorWin();
						moderatorWin.ShowDialog();
					}
					else if(organizer != null)
					{
						clearForm();
						var orgPg = new OrgPg(organizer);
						//orgPg.DataContext = organizer;
						OrganizerWin organizerWin = new OrganizerWin(organizer);
						organizerWin.ShowDialog();
					}
					else if(jury != null)
					{
						clearForm();
						JuryWin juryWin = new JuryWin();
						juryWin.ShowDialog();
					}
					else
					{
						_countUnsuccessful++;
						MessageBox.Show("Неверно введены логин или пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
						if(_countUnsuccessful == 1)
						{
							tblCaptcha.Visibility = Visibility.Visible;
							tbCaptcha.Visibility = Visibility.Visible;
							tblCaptcha.Text = Model.Captcha.GeneratedCaptcha();
							tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
						}
					}
				}
				else
				{
					if (members != null && members.Email == login && members.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						clearForm();
						MembersWin membersWin = new MembersWin();
						membersWin.ShowDialog();
					}
					else if (moderator != null && moderator.Email == login && moderator.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						clearForm();
						ModeratorWin moderatorWin = new ModeratorWin();
						moderatorWin.ShowDialog();
					}
					else if (organizer != null && organizer.Email == login && organizer.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						clearForm();
						OrganizerWin organizerWin = new OrganizerWin(organizer);
						organizerWin.ShowDialog();
					}
					else if (jury != null && jury.Email == login && jury.Password == password && tbCaptcha.Text == tblCaptcha.Text)
					{
						clearForm();
						JuryWin juryWin = new JuryWin();
						juryWin.ShowDialog();
					}
					else
					{
						MessageBox.Show("Введите данные заново через 10 секунд!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
						tblCaptcha.Text = "";
						tbCaptcha.Clear();
						tblCaptcha.Visibility = Visibility.Hidden;
						tbCaptcha.Visibility = Visibility.Hidden;
						tbLogin.Clear();
						tbLogin.IsEnabled = false;
						pbPassword.Clear();
						pbPassword.IsEnabled = false;
						btnEnter.IsEnabled = false;
						tblTimer.Visibility = Visibility.Visible;
						CountDown(10, TimeSpan.FromSeconds(1), cur => tblTimer.Text =  cur.ToString() + " сек.");
					}
				}
			}
			else
			{
				MessageBox.Show("Не заполнены поля Логин и Пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void enableForm()
		{
			tbLogin.IsEnabled = true;
			pbPassword.IsEnabled = true;
			btnEnter.IsEnabled = true;
			tblTimer.Visibility = Visibility.Hidden;
		}

		private void CountDown(int count,TimeSpan interval, Action<int> ts)
		{
			var dt = new DispatcherTimer();
			dt.Interval = interval;
			dt.Tick += (_, a) =>
			{
				if (count-- == 0)
				{
					enableForm();
					dt.Stop();
				}
				else
					ts(count);
			};
			ts(count);
			dt.Start();
		}

		private void clearForm()
		{
			pbPassword.Clear();
			tbLogin.Clear();
			tbCaptcha.Clear();
			tblCaptcha.Visibility = Visibility.Hidden;
			tbCaptcha.Visibility = Visibility.Hidden;
			tblTimer.Visibility = Visibility.Hidden;
		}

		private void pbPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				btnEnter_Click(sender, e);
			}
		}
	}
}
