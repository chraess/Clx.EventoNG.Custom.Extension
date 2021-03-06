USE [EventoNG_Dev_104]
GO
/****** Object:  UserDefinedFunction [dbo].[fBrx_Lib_NGField_IntInt_Get]    Script Date: 17.03.2014 11:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fBrx_Lib_NGField_IntInt_Get] 
(
	@sEntityType VARCHAR(100),
	@iIDFunction INT,
	@iIDKey1 INT,
	@iIDKey2 INT
)
RETURNS varchar(max)
AS
BEGIN
declare @result VARCHAR(max)
SET @result='?'
	
IF(@sEntityType='PersonenAnmeldung') BEGIN	
	
	
	
	IF (@iIDFunction=70004) BEGIN 
		
		select @result=a.AnlassBezeichnung+' '+p.PersonNachname
		FROM PersonenAnmeldung pa
		join Anmeldung anm on pa.IDAnmeldung = anm.IDAnmeldung
		join Anlass a on a.IDAnlass = anm.IDAnlass 
		join Person p on p.IDPerson = pa.IDPerson
		WHERE pa.IDAnmeldung=@iIDKey1 and pa.IDPerson=@iIDKey2
		
	END
	
	
END
RETURN @result
END
