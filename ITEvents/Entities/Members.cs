//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITEvents.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Members
    {
        public int ID_Members { get; set; }
        public string Fisrtname { get; set; }
        public string Secondname { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Date_of_birthday { get; set; }
        public Nullable<int> ID_City { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
    
        public virtual City City { get; set; }

		public string ImgPathJpg
		{
			get
			{
				var path = "pack://application:,,,/Resources/Members/" + this.Photo;
				return path;
			}
		}
	}
}
