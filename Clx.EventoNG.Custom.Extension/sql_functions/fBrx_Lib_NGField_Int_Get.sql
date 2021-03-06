USE [EventoNG_Dev_104]
GO
/****** Object:  UserDefinedFunction [dbo].[fBrx_Lib_NGField_Int_Get]    Script Date: 17.03.2014 10:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER FUNCTION [dbo].[fBrx_Lib_NGField_Int_Get] 
(
	@sEntityType VARCHAR(100),
	@iIDFunction INT,
	@iIDKey INT
)
RETURNS varchar(max)

BEGIN
	declare @result VARCHAR(max)
	SET @result='?'
		
	IF(@sEntityType='Person') BEGIN	

	IF (@iIDFunction=100001) BEGIN 			
			
			SELECT @result=CONVERT(varchar(max), COUNT(*))
				FROM [dbo].[tblPersonenAnmeldung] AS [t1]
				INNER JOIN [dbo].[tblAnmeldung] AS [t2] ON [t2].[IDAnmeldung] = [t1].[IDAnmeldung]
				INNER JOIN [dbo].[tblAnlass] AS [t3] ON [t3].[IDAnlass] = [t2].[IDAnlass]
				WHERE ([t3].[IDAnlassTyp] = 9 AND [t1].[IdPerson]=@iIdKey)
			
	END


	IF (@iIDFunction = 100002) BEGIN

			select @result=CONVERT(varchar(max),
				(select anl.AnlassNummer+';'+s.statusname
				from tblPersonenAnmeldung as pa
				join tblAnmeldung as a on a.idanmeldung = pa.idanmeldung
				join tblAnlass anl on anl.idanlass = a.IDAnlass 
				join tblStatus s on pa.idpastatus = s.idstatus
				where pa.idperson= @iIdKey
				FOR XML PATH))

		END

							
		IF (@iIDFunction=70001) BEGIN 
			
			select @result=p.PersonVorname +' ' + p.PersonNachname
			FROM Person p
			WHERE p.IdPerson=@iIdKey
			
		END
		
		IF (@iIDFunction=70002) BEGIN 
			
			select @result=CONVERT(varchar(max),p.IDPersonStatus)
			FROM Person p
			WHERE p.IdPerson=@iIdKey
			
		END
		
		IF (@iIDFunction=70003) BEGIN 
			
			select @result=CONVERT(varchar(max),p.PersonAktiv)
			FROM Person p
			WHERE p.IdPerson=@iIdKey
			
		END
	
		IF (@iIDFunction=70005) BEGIN 
			
			SELECT @RESULT=CONVERT(VARCHAR(max),
				(select ISNULL(cr.PCText,'-')+';'+convert(varchar,cr.PCWert)+';'+CONVERT(varchar,cr.PCDatum)+';'+CONVERT(varchar,YEAR(cr.PCDatum))
						FROM Person p
						JOIN PersonCredit cr ON p.IDPerson = cr.PCIdPerson
						WHERE p.IdPerson=@iIdKey
						FOR XML PATH))
			
		END
		
		IF (@iIDFunction=70006) BEGIN 
			
			SELECT @RESULT = '<row>Name;' + p.PersonVorname+' '+p.PersonNachname+'</row>'
			FROM Person p
			WHERE p.IdPerson=@iIdKey
			SELECT @RESULT = @result+ '<row>PLZ/Ort;' + p.PersonPlz+' '+p.PersonOrt+'</row>'
			FROM Person p
			WHERE p.IdPerson=@iIdKey
			SELECT  @RESULT = @result+'<row>Anzahl Modulanlass-Anmeldungen;' + CONVERT(varchar, COUNT(*))+'</row>'
			FROM PersonenAnmeldung pa 
			JOIN Anmeldung anm on anm.IDAnmeldung = pa.IDAnmeldung 
			JOIN Anlass a ON a.IDAnlass = anm.IDAnlass and a.idanlasstyp=3
			where pa.IDPerson = @iIDKey
			
		END
		
		IF (@iIDFunction=70007) BEGIN 
			
			SELECT @RESULT=CONVERT(VARCHAR(max),
				(select '@Key'+convert(varchar,pa.IDAnmeldung)+','+convert(varchar,pa.IDPerson)+'|'+ a.AnlassNummer +';'+a.AnlassBezeichnung
						FROM Person p
						JOIN PersonenAnmeldung pa on p.IDPerson = pa.IDPerson
						JOIN Anmeldung anm on anm.IDAnmeldung = pa.IDAnmeldung 
						JOIN Anlass a on a.IDAnlass = anm.IDAnlass 
						WHERE p.IdPerson=@iIdKey
							and a.IDAnlassTyp=3
							and a.AnlassBezeichnung LIKE '%INFO%'
						FOR XML PATH))
			
		END

END
     IF(@sEntityType='Anlass') BEGIN	

	 IF (@iIDFunction = 100004) BEGIN

		     select @result=CONVERT(varchar(max),
			 (select ZInfoBezeichnung+';'+CONVERT(VARCHAR,ZInfoTermin ,104 )+';'+ZInfoIdBenutzer
			  FROM dbo.Anlass A
              JOIN dbo.Zusatzinformation ZI ON ZI.ZInfoRefKey = A.IDAnlass AND ZInfoRefObjArt = 1 AND ZInfoAktiv = 1
              WHERE IDAnlass = @iIdKey
              AND ZInfoCode = 'WBZA Funktionen'
              AND ZInfoArt = 'I'
			  FOR XML PATH))

		END
	 END
	
RETURN @result
END



