BULK INSERT Customer
    FROM 'C:\temp\CustomerManagement.Database\Scripts\people.csv'
    WITH
    (
	FIRSTROW = 2,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
	ERRORFILE = 'C:\temp\CustomerManagement.Database\Scripts\Errors.log'
    )

BULK INSERT Colour
    FROM 'C:\temp\CustomerManagement.Database\Scripts\colours.csv'
    WITH
    (
	FIRSTROW = 2,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
	ERRORFILE = 'C:\temp\CustomerManagement.Database\Scripts\Errors.log'
    )

BULK INSERT CustomerColour
    FROM 'C:\temp\CustomerManagement.Database\Scripts\favourite_colours.csv'
    WITH
    (
	FIRSTROW = 2,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
	ERRORFILE = 'C:\temp\CustomerManagement.Database\Scripts\Errors.log'
    )