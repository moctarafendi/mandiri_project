# mandiri_project

Project ini dikhususkan untuk menyelesaikan soal tes dari Bank Mandiri sebagai BE .Net Developer.

Fitur di dalam project ini berupa services annual review karyawan:
1. CRUD User (user yang login) -> Role ada ADMIN dan ADMINISTRATOR
2. CRUD Employee (employee sebagai karyawan)
3. CRUD Business Area (pembagian wilayah untuk user)
4. Transaksi Annual Review
5. Sudah di proteksi menggunakan JWT Auth

Tools :
1. Microsoft Visual Studio 2022 Enterprise Edition
2. Microsoft SQL Server 2017
3. SQL Server Management Studio v18.12.1
4. Postman v10.20
   
Step :
1. Clone project
2. Create database
3. Jalankan script create table-> ~\mandiri_project\Migrations\2023.11.26_Script Create Table.sql
4. Jalankan script insert data dummy-> ~\mandiri_project\Migrations\2023.11.26_Script Insert_data.sql
5. Setting connection string di dalam appSettings.json
6. Running project menggunakan Visual Studio 2022
7. Import Postman collection dari Jalankan script -> ~\mandiri_project\Mandiri.postman_collection.json
8. Lakukan Login terlebih dahulu menggunakan api jwt/login menggunakan email : moctarafendi@gmail.com , password : 12345
9. Token yang sudah didapatkan ditaruh di parent postmanCollection
10. Setelah mendapat token baru bisa mengakses api yang lain

Created By :
Moctar Afendi @2023
