USE BACKOFFICE
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TANKS_INFO]') AND type in (N'U'))
BEGIN
	CREATE TABLE TANKS_INFO (TankNr INT PRIMARY KEY,
							 ProductNr INT,
							 ProductId CHAR(20),
							 DateLastMeasure CHAR(8),
							 TimeLastMeasure CHAR(6),
							 Alarms VARCHAR(20),
							 FuelHeight NUMERIC(14,5),
							 FuelTemp NUMERIC(14,5),
							 FuelVolume NUMERIC(14,5),
							 MeasureUnit CHAR(5),
							 TemperatureUnit CHAR(1),
							 WaterHeight NUMERIC(14,5),
							 WaterVolume NUMERIC(14,5))
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TANKS_INFO_HIST]') AND type in (N'U'))
BEGIN
	CREATE TABLE TANKS_INFO_HIST (TankNr INT,
								  ProductNr INT,
								  ProductId CHAR(20),
								  DateLastMeasure CHAR(8),
								  TimeLastMeasure CHAR(6),
								  Alarms VARCHAR(20),
								  FuelHeight NUMERIC(14,5),
								  FuelTemp NUMERIC(14,5),
								  FuelVolume NUMERIC(14,5),
								  MeasureUnit CHAR(5),
								  TemperatureUnit CHAR(1),
								  WaterHeight NUMERIC(14,5),
								  WaterVolume NUMERIC(14,5),
								  DateTimeMeasure DATETIME)
END
GO