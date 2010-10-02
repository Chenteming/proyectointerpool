while ( <> ){
	if(/(.*)\$(.*)\$(.*)/){
		print "insert into [interpooldb].[dbo].[Famous] (FamousName, City_CityId, NameFileFamous) values (\'" . $2 . "\', (Select CityId FROM [interpooldb].[dbo].[Cities] where Lower(CityName) like Lower(\'$1\')), \'" . $3 . "\');" . "\n";
	}
}