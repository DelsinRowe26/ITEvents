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
	/// Логика взаимодействия для MyProfilePg.xaml
	/// </summary>
	public partial class MyProfilePg : Page
	{

		Members members = null;
		Moderators moderators = null;
		Jury jury = null;
		Organizer organizer = null;

		public MyProfilePg()
		{
			InitializeComponent();
		}

		private void chbVisibleThePassword_Checked(object sender, RoutedEventArgs e)
		{
			tblVisiblePass.Text = pbChangingThePass.Password;
			tblVisibleRep.Text = pbChangingThePassRepeat.Password;
        }

		private void chbVisibleThePassword_Unchecked(object sender, RoutedEventArgs e)
		{
			tblVisiblePass.Text = " ";
			tblVisibleRep.Text = " ";
		}

		private void chbChangingThePassword_Checked(object sender, RoutedEventArgs e)
		{
			pbChangingThePass.IsEnabled = true;
			pbChangingThePassRepeat.IsEnabled = true;
			btnOk.IsEnabled = true;
			chbVisibleThePassword.IsEnabled = true;
		}

		private void chbChangingThePassword_Unchecked(object sender, RoutedEventArgs e)
		{
			pbChangingThePass.IsEnabled = false;
			pbChangingThePassRepeat.IsEnabled = false;
			btnOk.IsEnabled = false;
			chbVisibleThePassword.IsEnabled = false;
		}

		private void pbChangingThePass_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if(chbVisibleThePassword.IsChecked == true)
			{
				tblVisiblePass.Text = pbChangingThePass.Password;
			}
		}

		private void pbChangingThePassRepeat_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (chbVisibleThePassword.IsChecked == true)
			{
				tblVisibleRep.Text = pbChangingThePassRepeat.Password;
			}
		}

		private void UpdateTable()
		{
			var email = tblEmail.Text;
			var dbConn = EventEntitiesIT.GetContext();

			organizer = dbConn.Organizer.Where(p => p.Email == email).FirstOrDefault();
			jury = dbConn.Jury.Where(p => p.Email == email).FirstOrDefault();
			moderators = dbConn.Moderators.Where(p => p.Email == email).FirstOrDefault();
			members = dbConn.Members.Where(p => p.Email == email).FirstOrDefault();

			if(pbChangingThePass.Password == pbChangingThePassRepeat.Password)
			{
				if(organizer != null)
				{
					if (pbChangingThePassRepeat.Password != organizer.Password)
					{
						organizer.Password = pbChangingThePassRepeat.Password;
						dbConn.SaveChanges();
						MessageBox.Show("Пароль был успешно изменен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show("Новый пароль совпадает со старым!\nИзмените пароль!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
				else if(moderators != null)
				{
					if(pbChangingThePassRepeat.Password != moderators.Password)
					{
						moderators.Password = pbChangingThePassRepeat.Password;
						dbConn.SaveChanges();
						MessageBox.Show("Пароль был успешно изменен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show("Новый пароль совпадает со старым!\nИзмените пароль!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
				else if (members != null)
				{
					if(pbChangingThePassRepeat.Password != members.Password)
					{
						members.Password = pbChangingThePassRepeat.Password;
						dbConn.SaveChanges();
						MessageBox.Show("Пароль был успешно изменен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show("Новый пароль совпадает со старым!\nИзмените пароль!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
				else if (jury != null)
				{
					if(pbChangingThePassRepeat.Password != jury.Password)
					{
						jury.Password = pbChangingThePassRepeat.Password;
						dbConn.SaveChanges();
						MessageBox.Show("Пароль был успешно изменен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show("Новый пароль совпадает со старым!\nИзмените пароль!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}

			}
		}

		private void ClearForm()
		{
			pbChangingThePass.Password = "";
			pbChangingThePassRepeat.Password = "";
			chbChangingThePassword.IsChecked = false;
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			UpdateTable();
			ClearForm();
			chbChangingThePassword_Unchecked(sender, e);
		}

		private void btnCancle_Click(object sender, RoutedEventArgs e)
		{
			ClearForm();
			chbChangingThePassword_Unchecked(sender, e);
		}
	}
}
