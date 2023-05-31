CREATE TABLE [dbo].[Group]
(
	[Name] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Year] DATE NULL, 
    [Study Time] NVARCHAR(50) NULL, 
    [Course] NCHAR(10) NULL, 
    [Uid Specialization] NVARCHAR(50) NOT NULL, 
    [Accepted] INT NULL, 
    [first year] NCHAR(10) NULL, 
    [second year] NCHAR(10) NULL, 
    [third year] NCHAR(10) NULL
)
