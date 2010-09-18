while ( <> ){
	if(/(.*)\$(.*)$/){
		print "insert into [interpooldb].[dbo].[CityPropertySet] (CityPropertyContent, Dyn, City_CityId) values (\'" . $2 . "\',  \'false\' ,(Select CityId FROM [interpooldb].[dbo].[Cities] where Lower(CityName) like Lower(\'$1\')));" . "\n";
	}
}