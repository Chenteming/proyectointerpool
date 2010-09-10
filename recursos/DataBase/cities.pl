while ( <> ){
	if(/\d+.-.(.*),.(.*)$/){
		print "insert into [interpooldb].[dbo].[Cities] (CityName, CityCountry, Level_LevelId) values (\'" . $1 . "\',\'" .  $2 .  "\', (Select max(LevelId) FROM [interpooldb].[dbo].[Levels]));" . "\n";
	}
}