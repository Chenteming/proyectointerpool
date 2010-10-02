while ( <> ){
	if(/(.*)\$(.*)\$(.*)/){
		print "insert into [interpooldb].[dbo].[CityPropertySet] FamousName, (City_CityId, NameFile) values (\'" . $2 . "\', (Select CityId FROM [interpooldb].[dbo].[Cities] where Lower(CityName) like Lower(\'$1\')), \'" . $3 . "\');" . "\n";
	}
}