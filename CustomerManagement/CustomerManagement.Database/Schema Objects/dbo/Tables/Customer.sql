CREATE TABLE [dbo].[Customer]
(
	[Id] INT IDENTITY NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[LastActive]  DATETIME NOT NULL,
	[WebCustomer] BIT NOT NULL,
	[PreviouslyOrdered] BIT NOT NULL,
	CONSTRAINT pk_customerId PRIMARY KEY (Id)
)