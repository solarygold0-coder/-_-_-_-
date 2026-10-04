# نظام إدارة المعاملات الصادرة والواردة

تطبيق سطح مكتب احترافي مكتوب بـ C# و WPF لإدارة المعاملات الصادرة والواردة.

## المميزات

✅ واجهة عربية RTL احترافية
✅ إدارة المعاملات (إضافة، تعديل، حذف، بحث)
✅ تصنيفات الصادر والوارد
✅ حالات المعاملة (معلقة، جاري، مكتملة، متأخرة)
✅ لوحة تحكم مع إحصائيات
✅ تقارير شاملة
✅ نظام مستخدمين وأدوار
✅ قاعدة بيانات محلية SQLite
✅ تصميم Modern مع Material Design

## المتطلبات

- .NET 6.0 SDK أو أحدث
- Visual Studio 2022 أو VS Code
- SQL Server أو SQLite

## التثبيت

1. استنساخ المستودع:
```bash
git clone https://github.com/solarygold0-coder/نظام_معاملات_صادر_وارد.git
cd نظام_معاملات_صادر_وارد
```

2. فتح المشروع في Visual Studio

3. استعادة NuGet packages:
```bash
dotnet restore
```

4. تحديث قاعدة البيانات:
```bash
dotnet ef database update
```

5. تشغيل التطبيق:
```bash
dotnet run
```

## الاستخدام

### بيانات المسؤول الافتراضية
- اسم المستخدم: admin
- كلمة المرور: admin123

## الهيكل

```
TransactionManagementSystem/
├── Models/              # نماذج البيانات
├── Data/               # قاعدة البيانات
├── Services/           # الخدمات والمنطق
├── ViewModels/         # ViewModels
├── Views/              # واجهات المستخدم
└── Resources/          # الموارد
```

## المطورون

- solarygold0-coder

## الرخصة

MIT License