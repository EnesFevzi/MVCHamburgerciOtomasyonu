namespace MVCHamburgerciOtomasyonu.Web.ResultMessages
{
    
    public static class Messages
	{
		public static class Menu
		{
			public static string Add(string menuName)
			{
				return $"{menuName} başlıklı menü başarıyla eklenmiştir.";
			}
			public static string Update(string menuName)
			{
				return $"{menuName} başlıklı menü başarıyla güncellenmiştir.";
			}
			public static string Delete(string menuName)
			{
				return $"{menuName} başlıklı menü başarıyla silinmiştir.";
			}
			public static string UndoDelete(string menuName)
			{
				return $"{menuName} başlıklı menü başarıyla geri alınmıştır.";
			}
		}
        public static class Message
        {
            public static string Send(string messageName)
            {
                return $"{messageName} başlıklı mesaj başarıyla eklenmiştir.";
            }
            
        }
        public static class User
        {
            public static string Add(string userName)
            {
                return $"{userName} email adresli kullanıcı başarıyla eklenmiştir.";
            }
            public static string Update(string userName)
            {
                return $"{userName} email adresli kullanıcı başarıyla güncellenmiştir.";
            }
            public static string Delete(string userName)
            {
                return $"{userName} email adresli kullanıcı başarıyla silinmiştir.";
            }
        }
        public static class MenuSize
        {
            public static string Add(string menuSizeName)
            {
                return $"{menuSizeName} başlıklı menü boyutu başarıyla eklenmiştir.";
            }
            public static string Update(string menuSizeName)
            {
                return $"{menuSizeName} başlıklı menü boyutu başarıyla güncellenmiştir.";
            }
            public static string Delete(string menuSizeName)
            {
                return $"{menuSizeName} başlıklı menü boyutu başarıyla silinmiştir.";
            }
            public static string UndoDelete(string menuSizeName)
            {
                return $"{menuSizeName} başlıklı menü boyutu başarıyla geri alınmıştır.";
            }
        }
        public static class Extra
        {
            public static string Add(string extraName)
            {
                return $"{extraName} başlıklı ekstra başarıyla eklenmiştir.";
            }
            public static string Update(string extraName)
            {
                return $"{extraName} başlıklı ekstra başarıyla güncellenmiştir.";
            }
            public static string Delete(string extraName)
            {
                return $"{extraName} başlıklı ekstra başarıyla silinmiştir.";
            }
            public static string UndoDelete(string extraName)
            {
                return $"{extraName} başlıklı ekstra başarıyla geri alınmıştır.";
            }
        }
    }

}