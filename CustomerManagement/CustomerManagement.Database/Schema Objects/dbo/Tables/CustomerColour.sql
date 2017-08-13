CREATE TABLE [dbo].[CustomerColour]
(
	[CustomerId] INT NOT NULL,
	[ColourId] INT NOT NULL,
	CONSTRAINT pk_customerIdColor PRIMARY KEY (CustomerId, ColourId),
	CONSTRAINT fk_customerColorCustomerId FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
	CONSTRAINT fk_customerColourColourId  FOREIGN KEY(ColourId) REFERENCES Colour(Id)
)