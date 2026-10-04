# نظام إدارة المعاملات الصادرة والواردة

## المشروع
تطبيق سطح المكتب C# WPF لإدارة المعاملات الصادرة والواردة، مع شاشة تسجيل دخول، إدارة المعاملات، تقارير، وإحصاءات.

## التشغيل المحلي
1. افتح المشروع في Visual Studio 2022
2. تأكد من تثبيت حزمة NuGet التالية:
   - Microsoft.EntityFrameworkCore.Sqlite
3. اختر Configuration = Release
4. قم ببناء المشروع: `dotnet build`
5. شغّل التطبيق: `dotnet run`

## تجهيز ملف الـ Installer
يتم دعم تجهيز الـ installer على Windows باستخدام WiX Toolset.

### المتطلبات
- Windows 10 أو Windows 11
- Visual Studio 2022
- WiX Toolset v3.11 أو أحدث

### خطوات البناء
1. افتح PowerShell كـ Administrator
2. انتقل إلى مجلد `Installer`
3. نفذ:
   ```powershell
   .\build-installer.ps1
   ```
4. سيتم إنشاء ملف MSI داخل `Installer\bin\Release`

### ملاحظات
- الملف العملي يتم إنتاجه في وضع Release فقط
- يمكن تثبيت التطبيق في نظام التشغيل Windows 10/11 بطريقة قياسية عبر ملف .msi
- عند التثبيت، ستظهر اختصارات سطح المكتب وقائمة Start تلقائيًا

## بيانات الدخول الافتراضية
- اسم المستخدم: `admin`
- كلمة المرور: `admin123`
