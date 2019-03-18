1.Создать БД с именем lesson7_alebastr
Поправить строку "AttachDBFilename = @"C:\Users\Алкесандр\Documents\lesson7_alebastr.mdf","

2. Создать таблицы
CREATE TABLE [dbo].[Deparments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [DepNme] NVARCHAR(50) NOT NULL
)
CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL, 
    [surname] NVARCHAR(50) NOT NULL, 
    [position] NVARCHAR(100) NOT NULL, 
    [birthday] DATE NOT NULL, 
    [depId] INT NOT NULL, 
    CONSTRAINT [FK_Employee_ToDepart] FOREIGN KEY ([depId]) REFERENCES [Deparments]([Id]) 
)
3. Для начального заполнения раскомментировать код в MainWindow.xaml.cs