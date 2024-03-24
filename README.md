Name of Angular GUI of this project is StockControl.
Please check ReadMe.txt file in order to see the aligned versions of the following table examples.

---------------------------------------------------------------------
		        Angular ve WebAPI ile Stok Kontrol Uygulaması			
					
							Elif Güner
							
							25.01.2024
---------------------------------------------------------------------					

Projede frontend olarak Angular, backend olarak WebAPI kullanılmıştır.
WebAPI, MSSQL DB'den Entity Framework Core ile veri okuma-yazma amacıyla kullanılmıştır.
Frontend'de kullanılan sign in ve stok kontrol sayfaları bootstrap template ile yapılmıştır.


---------------------------------------------------------------------

1. Projenin angular kısmını açmak için powershell ile projenin olduğu dizine gidilir
   ve code . komutu ile proje Visual Studio Code ile açılır :

	PS C:\Users\husey> cd C:\Users\husey\Desktop\Elif-workspace\Angular\StockControl
	PS C:\Users\husey\Desktop\Elif-workspace\Angular\StockControl> code .

2. F12 ile console açılır ve 'ng serve' komutu ile proje çalıştırılır ve web browserda açılır.
	
3. C:\Users\husey\Desktop\Elif-workspace\StockControlAPI projesinde Angular arayüzünün
   haberleştiği WebApi bulunur.
   
   StockControl.sln projesi Visual Studio'da açılır ve çalıştırılır.
   
   Bu proje Entity Framework Core ORM altyapısını kullanır.
   
   MS SQL Server'a bağlanarak StockControllerAPIDB isimli bir DB ve içine aşağıdaki tabloları ve kayitlari oluşturur:
   
 select * from [StockControllerAPIDB].[dbo].[Users]
 
Id          UserName      Password
----------- ------------- ----------
1           elif          elif1
2           ege           ege1

 select * from [StockControllerAPIDB].[dbo].[Categories]

Id          CategoryName
----------- --------------
1           Bilgisayar
2           Beyaz Eşya
3           Ayakkabı

 select * from [StockControllerAPIDB].[dbo].[Warehouses]

Id          WarehouseName
----------- ---------------
1           Ankara
2           İstanbul
3           İzmir

 select * from [StockControllerAPIDB].[dbo].[Products]

Id          CategoryId  WarehouseId ProductName   Count       Price
----------- ----------- ----------- ------------- ----------- ---------
1           1           1           Casper        3           1000.00
2           1           2           Lenovo        5           1500.00
3           2           2           Buzdolabı     4           3000.00
4           2           3           Fırın         6           2000.00

   
4. angular projesi çalıştırıldığında web browserda gelen sign in sayfasına Users tablosundaki
   username\password'ler ile "login" olunur ve stock kontrol sayfasına yönlendirme yapılır.
   İki sayfa da bootstrap ile yapılmıştır.

5. Var olan username\password'ler yerine yenisi kullanılmak isteniyorsa "Register" butonuna 
   tıklanarak kayıt yapılır ve stock kontrol sayfasına yönlendirme yapılır.

6. Stok kontrol sayfasından ürün ekleme, silme, getirme ve güncelleme işlemleri yapılabilir.
   Ürün getirmek için, ürün adı girilip "Retrieve"  butonuna tıklanmalıdır.
   Ekleme sırasında, DB'de yer almayan kategori bilgisi girilirse bu bilgi Categories tablosuna 
   da eklenir.
   Update işlemi için ürün adı girilmesi zorunludur. Diğer alanlardan yalnızca değer girilen 
   alanlar güncellenir.
   
7. Stok kontrol işlemlerini yalnızca kayıtlı kullanıcıların yapabilmesi için, angular tarafında 
   HttpInterceptor ile bütün requestlerin header kısmına username\password eklendi.
   WebApi kısmında ise Basic Authentication kullanıldı.
   
   
